using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolverJapaneseCrosswords
{
    internal struct Textes
    {
        // Структура для изменения языка на форме. Подробнее - в комментариях метода ChangeLang (Form1.cs)
        internal static string currentLang;
        internal static readonly Dictionary<string, string> formMainText = new Dictionary<string, string> { ["EN"] = "Japanese Crosswords Solver", ["RU"] = "Решалка японских кроссвордов" };
        internal static readonly Dictionary<string, string> buttonOpenExcelText = new Dictionary<string, string> { ["EN"] = "Open Excel File", ["RU"] = "Открыть Excel файл" };
        internal static readonly Dictionary<string, string> checkBoxActionLog = new Dictionary<string, string> { ["EN"] = "Action Log:", ["RU"] = "Журнал событый:" };
        internal static readonly Dictionary<string, string> labelInput = new Dictionary<string, string> { ["EN"] = "Input option:", ["RU"] = "Вариант ввода:" };
        internal static readonly Dictionary<string, string> radioButtonManual = new Dictionary<string, string> { ["EN"] = "Manual", ["RU"] = "Вручную" };
        internal static readonly Dictionary<string, string> labelHeightMainZone = new Dictionary<string, string> { ["EN"] = "Height of field", ["RU"] = "Высота поля" };
        internal static readonly Dictionary<string, string> labelWidthMainZone = new Dictionary<string, string> { ["EN"] = "Width of field", ["RU"] = "Ширина поля" };
        internal static readonly Dictionary<string, string> labelLengthConditionLeft = new Dictionary<string, string> { ["EN"] = "Max count of\nelements in\ncondition(left)", ["RU"] = "Макс кол-во\nэлементов\nусловия (слева)" };
        internal static readonly Dictionary<string, string> labelLengthConditionTop = new Dictionary<string, string> { ["EN"] = "Max count of\nelements in\ncondition(top)", ["RU"] = "Макс кол-во\nэлементов\nусловия (сверху)" };
        internal static readonly Dictionary<string, string> buttonCheck = new Dictionary<string, string> { ["EN"] = "Check", ["RU"] = "Проверить" };
        internal static readonly Dictionary<string, string> buttonReset = new Dictionary<string, string> { ["EN"] = "Reset", ["RU"] = "Сброс" };
        internal static readonly Dictionary<string, string> buttonCalculate = new Dictionary<string, string> { ["EN"] = "Calculate", ["RU"] = "Расчет" };
        internal static readonly Dictionary<string, string> resetMessage = new Dictionary<string, string> { ["EN"] = "Are you sure? Loaded data will be lost.", ["RU"] = "Вы уверены? Загруженные данные будут потеряны." };
        internal static readonly Dictionary<string, string> buttonResetOnClick = new Dictionary<string, string> { ["EN"] = "Data has been reseted, waiting for new input", ["RU"] = "Произошел сброс данных, ожидание входных данных" };
        internal static readonly Dictionary<string, string> labelFormChooseWS = new Dictionary<string, string> { ["EN"] = "Please select needed worksheet and press OK", ["RU"] = "Выберите нужный лист и нажмите OK" };
        internal static readonly Dictionary<string, string> chooseExcelFileText = new Dictionary<string, string> { ["EN"] = "Select Excel File", ["RU"] = "Выберите документ Excel" };
        internal static readonly Dictionary<string, string> openingFile = new Dictionary<string, string> { ["EN"] = "Opening file ", ["RU"] = "Открытие файла " };
        internal static readonly Dictionary<string, string> reedingFromSheet = new Dictionary<string, string> { ["EN"] = "Reed data from worksheet ", ["RU"] = "Считывание данных с листа " };
        internal static readonly Dictionary<string, string> reedingError = new Dictionary<string, string> { ["EN"] = "Error while file reading (check is Excel installed and are selected book or worksheet have no internal mistakes)", ["RU"] = "Ошибка во время обработки файла (проверьте, установлен ли Excel и нет ли внутренних ошибок в выбранных файле и листе" };
        internal static readonly Dictionary<string, string> topCondition = new Dictionary<string, string> { ["EN"] = "of the top condition", ["RU"] = "верхнего условия" };
        internal static readonly Dictionary<string, string> leftCondition = new Dictionary<string, string> { ["EN"] = "of the left condition", ["RU"] = "левого условия" };
        internal static readonly Dictionary<string, string> errorEmptySheet = new Dictionary<string, string> { ["EN"] = "Mistake: looks like empty sheet has been selected.", ["RU"] = "Ошибка: похоже, был выбран пустой лист." };
        internal static readonly Dictionary<string, string> errorSetMaximumConditionRowLength = new Dictionary<string, string> { ["EN"] = "Mistake has been found in last row in selected file. Please check is file valid to requirements.", ["RU"] = "Найдена ошибка в последней строке выбранного файла. Пожалуйста, проверьте, соответствует ли файл требованиям." };
        internal static readonly Dictionary<string, string> errorSetMaximumConditionColumnLength = new Dictionary<string, string> { ["EN"] = "Mistake has been found in last column in selected file. Please check is file valid to requirements.", ["RU"] = "Найдена ошибка в последней колонке выбранного файла. Пожалуйста, проверьте, соответствует ли файл требованиям." };
        internal static readonly Dictionary<string, string> errorInMainZone = new Dictionary<string, string> { ["EN"] = "Mistake: not empty cell ", ["RU"] = "Ошибка: непустая ячейка " };
        internal static readonly Dictionary<string, string> errorInLeftTopZone = new Dictionary<string, string> { ["EN"] = "Mistake: left top part of worksheet should be empty. Not empty cell ", ["RU"] = "Ошибка: левая верхняя часть листа должна быть пустая. Непустая ячейка " };
        internal static readonly Dictionary<string, string> errorConditionGet = new Dictionary<string, string> { ["EN"] = "Mistake: not an integer value in cell ", ["RU"] = "Ошибка: не целочисленное значение в ячейке " };
        internal static readonly Dictionary<string, string> errorConditionIsTooBigInRow = new Dictionary<string, string> { ["EN"] = "Mistake: incorrect condition (sum in the row more than field size), row ", ["RU"] = "Ошибка: некорректное условие (сумма чисел в строке больше размеров поля), строка " };
        internal static readonly Dictionary<string, string> errorConditionIsTooBigInColumn = new Dictionary<string, string> { ["EN"] = "Mistake: incorrect condition (sum in the column more than field size), column ", ["RU"] = "Ошибка: некорректное условие (сумма чисел в строке больше размеров поля), столбец " };
        internal static readonly Dictionary<string, string> errorConditionSumDifferent = new Dictionary<string, string> { ["EN"] = "Mistake: the sum of the numbers in the columns is different from the sum in the rows", ["RU"] = "Ошибка: сумма чисел в столбцах отличается от суммы в строках" };
        internal static readonly Dictionary<string, string> errorNotPositiveValue = new Dictionary<string, string> { ["EN"] = "Mistake: not positive value in cell ", ["RU"] = "Ошибка: не положительное число в ячейке " };
        internal static readonly Dictionary<string, string> errorSummaryFileLoad = new Dictionary<string, string> { ["EN"] = "File has mistakes, please fix due to requirements and repeat loading", ["RU"] = "В файле ошибки, обновите файл согласно требованиям заполнения и повторите загрузку" };
        internal static readonly Dictionary<string, string> errorSummaryManualLoad = new Dictionary<string, string> { ["EN"] = "Mistakes have been found, please fix due to requirements and repeat", ["RU"] = "Обнаружены ошибки, обновите данные согласно требованиям заполнения и повторите загрузку" };
        internal static readonly Dictionary<string, string> fileLoaded = new Dictionary<string, string> { ["EN"] = "File has been loaded, waiting for calculation start", ["RU"] = "Файл загружен, ожидание начала расчета" };
        internal static readonly Dictionary<string, string> manualDataLoaded = new Dictionary<string, string> { ["EN"] = "Data has been loaded, waiting for calculation start", ["RU"] = "Данные загружены, ожидание начала расчета" };
        internal static readonly Dictionary<string, string> calculated = new Dictionary<string, string> { ["EN"] = "Calculated for (sec): ", ["RU"] = "Рассчитано за (сек): " };
        internal static readonly Dictionary<string, string> saveAsPicture = new Dictionary<string, string> { ["EN"] = "Save as picture", ["RU"] = "Сохранить изображение" };
        internal static readonly Dictionary<string, string> stepByStep = new Dictionary<string, string> { ["EN"] = "Step by step with delay(ms):", ["RU"] = "Расчет с задержкой:" };
        internal static readonly Dictionary<string, string> emptyCondition = new Dictionary<string, string> { ["EN"] = "Empty condition in row/column is not valid", ["RU"] = "Пустое условие в строке/колонке не допускается" };
        internal static readonly Dictionary<string, string> severalSolutions = new Dictionary<string, string> { ["EN"] = "Looks like there are several solutions or no solution, starting to look over all the variants", ["RU"] = "Имеется несколько решений или решение отсутствует, старт перебора всех вариантов" };
        internal static readonly Dictionary<string, string> solusionsFound = new Dictionary<string, string> { ["EN"] = "Quantity of solusions: ", ["RU"] = "Количество решений: " };
        internal static readonly Dictionary<string, string> navigation = new Dictionary<string, string> { ["EN"] = "You can see every solution via buttons (near field with display delay value setting) press", ["RU"] = "Можно просмотреть все варианты решения переключение кнопок (возле поля для установки значения задержки отображения)" };
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
