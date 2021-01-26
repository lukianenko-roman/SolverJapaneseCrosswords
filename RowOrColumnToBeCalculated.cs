using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolverJapaneseCrosswords
{
    class RowOrColumnToBeCalculated : RowOrColumnToBeCalculatedComparator
    {
        // Класс для создания массива всех строк и колонок условия для дальнейшего расчета.
        // Наследуется от класса, в котором реализован метод для сортировки массива для обработки строки / столбца с наименьшим кол-вом вариантов расположения условия (т.е. ускорения работы программы)
        internal static bool IsMainFieldFilled { get; private set; }    // заполнено ли поле
        private static int TotalFilledCellsTarget { get; set; }         // кол-во "черных" клеток согласно условия
        internal static int RowsCount { get; private set; }
        internal static int ColumnsCount { get; private set; }
        private static Cell[,] mainField;                               // поле с решением
        internal static int TotalFilledCells { get; private set; }      // счетчик "черных" ячеек

        private bool areAllVariantsCalculated;                          // расчитаны ли все варианты расположения условия
        private int lengthOfRowOrColumn;                                // длина строки или столбца (т.е. 
        private int elementsCount;                                      // кол-во элементов в условии
        private int emptiesCount;                                       // кол-во "пустых" клеток в строке/столбце (длина строки/столбца - сумма элементов условия)
        private int[] condition;                                        // условие
        private int numberInQueue;                                      // номер в очереди на вызов (расчитывается методом сортировки в классе RowOrColumnToBeCalculatedComparator)
        internal List<bool[]> possibleSolutions;                        // все возможные варианты расположения условия (расчитывается с учетом заполненности поля с решением)
        internal List<Task> tasksForRemovingIrrelevantVariants;         // список задач для удаления неподходящих вариантов (выполняются в фоновом режиме)
        private int tasksCount;
        internal static bool IsError { get; private set; }

        internal RowOrColumnToBeCalculated(bool isRow, int indexOfRowOrColumn, int[] condition, bool isNew = true)
        {
            // присвоение стартовых параметров
            this.condition = condition;
            IsRow = isRow;
            IndexOfRowOrColumn = indexOfRowOrColumn;
            lengthOfRowOrColumn = isRow ? ColumnsCount : RowsCount;
            possibleSolutions = new List<bool[]>();
            areAllVariantsCalculated = false;
            IsNeedToRemoveVariants = false;
            tasksCount = 0;
            tasksForRemovingIrrelevantVariants = new List<Task>();
            if (isNew)
            {
                IsFull = false;
                TotalFilledCells = 0;
                elementsCount = condition.Length;
                emptiesCount = lengthOfRowOrColumn - condition.Sum();
                VariantsCount = GetCountOfPossibleVariants();
            }
        }
        internal static void SetStatics(Cell[,] _mainField, int rowsCount, int columnsCount, int totalFilledCellsTarget)
        {
            // присвоение стартовых статических параметров. Вызывается, когда все условия проверены на корректность
            mainField = _mainField;
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            TotalFilledCellsTarget = totalFilledCellsTarget;
            IsMainFieldFilled = false;
            IsError = false;
        }
        internal static void Calculate(RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated)
        {
            // сортировка* массива условий (строк и столбцов) и вызов расчета первого элемента до тех пор, пока не найдено решение
            // либо за итерацию** не заполнена ни одна ячейка (т.е. нет единственно корректного решения или нет решения в принципе)
            // *массив изначальных элементов остается в порядке, в котором был создан, номер в очереди на вызов передается в переменную numberInQueue
            // **итерация включает в себя перебор всех элементов массива, если в первом и последующих элементах не была заполнена ни одна ячейка
            do
            {
                RowOrColumnToBeCalculatedComparator[] rowsOrColumnsToBeCalculatedComparator = new RowOrColumnToBeCalculatedComparator[RowsCount + ColumnsCount];
                for (int i = 0; i < RowsCount + ColumnsCount; i++) rowsOrColumnsToBeCalculatedComparator[i] = new RowOrColumnToBeCalculatedComparator(rowsAndColumnsToBeCalculated[i]);
                Array.Sort(rowsOrColumnsToBeCalculatedComparator);
                for (int i = 0; i < RowsCount + ColumnsCount; i++)
                    for (int j = 0; j < RowsCount + ColumnsCount; j++)
                        if (rowsAndColumnsToBeCalculated[i].IndexOfRowOrColumn == rowsOrColumnsToBeCalculatedComparator[j].IndexOfRowOrColumn &&
                            rowsAndColumnsToBeCalculated[i].IsRow == rowsOrColumnsToBeCalculatedComparator[j].IsRow)
                        {
                            rowsAndColumnsToBeCalculated[i].numberInQueue = j;
                            break;
                        }
            }
            while (rowsAndColumnsToBeCalculated[GetIndexOfRowOrColumnByNumberInQueue(rowsAndColumnsToBeCalculated, 0)].TryFillCellFullOrEmpty(rowsAndColumnsToBeCalculated));
        }
        private bool TryFillCellFullOrEmpty(RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated, int deep = 0)
        {
            bool isAnyResultInThisItitration = false;
            // если варианты расположения условия не расчитаны - рассчитать
            if (!areAllVariantsCalculated)
            {
                VariantsCount = 0;
                SetPossibleSolusions();
                areAllVariantsCalculated = true;
                IsNeedToRemoveVariants = false;
            }
            // если есть необходимость удалить неподходящие варианты* - удалить или дождаться выполнения удаления
            // *как только в какой-либо строке/столбце закрашивается любая ячейка, то часть вариантов расположения условия для столбца/строки этой ячейки можно отсечь
            if (IsNeedToRemoveVariants)
                if (tasksForRemovingIrrelevantVariants.Count == 0)
                {
                    TryRemoveIrrelevantVariants();
                    IsNeedToRemoveVariants = false;
                }
                else
                {
                    //while (IsNeedToRemoveVariants)
                    Task.WaitAll(tasksForRemovingIrrelevantVariants.ToArray());
                }
            if (VariantsCount == 0)
            {
                IsError = true;
                return false;
            }
            (int, int) link = (IsRow ? IndexOfRowOrColumn : 0, IsRow ? 0 : IndexOfRowOrColumn);
            // проверка для каждой ячейки в дальнейшем решении
            for (int i = 0; i < lengthOfRowOrColumn; i++)
            {
                if (IsRow) link.Item2 = i; else link.Item1 = i;
                // если ячейка уже заполнена - переходим к следующей
                if (mainField[link.Item1, link.Item2].State == StatesOfCell.unknown)
                {
                    // сравниваем значение данной ячейки в каждом из вариантов
                    bool target = possibleSolutions[0][i];
                    bool areAllVariantsEquelForThisCell = true;
                    for (int j = 1; j < VariantsCount; j++)
                    {
                        if (possibleSolutions[j][i] != target)
                        {
                            areAllVariantsEquelForThisCell = false;
                            VariantsCount = possibleSolutions.Count;
                            break;
                        }
                    }
                    // если во всех вариантах расположения условия данная ячейка одинакова, то
                    if (areAllVariantsEquelForThisCell)
                    {
                        mainField[link.Item1, link.Item2].State = target ? StatesOfCell.full : StatesOfCell.empty;  // заполняем поле с решением
                        Cell.FillCell(link.Item1, link.Item2, target);                                              // красим ячейку для вывода пользователю
                        TotalFilledCells += target ? 1 : 0;                                                         // увеличиваем счетчик заполненных "черных" ячеек
                        isAnyResultInThisItitration = true; // указываем, что в данной итерации была заполнена как минимум 1 ячейка, т.е. следующую итерацию нужно начинать с элемента,
                                                            //  у которого меньше всего вариантов расположения условия
                        int rowOrColumnToChangeIndex = i + (IsRow ? RowsCount : 0);             // ячейка заполнена, значит можно отсечь часть вариантов, указываем индекс строки/столбца для удаления вариантов

                        if (rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].areAllVariantsCalculated == true) // если все варианты уже расчитаны, то запускаем удаление неподходящих в фоне
                        {                                                                                           // в противном случае неподходящие варианты будут отсечены на этапе расчета
                            rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].IsNeedToRemoveVariants = true;   // ***запуск расчета всех вариантов в фоне может занять очень большой объем памяти для больших кроссвордов
                            AddAndStartTaskForRemovingIrrelevantVariants(rowsAndColumnsToBeCalculated, rowOrColumnToChangeIndex, IndexOfRowOrColumn);
                        }
                    }
                }
            }
            if (VariantsCount == 1) // если остался только один вариант,
            {
                IsFull = true;      // строка заполнена
                if (TotalFilledCells == TotalFilledCellsTarget) // если все "черные" клетки заполнены,
                {
                    IsMainFieldFilled = true;                   // решение найдено
                    FillUnkownCellsAsEmpties();                 // заполняем оставшиеся (если такие будут) незаполненные ячейки "пустыми"
                    return false;                               // выходим из цикла
                }
            }
            if (!isAnyResultInThisItitration && // если ни одна ячейка не заполнена за текущую итерацию,
                deep + 1 < RowsCount + ColumnsCount && // просматривается не последний элемент,
                !rowsAndColumnsToBeCalculated[GetIndexOfRowOrColumnByNumberInQueue(rowsAndColumnsToBeCalculated, deep + 1)].IsFull) // следующий элемент не заполнен полностью,
                //то рекурсивный вызов следующего элемента
                isAnyResultInThisItitration = rowsAndColumnsToBeCalculated[GetIndexOfRowOrColumnByNumberInQueue(rowsAndColumnsToBeCalculated, deep + 1)].TryFillCellFullOrEmpty(rowsAndColumnsToBeCalculated, deep + 1);

            return isAnyResultInThisItitration;
        }
        private void FillUnkownCellsAsEmpties()
        {
            // вызывается, если все "черные" ячейки заполнены. заполняет неокрашенные ячейки пустотами
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    if (mainField[i, j].State == StatesOfCell.unknown)
                    {
                        mainField[i, j].State = StatesOfCell.empty;
                        Cell.FillCell(i, j, false);
                    }
        }
        private void AddAndStartTaskForRemovingIrrelevantVariants(RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated, int rowOrColumnToChangeIndex, int cellNumberToCheck = -1)
        {
            // запуск нового Task на удаление неактуальных вариантов с учетом ячейки, которую нужно просматривать. новый Task запускается после выполнения предыдущего, если их несколько
            rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksForRemovingIrrelevantVariants.Add(new Task(() =>
            {
                rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].IsNeedToRemoveVariants = true;
                int currentCount = rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksCount + 1;
                rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksCount++;
                if (currentCount != 1)
                    rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksForRemovingIrrelevantVariants[currentCount - 2].Wait();
                rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].TryRemoveIrrelevantVariants(cellNumberToCheck);
                if (rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksForRemovingIrrelevantVariants.Count == currentCount)
                    rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].IsNeedToRemoveVariants = false;
            }));
            rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksForRemovingIrrelevantVariants[rowsAndColumnsToBeCalculated[rowOrColumnToChangeIndex].tasksForRemovingIrrelevantVariants.Count - 1].Start();
        }
        internal static int GetIndexOfRowOrColumnByNumberInQueue(RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated, int numberForSearch)
        {
            // возвращает индекс элемента по заданному номеру в очереди на вызов
            for (int i = 0; i < RowsCount + ColumnsCount; i++)
                if (rowsAndColumnsToBeCalculated[i].numberInQueue == numberForSearch)
                    return i;
            return -1;
        }
        private int GetCountOfPossibleVariants()
        {
            // возвращеет изначальное кол-во вариантов расстановки условия исходя из кол-ва элементов в условии строки/столбца и кол-ва заданных пустых ячеек
            // из-за возможного превышения разрядности Int в случае превышения возвращеется int.MaxValue для "откидывания" элемента в конец очереди
            // если очередь дойдет до элемента с таким кол-вом вариантов, то варианты будут расчитываться с учетом заполненных ячеек (в итоге вариантов будет меньше)
            ulong result = 1;
            int k = emptiesCount + 1 - elementsCount;
            int j = 1;
            for (int i = elementsCount + 1; i <= emptiesCount + 1; i++)
            {
                result *= (ulong)i;
                if (j <= k)
                {
                    result /= (ulong)j;
                    j++;
                }
            }
            if (result > int.MaxValue)
                return int.MaxValue;
            return (int)result;
        }
        private int SetPossibleSolusions(int[] prevValues = null, int deep = 0)
        {
            // рекурсивый перебор всех вариантов расположения пустых клеток
            int[] variantToAdd;
            int valueFrom;
            int valueTo;
            if (prevValues != null) // если не первичный вызов метода
            {
                valueFrom = 1; // начинаем перебор с 1, т.к. между заданными элементами должна быть минимум 1 пустая ячейка
                int sumOfPrevValues = prevValues.Sum();
                valueTo = Math.Min(emptiesCount - elementsCount + 2, emptiesCount - sumOfPrevValues - elementsCount + prevValues.Length + 1); // расчет значения, до которого делать перебор по текущему индексу
                                                                                                                                              // с учетом максимального количества пустых ячеек и предыдущих подставленных значений
                variantToAdd = new int[prevValues.Length + 1]; // копируем переданный массив, оставляем одно значение пустым
                for (int j = 0; j < prevValues.Length; j++)
                    variantToAdd[j] = prevValues[j];
            }
            else // если первичный вызов метода
            {
                variantToAdd = new int[1];
                valueFrom = 0;                              // начинаем перебор с 0, т.к. отступ от края до первого элемента может быть 0
                valueTo = emptiesCount - elementsCount + 1; // задаем максимальное значение с учетом правил расстановки элементов
            }
            for (int i = valueFrom; i <= valueTo; i++) // перебор всех возможных вариантов для текущей позиции
            {
                if (prevValues != null) // если не первичный вызов метода
                {
                    variantToAdd[prevValues.Length] = i; // добавляем в текущую позицию значение
                    if (prevValues.Length == elementsCount - 1) // если дошли до последнего элемента для заполнения
                    {
                        int emptyNumberWithMistake = SetPossibleSolusionForOneVariant(variantToAdd); // передаем полученный вариант расположения пустых ячеек, получаем номер элемента с ошибкой, если такой такой есть,
                        if (emptyNumberWithMistake != int.MaxValue) // и возвращаемся до элемента с ошибкой (т.е. до неподходящего под текущую заполненность решения элемента и продолжаем перебор, тем самым отсекая
                            if (deep > emptyNumberWithMistake)      // ненужный перебор заведомо неподходящих вариантов. если ошибка нет - получаем int.MaxValue и продолжаем перебор. По аналогии - для первичного вызова
                                return emptyNumberWithMistake;
                    }
                    else
                    {   // рекурсивное "углубление", если нет ошибки на текущем значении
                        int emptyNumberWithMistake = SetPossibleSolusions(variantToAdd, deep + 1);
                        if (emptyNumberWithMistake != int.MaxValue)
                        {
                            if (deep > emptyNumberWithMistake)
                                return emptyNumberWithMistake;
                        }
                    }
                }
                else
                {
                    variantToAdd[0] = i;
                    if (elementsCount != 1)
                    {// рекурсивное "углубление", если нет ошибки на текущем значении
                        if (SetPossibleSolusions(variantToAdd, deep + 1) != int.MaxValue)
                            continue;
                    }
                    else
                        SetPossibleSolusionForOneVariant(variantToAdd);
                }
            }
            return int.MaxValue;
        }
        private int SetPossibleSolusionForOneVariant(int[] variantToAdd)
        {
            // расчет варианта расположения элементов в зависимости от полученного расположения пустых ячеек (true - "черные", false - "пустые")
            int counter = 0;
            bool[] varToAdd = new bool[lengthOfRowOrColumn];
            for (int i = 0; i < elementsCount; i++)
            {
                for (int j = counter + variantToAdd[i]; j < counter + condition[i] + variantToAdd[i]; j++)
                    varToAdd[j] = true;
                counter += variantToAdd[i] + condition[i];
            }
            // проверка варианта согласно текущей заполненности поля для решения, если нет ошибки - добавляем как возможное решение, иначе возвращаем номер элемента "пустоты", из-за которого возникла ошибка
            int emptyNumberWithMistake = CheckVariantToAdd(varToAdd);
            if (emptyNumberWithMistake == int.MaxValue)
            {
                possibleSolutions.Add(varToAdd);
                VariantsCount = possibleSolutions.Count;
                return int.MaxValue;
            }
            else
            {
                return emptyNumberWithMistake;
            }
        }
        private int CheckVariantToAdd(bool[] varToAdd)
        {
            // проверка переданного варианта заполнения в соответствии с текущим состоянием поля для решения
            (int, int) link = (IsRow ? IndexOfRowOrColumn : 0, IsRow ? 0 : IndexOfRowOrColumn);
            for (int i = 0; i < lengthOfRowOrColumn; i++)
            {
                if (IsRow) link.Item2 = i; else link.Item1 = i;
                if (mainField[link.Item1, link.Item2].State != StatesOfCell.unknown)
                {
                    bool target = mainField[link.Item1, link.Item2].State == StatesOfCell.full ? true : false;
                    if (target != varToAdd[i])
                    {
                        return GetEmptyNumberFromVariantWithMistake(varToAdd, i); // вернуть номер "пустоты" по порядку, на котором возникли ошибка
                    }
                }
            }
            return int.MaxValue;
        }
        private int GetEmptyNumberFromVariantWithMistake(bool[] varToAdd, int iMistake)
        {
            // поиск номера по порядку "пустоты", из-за которого вариант не подходит
            int result = 0;
            if (!varToAdd[iMistake]) result--;
            for (int i = iMistake; i > -1; i--)
            {
                if (varToAdd[i])
                {
                    result++;
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (!varToAdd[j])
                        {
                            i = j + 1;
                            break;
                        }
                        if (j == 0)
                            return result;
                    }
                }
            }
            return result;
        }
        private void TryRemoveIrrelevantVariants(int cellNumberToCheck = -1)
        {
            // удаление неактуальных вариантов расположения элеметнов условия с/без учета номера ячейки, по которой нужно провести проверку
            (int, int) link = (IsRow ? IndexOfRowOrColumn : 0, IsRow ? 0 : IndexOfRowOrColumn);
            int iFrom = (cellNumberToCheck == -1) ? 0 : cellNumberToCheck;
            int iTo = (cellNumberToCheck == -1) ? lengthOfRowOrColumn : cellNumberToCheck + 1;
            for (int i = iFrom; i < iTo; i++)
            {
                if (IsRow) link.Item2 = i; else link.Item1 = i;
                if (mainField[link.Item1, link.Item2].State != StatesOfCell.unknown)
                {
                    bool target = mainField[link.Item1, link.Item2].State == StatesOfCell.full ? true : false;
                    for (int j = 0; j < VariantsCount; j++)
                    {
                        if (possibleSolutions[j][i] != target)
                        {
                            possibleSolutions.RemoveAt(j);
                            VariantsCount--;
                            j--;
                        }
                    }
                }
            }
        }
        internal static Cell[,] CloneMainField()
        { // для копирования текущего состояния решения
            Cell[,] mainFieldToBeReturned = new Cell[RowsCount, ColumnsCount];
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                {
                    mainFieldToBeReturned[i, j] = new Cell();
                    mainFieldToBeReturned[i, j].State = mainField[i, j].State;
                }
            mainField[0, 0].State = StatesOfCell.empty;
            return mainFieldToBeReturned;
        }
        internal static void SetStatics(RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated, CloneOfStaticsOfRowOrColumnToBeCalculated clone)
        {
            // возврат статических значений при вызове перебора всех вариантов
            CloneArray(clone.cloneRowOrColumnToBeCalculated, rowsAndColumnsToBeCalculated);
            rowsAndColumnsToBeCalculated = (RowOrColumnToBeCalculated[])clone.cloneRowOrColumnToBeCalculated.Clone();
            IsError = false;
            IsMainFieldFilled = clone.isMainFieldFilled;
            TotalFilledCells = clone.totalFilledCells;
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                {
                    mainField[i, j] = new Cell();
                    mainField[i, j].State = clone.mainField[i, j].State;
                    Cell.FillCell(i, j, mainField[i, j].State == StatesOfCell.empty ? false : true, mainField[i, j].State == StatesOfCell.unknown ? true : false);
                }
        }
        internal static void CloneArray(RowOrColumnToBeCalculated[] copyFrom, RowOrColumnToBeCalculated[] copyTo)
        {
            // возврат значений в случае перебора всех вариантов
            for (int i = 0; i < copyTo.Length; i++)
            {
                copyTo[i] = new RowOrColumnToBeCalculated(copyFrom[i].IsRow, copyFrom[i].IndexOfRowOrColumn, copyFrom[i].condition, false);
                copyTo[i].areAllVariantsCalculated = true;
                copyTo[i].IsFull = copyFrom[i].IsFull;
                copyTo[i].IsNeedToRemoveVariants = copyFrom[i].IsNeedToRemoveVariants;
                copyTo[i].VariantsCount = copyFrom[i].VariantsCount;
                copyTo[i].possibleSolutions = new List<bool[]>();
                for (int j = 0; j < copyFrom[i].possibleSolutions.Count; j++)
                {
                    bool[] massToAdd = new bool[copyFrom[i].possibleSolutions[j].Length];
                    for (int k = 0; k < copyFrom[i].possibleSolutions[j].Length; k++)
                    {
                        massToAdd[k] = copyFrom[i].possibleSolutions[j][k];
                    }
                    copyTo[i].possibleSolutions.Add(massToAdd);
                }
            }
        }
    }
}