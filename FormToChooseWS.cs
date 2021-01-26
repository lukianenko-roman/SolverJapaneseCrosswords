using System;
using System.Windows.Forms;

namespace SolverJapaneseCrosswords
{
    public partial class FormToChooseWS : Form
    {
        private string ws;
        public FormToChooseWS()
        {
            InitializeComponent();
        }

        internal string WS
        {
            get { return ws; }
        }

        internal void SetDefaults(string[] sheets)
        {
            comboBox1.Items.AddRange(sheets);
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            label1.Text = Textes.labelFormChooseWS[Textes.currentLang];
        }

        private void buttonToChooseWS_Click(object sender, EventArgs e)
        {
            ws = comboBox1.SelectedItem.ToString();
            this.Close();
        }
    }
}
