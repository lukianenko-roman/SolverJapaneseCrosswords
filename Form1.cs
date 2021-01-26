using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SolverJapaneseCrosswords
{
    public partial class FormMain : Form
    {
        private Field field;
        private static List<Cell[,]> solutions;
        private static int currentSolution;
        readonly int numericUpDownHeightMainZoneStartValue;
        readonly int numericUpDownWidthMainZoneStartValue;
        readonly int numericUpDownMaxCountLeftStartValue;
        readonly int numericUpDownMaxCountTopStartValue;
        public FormMain()
        {
            InitializeComponent();
            // Присваивания изначальных значений для ручного ввода (необходимо для корректного сброса)
            numericUpDownHeightMainZoneStartValue = (int)numericUpDownHeightMainZone.Value;
            numericUpDownWidthMainZoneStartValue = (int)numericUpDownWidthMainZone.Value;
            numericUpDownMaxCountLeftStartValue = (int)numericUpDownMaxCountLeft.Value;
            numericUpDownMaxCountTopStartValue = (int)numericUpDownMaxCountTop.Value;
            // Привязка элементов формы и изначальных параметров классу, в котором будут изменяться эти элементы
            Cell.ConnectMainFormControls(dataGridViewLeft, dataGridViewTop, dataGridViewMain, this, progressBar, labelProgress);
            Cell.UpdateMainFormSizes(Width, Height);
            // Выбор стартовых параметров
            radioButtonEN.Checked = true;
            radioButtonInputExcel.Checked = true;
            checkBoxDelay.Checked = false;
            // Привязка событий к обновлению ActionLog
            ExcelInput.OnAddingMessage += UpdateActionLogTextBox;
            Field.OnAddingError += UpdateActionLogTextBox;
            Field.OnAddingMessage += UpdateActionLogTextBox;
            currentSolution = 0;
        }
        private void ButtonOpenExcel_Click(object sender, EventArgs e)
        {
            // Выбор файла (при необходимости - вкладки) для чтения и проверки данных. Если проверка пройдена - доступен расчет; в противном случае - ожидание корректного ввода/изменения метода ввода
            buttonOpenExcel.Enabled = false;
            radioButtonInputExcel.Enabled = false;
            radioButtonInputManual.Enabled = false;
            field = new Field();
            field.ReadDataFromExcel(ExcelInput.OpenExcelFile(Textes.chooseExcelFileText[Textes.currentLang]));
            if (field.IsValidate)
            {
                UpdateActionLogTextBox(Textes.fileLoaded[Textes.currentLang] + "\n", 0x7800FF00);
                buttonReset.Enabled = true;
                buttonCalculate.Enabled = true;
            }
            else
            {
                UpdateActionLogTextBox(Textes.errorSummaryFileLoad[Textes.currentLang] + "\n", 0x78FF0000);
                ButtonReset_Click(null, null);
                return;
            }
        }
        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            // Проверка введенных пользователем данных на корректность. Если данные корректны, доступен старт расчета, если нет - ничего не меняется, ожидание корректных данных/смены режима ввода
            buttonCheck.Enabled = false;
            radioButtonInputExcel.Enabled = false;
            radioButtonInputManual.Enabled = false;
            buttonReset.Enabled = false;
            Cell.SetCurrentCellAsNull();
            field = new Field();
            field.ReadDataFromManualBlock((int)numericUpDownWidthMainZone.Value, (int)numericUpDownHeightMainZone.Value,
                (int)numericUpDownMaxCountLeft.Value, (int)numericUpDownMaxCountTop.Value,
                dataGridViewLeft, dataGridViewTop);
            if (field.IsValidate)
            {
                UpdateActionLogTextBox(Textes.manualDataLoaded[Textes.currentLang] + "\n", 0x7800FF00);
                Cell.SetLeftAndTopDGVsEnable(false);
                panelManualInput.Enabled = false;
                buttonReset.Enabled = true;
                buttonCalculate.Enabled = true;
            }
            else
            {
                UpdateActionLogTextBox(Textes.errorSummaryManualLoad[Textes.currentLang] + "\n", 0x78FF0000);
                field = null;
                buttonCheck.Enabled = true;
                radioButtonInputExcel.Enabled = true;
                radioButtonInputManual.Enabled = true;
                buttonReset.Enabled = true;
            }
        }
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            // Сброс всех считанных/введенных данных. Вызывается либо кнопкой Reset, либо при наличии ошибок во время чтения из файла, либо при изменении метода ввода
            if (sender != null)
            {
                if (MessageBox.Show(Textes.resetMessage[Textes.currentLang], "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                UpdateActionLogTextBox(Textes.buttonResetOnClick[Textes.currentLang] + "\n", 0x78FF0000);
            }
            buttonReset.Enabled = false;
            buttonSave.Enabled = false;
            field = null;
            Cell.ClearControls(dataGridViewLeft, dataGridViewTop, dataGridViewMain);
            if (radioButtonInputExcel.Checked)
                buttonOpenExcel.Enabled = true;
            else
            {
                SetDefaultValuesNumericUpDowns();
                Cell.ManualSetupDGVs(true, (int)numericUpDownWidthMainZone.Value, (int)numericUpDownHeightMainZone.Value, (int)numericUpDownMaxCountLeft.Value, (int)numericUpDownMaxCountTop.Value);
                buttonReset.Enabled = true;
                buttonCheck.Enabled = true;
            }
            buttonCalculate.Enabled = false;
            radioButtonInputExcel.Enabled = true;
            radioButtonInputManual.Enabled = true;
            labelProgress.Text = null;
            solutions = null;
            currentSolution = 0;
            buttonNextSolution.Visible = false;
            buttonPrevSolution.Visible = false;
            GC.Collect();
        }
        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            // Вызов методов расчета решения, выключение/включение соответствующих кнопок.
            DateTime startTime = DateTime.Now;
            buttonCalculate.Enabled = false;
            buttonReset.Enabled = false;
            field.SolveCrossword();
            buttonReset.Enabled = true;
            buttonSave.Enabled = true;
            TimeSpan timeOfCalculation = DateTime.Now - startTime;
            UpdateActionLogTextBox(Textes.calculated[Textes.currentLang] + Math.Round(timeOfCalculation.TotalSeconds, 3).ToString() + "\n", 0x7800FF00);
            if (solutions != null && solutions.Count > 1)
            {
                Cell.ShowCurrentSolution(solutions[0]);
                buttonPrevSolution.Visible = true;
                buttonNextSolution.Visible = true;
                UpdateActionLogTextBox(Textes.navigation[Textes.currentLang] + "\n", 0x7800FF00);
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Сохранение результата расчета как изображение
            Cell.SaveAsPicture(panelWithDGVs);
        }
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            // Изменение координат и размеров элементов формы, которые необходимо менять при изменении размеров формы (нижние элементы формы и все DataGridView)
            // При изменении изначального расположения элементов на форму необходима корректировка
            richTextBoxActionLog.Width = this.Width - 40;
            richTextBoxActionLog.Location = new Point(richTextBoxActionLog.Location.X, this.Height - 136);
            checkBoxActionLog.Location = new Point(checkBoxActionLog.Location.X, this.Height - 161);
            checkBoxDelay.Location = new Point(checkBoxDelay.Location.X, this.Height - 161);
            numericUpDownDelay.Location = new Point(numericUpDownDelay.Location.X, this.Height - 161);
            progressBar.Location = new Point(progressBar.Location.X, this.Height - 161);
            progressBar.Width = this.Width - 516;
            labelProgress.Location = new Point(labelProgress.Location.X, progressBar.Location.Y + 10);
            panelWithDGVs.Width = Width - 208;
            panelWithDGVs.Height = Height - 179;
            buttonNextSolution.Location = new Point(buttonNextSolution.Location.X, this.Height - 161);
            buttonPrevSolution.Location = new Point(buttonPrevSolution.Location.X, this.Height - 161);
            //if (!Cell.isDGVsSeted()) Cell.ConnectMainFormControls(dataGridViewLeft, dataGridViewTop, dataGridViewMain, this, progressBar, labelProgress);
            Cell.UpdateMainFormSizes(Width, Height);
            Invalidate();
        }
        private void RadioButtonLang_CheckedChanged(object sender, EventArgs e)
        {
            // Изменение языка. Подробнее в комментариях к методу ChangeLang
            RadioButton rb = (RadioButton)sender;
            ChangeLang(rb.Text);
        }
        private void RadioButtonInput_CheckedChanged(object sender, EventArgs e)
        {
            // Открытие/сокрытие соответствующих зон на форме при изменении варианта ввода (Excel или ручной).
            RadioButton rb = (RadioButton)sender;
            if (rb == radioButtonInputExcel)
            {
                SetButtonsAndPanelsEnable(true);
                Cell.SetVisibleDGV(false, dataGridViewLeft, dataGridViewTop, dataGridViewMain);
                Cell.SetLeftAndTopDGVsEnable(false);
                Cell.ClearControls(dataGridViewLeft, dataGridViewTop);
            }
            else
            {
                buttonCheck.Enabled = true;
                SetButtonsAndPanelsEnable(false);
                Cell.ManualSetupDGVs(true, (int)numericUpDownWidthMainZone.Value, (int)numericUpDownHeightMainZone.Value, (int)numericUpDownMaxCountLeft.Value, (int)numericUpDownMaxCountTop.Value);
            }
            if (field != null)
            {
                ButtonReset_Click(null, null);
            }
        }
        private void CheckBoxDelay_CheckedChanged(object sender, EventArgs e)
        {
            // Если неактивен - шаг обновления 0, вывод по мере решения; в противном случае - поочередный вывод согласно значения NumericUpDownDelay
            CheckBox checkBox = (CheckBox)sender;
            numericUpDownDelay.Enabled = checkBox.Checked;
            Cell.SetDelay(checkBox.Checked ? (int)numericUpDownDelay.Value : 0);
        }
        private void NumericUpDownDelay_ValueChanged(object sender, EventArgs e)
        {
            // Установка шага обновления (для поочередного вывода расчитанных значений ячеек на экран)
            Cell.SetDelay((int)numericUpDownDelay.Value);
        }
        private void NumericUpDownHeightMainZone_ValueChanged(object sender, EventArgs e)
        {
            // Добавление/удаление строк в DataGridView с решением и условием слева.
            numericUpDownHeightMainZone.Value = (int)numericUpDownHeightMainZone.Value;
            Cell.ManualSetupDGVs(false, _mainFieldHeigth: (int)numericUpDownHeightMainZone.Value);
        }
        private void NumericUpDownWidthMainZone_ValueChanged(object sender, EventArgs e)
        {
            // Добавление/удаление колонок в DataGridView с решением и условием сверху.
            numericUpDownWidthMainZone.Value = (int)numericUpDownWidthMainZone.Value;
            Cell.ManualSetupDGVs(false, _mainFieldWidth: (int)numericUpDownWidthMainZone.Value);
        }
        private void NumericUpDownMaxCountLeft_ValueChanged(object sender, EventArgs e)
        {
            // Добавление/удаление колонок в DataGridView с условияем слева.
            numericUpDownMaxCountLeft.Value = (int)numericUpDownMaxCountLeft.Value;
            Cell.ManualSetupDGVs(false, _MaxLengthLeft: (int)numericUpDownMaxCountLeft.Value);
        }
        private void NumericUpDownMaxCountTop_ValueChanged(object sender, EventArgs e)
        {
            // Добавление/удаление строк в DataGridView с условием сверху.
            numericUpDownMaxCountTop.Value = (int)numericUpDownMaxCountTop.Value;
            Cell.ManualSetupDGVs(false, _MaxLengthTop: (int)numericUpDownMaxCountTop.Value);
        }
        private void DataGridViewMain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Добавление "разделителей" через каждый 5 строк или колонок на DataGridView с решением
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            using (Pen p = new Pen(Color.Black, 1))
            {
                if (e.RowIndex % 5 == 0 && e.RowIndex != 0)
                    e.Graphics.DrawLine(p, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Height, e.CellBounds.Y);
                if (e.ColumnIndex % 5 == 0 && e.ColumnIndex != 0)
                    e.Graphics.DrawLine(p, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height);
                e.Handled = true;
            }
        }
        private void DataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Изменение размера шрифта после ручного ввода при необходимости (применимо к DataGridView с условием - левый и верхний).
            Cell.SetFontSizeForDGVs(e.RowIndex, e.ColumnIndex, (DataGridView)sender);
        }
        private void SetDefaultValuesNumericUpDowns()
        {
            // Вызывается при сбросе.
            numericUpDownHeightMainZone.Value = numericUpDownHeightMainZoneStartValue;
            numericUpDownWidthMainZone.Value = numericUpDownWidthMainZoneStartValue;
            numericUpDownMaxCountLeft.Value = numericUpDownMaxCountLeftStartValue;
            numericUpDownMaxCountTop.Value = numericUpDownMaxCountTopStartValue;
            panelManualInput.Enabled = true;
        }
        private void SetButtonsAndPanelsEnable(bool isTrue)
        {
            // Вызывается при изменении выбора ввода (Excel или ручной), соответственно разблокируются и блокируются необходимые элементы управления
            buttonOpenExcel.Enabled = isTrue;
            buttonReset.Enabled = !isTrue;
            panelManualInput.Enabled = !isTrue;
        }
        private void UpdateActionLogTextBox(string message, int colorARGB = 0x78000000)
        {
            // 0x780000FF - синий, 0x7800FF00 - зеленый, 0x78FF0000 - красный, 0x78000000 - черный
            // Обновление ActionLog. Вызывается напрямую либо через подвязанные события из других классов
            if (checkBoxActionLog.Checked)
            {
                richTextBoxActionLog.AppendText(message);
                richTextBoxActionLog.Select(richTextBoxActionLog.Text.Length - message.Length, message.Length);
                richTextBoxActionLog.SelectionColor = Color.FromArgb(colorARGB);
                richTextBoxActionLog.ScrollToCaret();
            }
        }
        private void ChangeLang(string lang)
        {
            // Изменение языка на всех элементах формы. При добавлении элемента с текстом на форму необходимо добавить этот элемент в данный метод по аналогии с другими элементами.
            // Структура Textes также должна быть обновлена при добавлении нового элемента (новый элемент Dictinary, на который будет ссылаться текст элемента.
            // При добавлении нового языка на форму необходимо добавить новый RadioButton на panelLang и обновить всю структуру Textes (ключ должен совпадать с текстом на новом RadioButton.
            Textes.currentLang = lang;
            this.Text = Textes.formMainText[lang];
            buttonOpenExcel.Text = Textes.buttonOpenExcelText[lang];
            checkBoxActionLog.Text = Textes.checkBoxActionLog[lang];
            labelInput.Text = Textes.labelInput[lang];
            radioButtonInputManual.Text = Textes.radioButtonManual[lang];
            labelHeightMainZone.Text = Textes.labelHeightMainZone[lang];
            labelWidthMainZone.Text = Textes.labelWidthMainZone[lang];
            labelLengthConditionLeft.Text = Textes.labelLengthConditionLeft[lang];
            labelLengthConditionTop.Text = Textes.labelLengthConditionTop[lang];
            buttonCheck.Text = Textes.buttonCheck[lang];
            buttonReset.Text = Textes.buttonReset[lang];
            buttonCalculate.Text = Textes.buttonCalculate[lang];
            buttonSave.Text = Textes.saveAsPicture[lang];
            checkBoxDelay.Text = Textes.stepByStep[lang];
        }
        internal static void SetSolusions(List<Cell[,]> _solutions)
        {
            // вызывается в случае нескольких решений
            solutions = _solutions;
        }
        private void buttonPrevSolution_Click(object sender, EventArgs e)
        {
            currentSolution = currentSolution == 0 ? solutions.Count - 1 : currentSolution - 1;
            Cell.ShowCurrentSolution(solutions[currentSolution]);
        }
        private void buttonNextSolusion_Click(object sender, EventArgs e)
        {
            currentSolution = currentSolution == solutions.Count - 1 ? 0 : currentSolution + 1;
            Cell.ShowCurrentSolution(solutions[currentSolution]);
        }
    }
}