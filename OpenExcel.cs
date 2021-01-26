using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace SolverJapaneseCrosswords
{
    public static class OpenExcel
    {
        private static string path;

        private static Application excApp;
        private static Workbook wb;

        private static Process[] excelProcessesBefore;
        private static Process[] excelProcessesAfter;
        private static Process excelProcessesToBeKilled;
        public static List<string> GetWorkSheetsList(string _path = "")
        {
            path = _path;

            excelProcessesBefore = GetExcelProcesses();
            excApp = new Application();
            excelProcessesAfter = GetExcelProcesses();
            SetExcelProcessesToBeKilled();

            wb = excApp.Workbooks.Open(path, false, true);
            List<string> sheets = new List<string>();
            foreach (Worksheet _ws in wb.Worksheets) sheets.Add(_ws.Name);

            return sheets;
        }
        public static string[,] GetInputStringTable(string workSheet)
        {
            wb = excApp.Workbooks.Open(path, false, true);
            Worksheet ws = (Worksheet)wb.Sheets[workSheet];
            int h = ws.UsedRange.Rows.Count;
            int w = ws.UsedRange.Columns.Count;
            string[,] table = new string[h, w];
            for (int i = 1; i <= h; i++)
                for (int j = 1; j <= w; j++)
                {
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j];
                    if (range.Value != null) table[i - 1, j - 1] = range.Value.ToString();
                }
            wb.Close();
            excApp.Quit();
            KillExcelProcess();
            GC.Collect();

            return table;
        }
        private static Process[] GetExcelProcesses()
        {
            return Process.GetProcessesByName("excel");
        }
        private static void SetExcelProcessesToBeKilled()
        {
            if (excelProcessesBefore.Length > 0)
            {
                foreach (Process pAfter in excelProcessesAfter)
                    foreach (Process pBefore in excelProcessesAfter)
                        if (pBefore != pAfter)
                            excelProcessesToBeKilled = pAfter;
            }
            else
                excelProcessesToBeKilled = excelProcessesAfter[0];
        }
        private static void KillExcelProcess()
        {
            excelProcessesToBeKilled?.Kill();
            wb = null;
            excApp = null;
            excelProcessesBefore = null;
            excelProcessesAfter = null;
            excelProcessesToBeKilled = null;
        }
    }
}
