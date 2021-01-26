using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SolverJapaneseCrosswords
{
    partial class Field
    {
        private Cell[,] mainField;                       // "поле" для дальнейшего решения
        private int mainFieldHeight;                     // высота "поля", в котором в итоге будет решение задания
        private int mainFieldWidth;                      // ширина "поля", в котором в итоге будет решение задания
        private List<int>[] conditionRows;               // условие задания (строки)
        private List<int>[] conditionColumns;            // условие задания (столбцы)
        internal bool IsValidate { get; private set; }
        private string errorDescription = "";
        private string ErrorDescription
        {
            get { return errorDescription; }
            set { errorDescription = value; OnAddingError(ErrorDescription); }
        }
        internal delegate void AddingError(string message, int colorARGB = 0x78FF0000);
        internal static event AddingError OnAddingError;
        internal void ReadDataFromExcel(string[,] inputFromExcel)
        {
            // Чтение данных из Excel методами библиотеки OpenExcelLibrary (создана на .Net Framework, т.к. в .Net Core поддержка библиотеки Microsoft.Office.Interop.Excel нестабильна)
            if (inputFromExcel == null) return;
            IsValidate = true;
            int inputFromExcelRowsCount = 0;
            int inputFromExcelColumnsCount = 0;
            int maxConditionRowLength = 0;
            int maxConditionColumnLength = 0;
            SetInputFromExcelCounts(inputFromExcel, ref inputFromExcelRowsCount, ref inputFromExcelColumnsCount);
            SetMaxConditionLength(inputFromExcel, ref maxConditionRowLength, inputFromExcelRowsCount, inputFromExcelColumnsCount, true);
            SetMaxConditionLength(inputFromExcel, ref maxConditionColumnLength, inputFromExcelColumnsCount, inputFromExcelRowsCount, false);
            SetMainField(inputFromExcelRowsCount, inputFromExcelColumnsCount, maxConditionRowLength, maxConditionColumnLength);
            CheckIsMainZoneNull(inputFromExcel, inputFromExcelRowsCount, inputFromExcelColumnsCount, maxConditionRowLength, maxConditionColumnLength);
            CheckIsLeftTopZoneNull(inputFromExcel, maxConditionRowLength, maxConditionColumnLength);
            int sumRows = FillConditions(inputFromExcel, ref conditionRows, inputFromExcelRowsCount, maxConditionRowLength, maxConditionColumnLength, mainFieldHeight, true);
            int sumColumns = FillConditions(inputFromExcel, ref conditionColumns, inputFromExcelColumnsCount, maxConditionColumnLength, maxConditionRowLength, mainFieldWidth, false);
            SetStaticsIfValidated(sumRows, sumColumns, maxConditionRowLength, maxConditionColumnLength);
        }
        internal void ReadDataFromManualBlock(int _mainFieldWidth, int _mainFieldHeigth, int _maxLengthLeft, int _maxLengthTop, DataGridView DGVleft, DataGridView DGVtop)
        {
            // чтение и проверка данных из DataGridView, введенных пользователем
            IsValidate = true;
            SetMainField(_mainFieldHeigth + _maxLengthTop, _mainFieldWidth + _maxLengthLeft, _maxLengthLeft, _maxLengthTop);
            conditionRows = new List<int>[_mainFieldHeigth];
            conditionColumns = new List<int>[_mainFieldWidth];
            int sumRows = FillConditionsFromManualBlock(conditionRows, DGVleft, true);
            int sumColumns = FillConditionsFromManualBlock(conditionColumns, DGVtop, false);
            SetStaticsIfValidated(sumRows, sumColumns, _maxLengthLeft, _maxLengthTop);
        }
        private void SetStaticsIfValidated(int sumRows, int sumColumns, int maxConditionRowLength, int maxConditionColumnLength)
        {
            if (sumRows != sumColumns) AddError(Textes.errorConditionSumDifferent[Textes.currentLang]);
            else
            {
                if (IsValidate)
                {
                    RowOrColumnToBeCalculated.SetStatics(mainField, mainFieldHeight, mainFieldWidth, sumRows);
                    Cell.UpdateMainFieldSizes(mainFieldWidth, mainFieldHeight, maxConditionRowLength, maxConditionColumnLength);
                    Cell.SetupDGVs(conditionRows, conditionColumns);
                }
            }
        }
        private int FillConditionsFromManualBlock(List<int>[] condition, DataGridView DGV, bool isRow)
        {
            // заполнение + проверка conditionRows и conditionColumns по введенным пользователем данным из DataGridView
            int sum = 0;
            int iTo = isRow ? DGV.RowCount : DGV.ColumnCount;
            int jTo = isRow ? DGV.ColumnCount : DGV.RowCount;
            for (int i = 0; i < iTo; i++)
            {
                condition[i] = new List<int>();
                for (int j = 0; j < jTo; j++)
                {
                    int cellValue;
                    (int, int) link = isRow ? (j, i) : (i, j);
                    if (DGV[link.Item1, link.Item2].Value != null)
                        if (int.TryParse(DGV[link.Item1, link.Item2].Value.ToString(), out cellValue))
                        {
                            if (cellValue > 0)
                            {
                                condition[i].Add(cellValue);
                                sum += cellValue;
                            }
                            else
                            {
                                AddError(Textes.errorNotPositiveValue[Textes.currentLang] + $"{i + 1} {j + 1} { (isRow ? Textes.leftCondition[Textes.currentLang] : Textes.topCondition[Textes.currentLang])}");
                            }
                        }
                        else
                        {
                            AddError(Textes.errorConditionGet[Textes.currentLang] + $"{i + 1} {j + 1} { (isRow ? Textes.leftCondition[Textes.currentLang] : Textes.topCondition[Textes.currentLang])}");
                        }
                }
                CheckCondition(condition[i], isRow, i + 1, isRow ? Textes.errorConditionIsTooBigInRow[Textes.currentLang] : Textes.errorConditionIsTooBigInColumn[Textes.currentLang], false);
            }
            return sum;
        }
        private void SetInputFromExcelCounts(string[,] inputFromExcel, ref int inputFromExcelRowsCount, ref int inputFromExcelColumnsCount)
        {
            inputFromExcelRowsCount = inputFromExcel.GetUpperBound(0) + 1;
            inputFromExcelColumnsCount = inputFromExcel.Length / inputFromExcelRowsCount;
        }
        private void SetMaxConditionLength(string[,] inputFromExcel, ref int maxConditionLength, int inputFromExcelConditionCount, int inputFromExcelOppositeConditionCount, bool isRow)
        {
            maxConditionLength = 0;
            (int, int) links;
            if (isRow) links = (inputFromExcelConditionCount - 1, 0);
            else links = (0, inputFromExcelConditionCount - 1);
            for (int i = inputFromExcelOppositeConditionCount - 1; i >= 0; i--)
            {
                if (isRow) links.Item2 = i;
                else links.Item1 = i;
                if (!(inputFromExcel[links.Item1, links.Item2] == null))
                {
                    maxConditionLength = i + 1;
                    break;
                }
            }
            if (maxConditionLength == 0 || maxConditionLength == inputFromExcelConditionCount) AddError(Textes.errorSetMaximumConditionRowLength[Textes.currentLang]);
        }
        private void SetMainField(int inputFromExcelRowsCount, int inputFromExcelColumnsCount, int maxConditionRowLength, int maxConditionColumnLength)
        {
            if (!IsValidate) return;
            mainFieldHeight = inputFromExcelRowsCount - maxConditionColumnLength;
            mainFieldWidth = inputFromExcelColumnsCount - maxConditionRowLength;
            mainField = new Cell[mainFieldHeight, mainFieldWidth];
            for (int i = 0; i < mainFieldHeight; i++)
                for (int j = 0; j < mainFieldWidth; j++)
                    mainField[i, j] = new Cell();
        }
        private void CheckIsMainZoneNull(string[,] inputFromExcel, int inputFromExcelRowsCount, int inputFromExcelColumnsCount, int maxConditionRowLength, int maxConditionColumnLength)
        {// проверка на пустоты диапазона, где будет решение
            if (!(maxConditionColumnLength == 0 || maxConditionRowLength == 0))
            {
                for (int i = maxConditionColumnLength; i < inputFromExcelRowsCount - 1; i++)
                    for (int j = maxConditionRowLength; j < inputFromExcelColumnsCount - 1; j++)
                        if (inputFromExcel[i, j] != null) AddError(Textes.errorInMainZone[Textes.currentLang] + ParseIntToColumn(j + 1) + (i + 1));
            }
            else AddError(Textes.errorEmptySheet[Textes.currentLang]);
        }
        private void CheckIsLeftTopZoneNull(string[,] inputFromExcel, int maxConditionRowLength, int maxConditionColumnLength)
        {
            if (!(maxConditionColumnLength == 0 || maxConditionRowLength == 0))
                for (int i = 0; i < maxConditionColumnLength; i++)
                    for (int j = 0; j < maxConditionRowLength; j++)
                        if (inputFromExcel[i, j] != null) AddError(Textes.errorInLeftTopZone[Textes.currentLang] + ParseIntToColumn(j + 1) + (i + 1));
        }
        private int FillConditions(string[,] inputFromExcel, ref List<int>[] condition, int inputFromExcelConditionCount, int maxConditionLength, int maxOppositeConditionLength, int mainFieldOppositeDimension, bool isRow)
        {// заполнение условий задания из файла
            if (!IsValidate) return 0;
            string errorText;
            int sumCondition = 0;
            if (isRow) errorText = Textes.errorConditionIsTooBigInRow[Textes.currentLang];
            else errorText = Textes.errorConditionIsTooBigInColumn[Textes.currentLang];
            condition = new List<int>[mainFieldOppositeDimension];
            (int, int) links;
            for (int i = maxOppositeConditionLength; i < inputFromExcelConditionCount; i++)
            {
                condition[i - maxOppositeConditionLength] = new List<int>();
                for (int j = 0; j < maxConditionLength; j++)
                {
                    if (isRow) links = (i, j);
                    else links = (j, i);
                    if (inputFromExcel[links.Item1, links.Item2] != null)
                        try
                        {
                            condition[i - maxOppositeConditionLength].Add(int.Parse(inputFromExcel[links.Item1, links.Item2]));
                            sumCondition += condition[i - maxOppositeConditionLength][condition[i - maxOppositeConditionLength].Count - 1];
                            if (condition[i - maxOppositeConditionLength][condition[i - maxOppositeConditionLength].Count - 1] < 1)
                                AddError(Textes.errorNotPositiveValue[Textes.currentLang] + ParseIntToColumn(links.Item2 + 1) + (links.Item1 + 1));
                        }
                        catch { AddError(Textes.errorConditionGet[Textes.currentLang] + ParseIntToColumn(links.Item2 + 1) + (links.Item1 + 1)); }
                }
                CheckCondition(condition[i - maxOppositeConditionLength], isRow, i + 1, errorText, true);
            }
            return sumCondition;
        }
        private void CheckCondition(List<int> conditionList, bool isRow, int pointer, string errorText, bool isParseIntToColumn)
        {
            // проверка строк и колонок условий на пустоту и превышение размеров таблицы, в которой будет решение
            int possibleMaximum = isRow ? mainFieldWidth : mainFieldHeight;
            int sumOfConditionList = conditionList.Sum() + conditionList.Count - 1;
            if (sumOfConditionList > possibleMaximum)
            {
                if (isRow) AddError(errorText + pointer);
                else AddError(errorText + (isParseIntToColumn ? ParseIntToColumn(pointer) : pointer.ToString()));
            }
            if (conditionList.Count == 0) AddError(Textes.emptyCondition[Textes.currentLang]);
        }
        private string ParseIntToColumn(int i)
        {// преобразование номера колонки в буквенное значение Excel
            string str;
            str = (i < 27) ? ($"{(char)('A' + (i - 1) % 26)}") : (i < 703) ? ($"{(char)('A' + (i - 27) / 26)}{(char)('A' + (i - 1) % 26)}") : ($"{(char)('A' + (i - 703) / 676)}{(char)('A' + (i - 703) % 676 / 26)}{(char)('A' + (i - 1) % 26)}");
            return str;
        }
        private void AddError(string errorText)
        {
            IsValidate = false;
            ErrorDescription = errorText + "\n";
        }
    }
}