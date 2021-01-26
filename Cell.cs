using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolverJapaneseCrosswords
{
    internal enum StatesOfCell
    {
        unknown,
        empty,
        full
    }
    class Cell
    {
        private static int Size { get; set; }               // размер ячейки в пикселях для отображения на форме
        private static float SizeFloat { get; set; }
        private static int MaxLengthLeft { get; set; }      // максимальная длина условия слева
        private static int MaxLengthTop { get; set; }       // максимальная длина условия сверху
        private static int MainFormWidth { get; set; }      // ширина формы
        private static int MainFormHeigth { get; set; }     // высота формы
        private static int MainFieldWidth { get; set; }     // ширина поля для решения
        private static int MainFieldHeigth { get; set; }    // высота поля для решения
        private static DataGridView DGVleft { get; set; }   // элементы формы
        private static DataGridView DGVtop { get; set; }
        private static DataGridView DGVmain { get; set; }
        private static FormMain MainForm { get; set; }
        private static ProgressBar progressBar { get; set; }
        private static Label labelProgress { get; set; }
        private static int Delay { get; set; }              // время задержки для пошагового отображения результата расчета
        private const int maxSize = 50;                     // максимальный размер ячейки
        private const int minSize = 8;                      // минимальный размер ячейки
        private static List<Task> tasks;                    // список задач для отображения результата расчета

        internal StatesOfCell State { set; get; }
        internal Cell()
        {
            State = StatesOfCell.unknown;
        }
        internal static bool isDGVsSeted()
        {
            if (DGVleft == null)
                return false;
            else
                return true;
        }
        internal static void UpdateMainFieldSizes(int _mainFieldWidth, int _mainFieldHeigth, int _MaxLengthLeft, int _MaxLengthTop)
        {
            // заполнение исходных данных, вызов после проверки условия на корректность
            MainFieldWidth = _mainFieldWidth;
            MainFieldHeigth = _mainFieldHeigth;
            MaxLengthLeft = _MaxLengthLeft;
            MaxLengthTop = _MaxLengthTop;
            progressBar.Maximum = MainFieldWidth * MainFieldHeigth;
            UpdateCellSize();
        }
        private static void UpdateCellSize()
        {
            // поиск максимального размера ячейки с зависимости от ширины и высоты формы, кол-ва строк/столбцов, макс и мин размера ячейки.
            try { SizeFloat = Math.Min(Math.Max(Math.Min(((MainFormWidth - 210.0f - MaxLengthLeft - MainFieldWidth) / (MaxLengthLeft + MainFieldWidth)), ((MainFormHeigth - 179 - MaxLengthTop - MainFieldHeigth) / (MaxLengthTop + MainFieldHeigth))), minSize), maxSize); }
            catch (DivideByZeroException) { SizeFloat = minSize; }
            finally { Size = (int)Math.Floor(SizeFloat); }
        }
        internal static void ConnectMainFormControls(DataGridView _DGVleft, DataGridView _DGVtop, DataGridView _DGVmain, FormMain _FormMain, ProgressBar _progressBar, Label _labelProgress)
        {
            // привязка элементов управления, вызывается при запуске программы
            DGVleft = _DGVleft;
            DGVtop = _DGVtop;
            DGVmain = _DGVmain;
            MainForm = _FormMain;
            progressBar = _progressBar;
            labelProgress = _labelProgress;
        }
        internal static void UpdateMainFormSizes(int mainFormWidth, int mainFormHeigth)
        {
            // вызывается при открытии программы и при изменении размеров формы
            SetVisibleDGV(false, DGVleft, DGVtop, DGVmain);
            MainFormWidth = mainFormWidth;
            MainFormHeigth = mainFormHeigth;
            UpdateCellSize();
            DGVsUpdateSizeAndLocation();
            SetFontSizeForDGVs(-1, -1, DGVleft, DGVtop);
            if (MainFieldWidth > 0)
                SetVisibleDGV(true, DGVleft, DGVtop, DGVmain);
        }
        internal static void SetupDGVs(List<int>[] conditionRows, List<int>[] conditionColumns)
        {
            // заполнение из Excel
            SetVisibleDGV(false, DGVleft, DGVtop, DGVmain);

            for (int i = 0; i < MaxLengthLeft; i++)
            {
                DGVleft.Columns.Add(i.ToString(), null);
                DGVleft.Columns[i].Width = Size;
            }
            DGVleft.Rows.Add(MainFieldHeigth);
            for (int i = 0; i < MainFieldHeigth; i++)
            {
                DGVleft.Rows[i].Height = Size;
            }
            for (int i = 0; i < MainFieldWidth; i++)
            {
                DGVtop.Columns.Add(i.ToString(), null);
                DGVtop.Columns[i].Width = Size;
                DGVmain.Columns.Add(i.ToString(), null);
                DGVmain.Columns[i].Width = Size;
            }
            DGVtop.Rows.Add(MaxLengthTop);
            for (int i = 0; i < MaxLengthTop; i++)
            {
                DGVtop.Rows[i].Height = Size;
            }
            DGVmain.Rows.Add(MainFieldHeigth);
            for (int i = 0; i < MainFieldHeigth; i++)
            {
                DGVmain.Rows[i].Height = Size;
            }
            DGVsUpdateSizeAndLocation();
            FillLeftAndTopDGVs(conditionRows, conditionColumns);
            SetFontSizeForDGVs(-1, -1, DGVleft, DGVtop);
            SetVisibleDGV(true, DGVleft, DGVtop, DGVmain);

            SetCurrentCellAsNull();
        }
        internal static void SetCurrentCellAsNull()
        {
            DGVleft.CurrentCell = null;
            DGVtop.CurrentCell = null;
            DGVmain.CurrentCell = null;
        }
        internal static void ManualSetupDGVs(bool isNeedToClearDGVs, int _mainFieldWidth = 0, int _mainFieldHeigth = 0, int _MaxLengthLeft = 0, int _MaxLengthTop = 0)
        {
            // вызов при изменении ширины/высоты поля или максимальной длины условий, при старте ручного ввода
            if (isNeedToClearDGVs) ClearControls(DGVleft, DGVtop, DGVmain);
            MainFieldWidth = _mainFieldWidth != 0 ? _mainFieldWidth : MainFieldWidth;
            MainFieldHeigth = _mainFieldHeigth != 0 ? _mainFieldHeigth : MainFieldHeigth;
            MaxLengthLeft = _MaxLengthLeft != 0 ? _MaxLengthLeft : MaxLengthLeft;
            MaxLengthTop = _MaxLengthTop != 0 ? _MaxLengthTop : MaxLengthTop;

            ManualSetupOneDGV(DGVleft, MaxLengthLeft, MainFieldHeigth, true, false);
            ManualSetupOneDGV(DGVtop, MainFieldWidth, MaxLengthTop, false, true);
            ManualSetupOneDGV(DGVmain, MainFieldWidth, MainFieldHeigth);

            UpdateCellSize();
            DGVsUpdateSizeAndLocation();
            SetOneSizeForAllRowsAndColumnsOnDGVs(DGVmain, DGVtop, DGVleft);
            SetLeftAndTopDGVsEnable(true);
            SetVisibleDGV(true, DGVleft, DGVtop, DGVmain);
            SetCurrentCellAsNull();
        }
        private static void ManualSetupOneDGV(DataGridView DGV, int columnsCount, int rowsCount, bool removeFirstColumn = true, bool removeFirstRow = true)
        {
            // добавление/удаление колонок/строк в отображаемых DataGridView
            if (DGV.Columns.Count != columnsCount)
            {
                while (DGV.Columns.Count > columnsCount)
                    DGV.Columns.RemoveAt(removeFirstColumn ? 0 : DGV.Columns.Count - 1);
                while (DGV.Columns.Count < columnsCount)
                    if (DGV.Columns.Count == 0)
                        DGV.Columns.Add(null, null);
                    else
                        DGV.Columns.Insert(removeFirstColumn ? 0 : DGV.Columns.Count, new DataGridViewTextBoxColumn());
            }
            if (DGV.Rows.Count != rowsCount)
            {
                while (DGV.Rows.Count > rowsCount)
                    DGV.Rows.RemoveAt(removeFirstRow ? 0 : DGV.Rows.Count - 1);
                while (DGV.Rows.Count < rowsCount)
                    if (DGV.Rows.Count == 0)
                        DGV.Rows.Add(rowsCount - DGV.Rows.Count);
                    else
                        DGV.Rows.Insert(removeFirstRow ? 0 : DGV.Rows.Count, new DataGridViewRow());
            }
        }
        internal static void SetLeftAndTopDGVsEnable(bool isEnable)
        {
            DGVleft.Enabled = isEnable;
            DGVtop.Enabled = isEnable;
        }
        internal static void ClearControls(params DataGridView[] DGVs)
        {
            foreach (DataGridView DGV in DGVs)
            {
                DGV.Rows.Clear();
                DGV.Columns.Clear();
            }
            progressBar.Value = 0;
            SetVisibleDGV(false, DGVs);
        }
        private static void FillLeftAndTopDGVs(List<int>[] conditionRows, List<int>[] conditionColumns)
        {
            for (int i = 0; i < MainFieldHeigth; i++)
                for (int j = conditionRows[i].Count - 1; j > -1; j--)
                    DGVleft[MaxLengthLeft - conditionRows[i].Count + j, i].Value = conditionRows[i][j];
            for (int i = 0; i < MainFieldWidth; i++)
                for (int j = conditionColumns[i].Count - 1; j > -1; j--)
                    DGVtop[i, MaxLengthTop - conditionColumns[i].Count + j].Value = conditionColumns[i][j];
        }
        internal static void SetFontSizeForDGVs(int rowPointer, int columnPointer, params DataGridView[] DGVs)
        {
            // изменение размера шрифта в ячейке при необходимости
            foreach (DataGridView DGV in DGVs)
            {
                int iFrom = rowPointer == -1 ? 0 : rowPointer;
                int jFrom = columnPointer == -1 ? 0 : columnPointer;
                int iTo = rowPointer == -1 ? DGV.Rows.Count : rowPointer + 1;
                int jTo = columnPointer == -1 ? DGV.Columns.Count : columnPointer + 1;
                for (int i = iFrom; i < iTo; i++)
                    for (int j = jFrom; j < jTo; j++)
                    {
                        if (DGV[j, i].Value != null)
                        {
                            float fontSize = DGV.Font.Size;
                            DGV[j, i].Style.Font = new Font(DGV.Font.FontFamily, fontSize);
                            while (!(TextRenderer.MeasureText(DGV[j, i].Value.ToString(), DGV[j, i].Style.Font).Width < Size &&
                                TextRenderer.MeasureText(DGV[j, i].Value.ToString(), DGV[j, i].Style.Font).Height < Size) &&
                                !(fontSize < 0.5f))
                            {
                                fontSize -= 0.5f;
                                DGV[j, i].Style.Font = new Font(DGV.Font.FontFamily, fontSize);
                            }
                        }
                    }
            }
        }
        private static void DGVsUpdateSizeAndLocation()
        {
            DGVleft.Location = new Point(DGVleft.Location.X, MaxLengthTop * Size + 3);
            DGVleft.Width = Size * MaxLengthLeft + 3;
            DGVleft.Height = Size * MainFieldHeigth + 3;

            DGVtop.Location = new Point(MaxLengthLeft * Size + 3, DGVtop.Location.Y);
            DGVtop.Width = Size * MainFieldWidth + 3;
            DGVtop.Height = Size * MaxLengthTop + 3;

            DGVmain.Location = new Point(DGVtop.Location.X, DGVleft.Location.Y);
            DGVmain.Width = Size * MainFieldWidth + 3;
            DGVmain.Height = Size * MainFieldHeigth + 3;

            SetOneSizeForAllRowsAndColumnsOnDGVs(DGVmain, DGVtop, DGVleft);
        }
        private static void SetOneSizeForAllRowsAndColumnsOnDGVs(params DataGridView[] DGVs)
        {
            foreach (DataGridView DGV in DGVs)
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                    DGV.Rows[i].Height = Size;
                for (int i = 0; i < DGV.Columns.Count; i++)
                    DGV.Columns[i].Width = Size;
            }
        }
        internal static void SetVisibleDGV(bool isVisible, params DataGridView[] DGVs)
        {
            foreach (DataGridView DGV in DGVs) DGV.Visible = isVisible;
        }
        internal static void SetDelay(int x)
        {
            Delay = x;
        }
        internal static void FillCell(int x, int y, bool isFull, bool isUnknown = false)
        {
            // заполнение ячейки согласно расчета (заполнена - красим в черный цвет, пустая - в серый
            //if (isUnknown) SetDelay(0);
            progressBar.PerformStep();
            labelProgress.Text = $"{progressBar.Value} / {progressBar.Maximum}";
            if (tasks == null)
            {
                tasks = new List<Task>();
                tasks.Add(new Task(() => { ForTasksFillSell(x, y, isFull, isUnknown); }));
                tasks[0].Start();
            }
            else
            {
                int tasksCount = tasks.Count;
                tasks.Add(tasks[tasksCount - 1].ContinueWith((Task t) => { ForTasksFillSell(x, y, isFull, isUnknown); }));
            }
        }
        private static void ForTasksFillSell(int x, int y, bool isFull, bool isUnknown = false)
        {
            if (Delay != 0)
            {
                DGVmain[y, x].Style.BackColor = Color.Green;
                DGVmain.Invalidate();
                Task.Delay(Delay).Wait();
            }
            if (isFull)
                DGVmain[y, x].Style.BackColor = Color.Black;
            else
            {
                if (!isUnknown)
                    DGVmain[y, x].Style.BackColor = Color.LightGray;
                else
                    DGVmain[y, x].Style.BackColor = Color.White;
            }
            DGVmain.Invalidate();
        }
        internal static void SaveAsPicture(Panel panelWithDGVs)
        {
            // сохранение результата как картинка. для отображения всего изображения - в фоне увеличиваем размеры формы до максимальных
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".bmp | *.bmp";
            if (sfd.ShowDialog() != DialogResult.OK) return;
            int w = MainForm.Width;
            int h = MainForm.Height;
            MainForm.Visible = false;
            MainForm.Size = new Size(maxSize * (MaxLengthLeft + MainFieldWidth) + 214, maxSize * (MaxLengthTop + MainFieldHeigth) + 185);
            Bitmap bmp = new Bitmap(Size * (MaxLengthLeft + MainFieldWidth) + 6, Size * (MaxLengthTop + MainFieldHeigth) + 6);
            panelWithDGVs.DrawToBitmap(bmp, panelWithDGVs.ClientRectangle);
            bmp.Save(sfd.FileName);
            MainForm.Size = new Size(w, h);
            MainForm.Visible = true;
        }
        internal static void ShowCurrentSolution(Cell[,] solution)
        {
            for (int i = 0; i < MainFieldHeigth; i++)
                for (int j = 0; j < MainFieldWidth; j++)
                    FillCell(i, j, solution[i, j].State == StatesOfCell.empty ? false : true);
        }
    }
}