using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace SolverJapaneseCrosswords
{
    class ExcelInput
    {
        private static string message;
        private static string Message
        {
            get { return message; }
            set { message = value; OnAddingMessage(message); }
        }
        internal delegate void AddingMessage(string Message, int colorARGB = 0x78000000);
        internal static event AddingMessage OnAddingMessage;
        internal static string[,] OpenExcelFile(string chooseExcelFileText)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            ofd.Title = Textes.chooseExcelFileText[Textes.currentLang];
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() != DialogResult.OK) return null;
            //получение всех листов выбранной книги
            string ws;
            try
            {
                Message = Textes.openingFile[Textes.currentLang] + ofd.FileName + "\n";
                List<string> sheets = OpenExcel.GetWorkSheetsList(ofd.FileName);
                ws = sheets[0];
                //выбор нужного листа книги
                if (!(sheets.Count == 1)) ws = SelectSheet(sheets);
                Message = Textes.reedingFromSheet[Textes.currentLang] + ws + "\n";
            }
            catch
            {
                Message = Textes.reedingError[Textes.currentLang];
                return null;
            }
            //считывание всего листа книги
            string[,] table = OpenExcel.GetInputStringTable(ws);
            return table;
        }
        private static string SelectSheet(List<string> sheets)
        {
            FormToChooseWS f = new FormToChooseWS();
            f.SetDefaults(sheets.ToArray());
            f.ShowDialog();
            return f.WS;
        }
    }
}
