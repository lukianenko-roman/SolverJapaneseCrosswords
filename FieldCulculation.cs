using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolverJapaneseCrosswords
{
    partial class Field
    {
        private RowOrColumnToBeCalculated[] rowsAndColumnsToBeCalculated;           // изначальный массив всех строк и колонок для расчета
        private List<Cell[,]> solutions;                                         // коллекция решений на случай перебора (если решение не может быть найдено без перебора)
        private string messageToAdd;
        private string MessageToAdd { get { return messageToAdd; } set { messageToAdd = value; OnAddingMessage(messageToAdd); } }
        internal delegate void AddingMessage(string message, int colorARGB = 0x780000FF);
        internal static event AddingMessage OnAddingMessage;
        internal void SolveCrossword()
        {
            // создание всех элементов rowsAndColumnsToBeCalculated
            rowsAndColumnsToBeCalculated = new RowOrColumnToBeCalculated[mainFieldHeight + mainFieldWidth];
            Task[] tasks = new Task[mainFieldHeight + mainFieldWidth];
            for (int i = 0; i < mainFieldHeight; i++)
            {
                int j = i;
                tasks[j] = new Task(() => rowsAndColumnsToBeCalculated[j] = new RowOrColumnToBeCalculated(true, j, conditionRows[j].ToArray()));
                tasks[j].Start();
            }
            for (int i = 0; i < mainFieldWidth; i++)
            {
                int j = i;
                tasks[j + mainFieldHeight] = new Task(() => rowsAndColumnsToBeCalculated[j + mainFieldHeight] = new RowOrColumnToBeCalculated(false, j, conditionColumns[j].ToArray()));
                tasks[j + mainFieldHeight].Start();
            }
            Task.WaitAll(tasks);
            solutions = new List<Cell[,]>();
            // поиск решения
            RowOrColumnToBeCalculated.Calculate(rowsAndColumnsToBeCalculated);
            if (RowOrColumnToBeCalculated.IsMainFieldFilled) // если решение есть
                rowsAndColumnsToBeCalculated = null;
            else                                             // если решения нет, начинаем перебор
            {
                MessageToAdd = Textes.severalSolutions[Textes.currentLang] + "\n";
                TryEveryVariantIfNoSolusion();
                FormMain.SetSolusions(solutions);
                MessageToAdd = Textes.solusionsFound[Textes.currentLang] + solutions.Count + "\n";
            }
        }
        private void TryEveryVariantIfNoSolusion()
        {
            int indexOfFirstElement = RowOrColumnToBeCalculated.GetIndexOfRowOrColumnByNumberInQueue(rowsAndColumnsToBeCalculated, 0);
            int variantsCount = rowsAndColumnsToBeCalculated[indexOfFirstElement].VariantsCount;
            CloneOfStaticsOfRowOrColumnToBeCalculated clone = new CloneOfStaticsOfRowOrColumnToBeCalculated(rowsAndColumnsToBeCalculated, indexOfFirstElement);

            for (int i = 0; i < variantsCount; i++)
            {
                RowOrColumnToBeCalculated.SetStatics(rowsAndColumnsToBeCalculated, clone);
                rowsAndColumnsToBeCalculated[indexOfFirstElement].possibleSolutions = new List<bool[]>();
                rowsAndColumnsToBeCalculated[indexOfFirstElement].possibleSolutions.Add(clone.cloneRowOrColumnToBeCalculated[indexOfFirstElement].possibleSolutions[i]);
                rowsAndColumnsToBeCalculated[indexOfFirstElement].VariantsCount = 1;
                RowOrColumnToBeCalculated.Calculate(rowsAndColumnsToBeCalculated);
                if (RowOrColumnToBeCalculated.IsMainFieldFilled)
                {
                    solutions.Add(RowOrColumnToBeCalculated.CloneMainField());
                }
                else
                {
                    if (!RowOrColumnToBeCalculated.IsError)
                        TryEveryVariantIfNoSolusion();
                }
                for (int j = 0; j < rowsAndColumnsToBeCalculated.Length; j++)
                {
                    Task.WaitAll(rowsAndColumnsToBeCalculated[j].tasksForRemovingIrrelevantVariants.ToArray());
                }
            }
        }
    }
}