namespace SolverJapaneseCrosswords
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenExcel = new System.Windows.Forms.Button();
            this.panelLang = new System.Windows.Forms.Panel();
            this.radioButtonEN = new System.Windows.Forms.RadioButton();
            this.radioButtonRU = new System.Windows.Forms.RadioButton();
            this.richTextBoxActionLog = new System.Windows.Forms.RichTextBox();
            this.checkBoxActionLog = new System.Windows.Forms.CheckBox();
            this.panelInputs = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.panelManualInput = new System.Windows.Forms.Panel();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.numericUpDownMaxCountTop = new System.Windows.Forms.NumericUpDown();
            this.labelLengthConditionTop = new System.Windows.Forms.Label();
            this.numericUpDownMaxCountLeft = new System.Windows.Forms.NumericUpDown();
            this.labelLengthConditionLeft = new System.Windows.Forms.Label();
            this.numericUpDownWidthMainZone = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeightMainZone = new System.Windows.Forms.NumericUpDown();
            this.labelWidthMainZone = new System.Windows.Forms.Label();
            this.labelHeightMainZone = new System.Windows.Forms.Label();
            this.labelInput = new System.Windows.Forms.Label();
            this.radioButtonInputManual = new System.Windows.Forms.RadioButton();
            this.radioButtonInputExcel = new System.Windows.Forms.RadioButton();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.dataGridViewTop = new System.Windows.Forms.DataGridView();
            this.dataGridViewLeft = new System.Windows.Forms.DataGridView();
            this.checkBoxDelay = new System.Windows.Forms.CheckBox();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.panelWithDGVs = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonPrevSolution = new System.Windows.Forms.Button();
            this.buttonNextSolution = new System.Windows.Forms.Button();
            this.panelLang.SuspendLayout();
            this.panelInputs.SuspendLayout();
            this.panelManualInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCountTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCountLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthMainZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightMainZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            this.panelWithDGVs.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenExcel
            // 
            this.buttonOpenExcel.Location = new System.Drawing.Point(0, 38);
            this.buttonOpenExcel.Name = "buttonOpenExcel";
            this.buttonOpenExcel.Size = new System.Drawing.Size(159, 23);
            this.buttonOpenExcel.TabIndex = 0;
            this.buttonOpenExcel.Text = "Открыть Excel файл";
            this.buttonOpenExcel.UseVisualStyleBackColor = true;
            this.buttonOpenExcel.Click += new System.EventHandler(this.ButtonOpenExcel_Click);
            // 
            // panelLang
            // 
            this.panelLang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLang.Controls.Add(this.radioButtonEN);
            this.panelLang.Controls.Add(this.radioButtonRU);
            this.panelLang.Location = new System.Drawing.Point(12, 12);
            this.panelLang.Name = "panelLang";
            this.panelLang.Size = new System.Drawing.Size(105, 33);
            this.panelLang.TabIndex = 1;
            // 
            // radioButtonEN
            // 
            this.radioButtonEN.AutoSize = true;
            this.radioButtonEN.Location = new System.Drawing.Point(51, 4);
            this.radioButtonEN.Name = "radioButtonEN";
            this.radioButtonEN.Size = new System.Drawing.Size(40, 17);
            this.radioButtonEN.TabIndex = 1;
            this.radioButtonEN.TabStop = true;
            this.radioButtonEN.Text = "EN";
            this.radioButtonEN.UseVisualStyleBackColor = true;
            this.radioButtonEN.CheckedChanged += new System.EventHandler(this.RadioButtonLang_CheckedChanged);
            // 
            // radioButtonRU
            // 
            this.radioButtonRU.AutoSize = true;
            this.radioButtonRU.Location = new System.Drawing.Point(8, 4);
            this.radioButtonRU.Name = "radioButtonRU";
            this.radioButtonRU.Size = new System.Drawing.Size(41, 17);
            this.radioButtonRU.TabIndex = 0;
            this.radioButtonRU.TabStop = true;
            this.radioButtonRU.Text = "RU";
            this.radioButtonRU.UseVisualStyleBackColor = true;
            this.radioButtonRU.CheckedChanged += new System.EventHandler(this.RadioButtonLang_CheckedChanged);
            // 
            // richTextBoxActionLog
            // 
            this.richTextBoxActionLog.Location = new System.Drawing.Point(12, 464);
            this.richTextBoxActionLog.Name = "richTextBoxActionLog";
            this.richTextBoxActionLog.ReadOnly = true;
            this.richTextBoxActionLog.Size = new System.Drawing.Size(760, 85);
            this.richTextBoxActionLog.TabIndex = 2;
            this.richTextBoxActionLog.Text = "";
            // 
            // checkBoxActionLog
            // 
            this.checkBoxActionLog.AutoSize = true;
            this.checkBoxActionLog.Checked = true;
            this.checkBoxActionLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActionLog.Location = new System.Drawing.Point(12, 439);
            this.checkBoxActionLog.Name = "checkBoxActionLog";
            this.checkBoxActionLog.Size = new System.Drawing.Size(77, 17);
            this.checkBoxActionLog.TabIndex = 4;
            this.checkBoxActionLog.Text = "Action Log";
            this.checkBoxActionLog.UseVisualStyleBackColor = true;
            // 
            // panelInputs
            // 
            this.panelInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInputs.Controls.Add(this.buttonSave);
            this.panelInputs.Controls.Add(this.buttonCalculate);
            this.panelInputs.Controls.Add(this.buttonReset);
            this.panelInputs.Controls.Add(this.panelManualInput);
            this.panelInputs.Controls.Add(this.labelInput);
            this.panelInputs.Controls.Add(this.radioButtonInputManual);
            this.panelInputs.Controls.Add(this.radioButtonInputExcel);
            this.panelInputs.Controls.Add(this.buttonOpenExcel);
            this.panelInputs.Location = new System.Drawing.Point(12, 59);
            this.panelInputs.Name = "panelInputs";
            this.panelInputs.Size = new System.Drawing.Size(162, 374);
            this.panelInputs.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(1, 342);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(158, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save as picture";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Enabled = false;
            this.buttonCalculate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCalculate.Location = new System.Drawing.Point(1, 312);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(158, 23);
            this.buttonCalculate.TabIndex = 6;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Enabled = false;
            this.buttonReset.Location = new System.Drawing.Point(30, 282);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(102, 23);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // panelManualInput
            // 
            this.panelManualInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelManualInput.Controls.Add(this.buttonCheck);
            this.panelManualInput.Controls.Add(this.numericUpDownMaxCountTop);
            this.panelManualInput.Controls.Add(this.labelLengthConditionTop);
            this.panelManualInput.Controls.Add(this.numericUpDownMaxCountLeft);
            this.panelManualInput.Controls.Add(this.labelLengthConditionLeft);
            this.panelManualInput.Controls.Add(this.numericUpDownWidthMainZone);
            this.panelManualInput.Controls.Add(this.numericUpDownHeightMainZone);
            this.panelManualInput.Controls.Add(this.labelWidthMainZone);
            this.panelManualInput.Controls.Add(this.labelHeightMainZone);
            this.panelManualInput.Location = new System.Drawing.Point(1, 85);
            this.panelManualInput.Name = "panelManualInput";
            this.panelManualInput.Size = new System.Drawing.Size(158, 194);
            this.panelManualInput.TabIndex = 4;
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(6, 165);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(146, 23);
            this.buttonCheck.TabIndex = 6;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.ButtonCheck_Click);
            // 
            // numericUpDownMaxCountTop
            // 
            this.numericUpDownMaxCountTop.Location = new System.Drawing.Point(107, 128);
            this.numericUpDownMaxCountTop.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownMaxCountTop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxCountTop.Name = "numericUpDownMaxCountTop";
            this.numericUpDownMaxCountTop.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownMaxCountTop.TabIndex = 3;
            this.numericUpDownMaxCountTop.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownMaxCountTop.ValueChanged += new System.EventHandler(this.NumericUpDownMaxCountTop_ValueChanged);
            // 
            // labelLengthConditionTop
            // 
            this.labelLengthConditionTop.AutoSize = true;
            this.labelLengthConditionTop.Location = new System.Drawing.Point(1, 118);
            this.labelLengthConditionTop.Name = "labelLengthConditionTop";
            this.labelLengthConditionTop.Size = new System.Drawing.Size(71, 39);
            this.labelLengthConditionTop.TabIndex = 2;
            this.labelLengthConditionTop.Text = "Max count of\r\nelements in\r\ncondition(top)";
            // 
            // numericUpDownMaxCountLeft
            // 
            this.numericUpDownMaxCountLeft.Location = new System.Drawing.Point(107, 76);
            this.numericUpDownMaxCountLeft.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownMaxCountLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxCountLeft.Name = "numericUpDownMaxCountLeft";
            this.numericUpDownMaxCountLeft.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownMaxCountLeft.TabIndex = 3;
            this.numericUpDownMaxCountLeft.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownMaxCountLeft.ValueChanged += new System.EventHandler(this.NumericUpDownMaxCountLeft_ValueChanged);
            // 
            // labelLengthConditionLeft
            // 
            this.labelLengthConditionLeft.AutoSize = true;
            this.labelLengthConditionLeft.Location = new System.Drawing.Point(1, 66);
            this.labelLengthConditionLeft.Name = "labelLengthConditionLeft";
            this.labelLengthConditionLeft.Size = new System.Drawing.Size(70, 39);
            this.labelLengthConditionLeft.TabIndex = 2;
            this.labelLengthConditionLeft.Text = "Max count of\r\nelements in\r\ncondition(left)";
            // 
            // numericUpDownWidthMainZone
            // 
            this.numericUpDownWidthMainZone.Location = new System.Drawing.Point(107, 32);
            this.numericUpDownWidthMainZone.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownWidthMainZone.Name = "numericUpDownWidthMainZone";
            this.numericUpDownWidthMainZone.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownWidthMainZone.TabIndex = 3;
            this.numericUpDownWidthMainZone.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownWidthMainZone.ValueChanged += new System.EventHandler(this.NumericUpDownWidthMainZone_ValueChanged);
            // 
            // numericUpDownHeightMainZone
            // 
            this.numericUpDownHeightMainZone.Location = new System.Drawing.Point(107, 3);
            this.numericUpDownHeightMainZone.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownHeightMainZone.Name = "numericUpDownHeightMainZone";
            this.numericUpDownHeightMainZone.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownHeightMainZone.TabIndex = 3;
            this.numericUpDownHeightMainZone.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownHeightMainZone.ValueChanged += new System.EventHandler(this.NumericUpDownHeightMainZone_ValueChanged);
            // 
            // labelWidthMainZone
            // 
            this.labelWidthMainZone.AutoSize = true;
            this.labelWidthMainZone.Location = new System.Drawing.Point(1, 34);
            this.labelWidthMainZone.Name = "labelWidthMainZone";
            this.labelWidthMainZone.Size = new System.Drawing.Size(69, 13);
            this.labelWidthMainZone.TabIndex = 2;
            this.labelWidthMainZone.Text = "Width of field";
            // 
            // labelHeightMainZone
            // 
            this.labelHeightMainZone.AutoSize = true;
            this.labelHeightMainZone.Location = new System.Drawing.Point(1, 5);
            this.labelHeightMainZone.Name = "labelHeightMainZone";
            this.labelHeightMainZone.Size = new System.Drawing.Size(72, 13);
            this.labelHeightMainZone.TabIndex = 2;
            this.labelHeightMainZone.Text = "Height of field";
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(2, 2);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(35, 13);
            this.labelInput.TabIndex = 3;
            this.labelInput.Text = "label1";
            // 
            // radioButtonInputManual
            // 
            this.radioButtonInputManual.AutoSize = true;
            this.radioButtonInputManual.Location = new System.Drawing.Point(2, 63);
            this.radioButtonInputManual.Name = "radioButtonInputManual";
            this.radioButtonInputManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonInputManual.TabIndex = 2;
            this.radioButtonInputManual.TabStop = true;
            this.radioButtonInputManual.Text = "Manual";
            this.radioButtonInputManual.UseVisualStyleBackColor = true;
            this.radioButtonInputManual.CheckedChanged += new System.EventHandler(this.RadioButtonInput_CheckedChanged);
            // 
            // radioButtonInputExcel
            // 
            this.radioButtonInputExcel.AutoSize = true;
            this.radioButtonInputExcel.Location = new System.Drawing.Point(1, 18);
            this.radioButtonInputExcel.Name = "radioButtonInputExcel";
            this.radioButtonInputExcel.Size = new System.Drawing.Size(51, 17);
            this.radioButtonInputExcel.TabIndex = 1;
            this.radioButtonInputExcel.TabStop = true;
            this.radioButtonInputExcel.Text = "Excel";
            this.radioButtonInputExcel.UseVisualStyleBackColor = true;
            this.radioButtonInputExcel.CheckedChanged += new System.EventHandler(this.RadioButtonInput_CheckedChanged);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.AllowUserToResizeColumns = false;
            this.dataGridViewMain.AllowUserToResizeRows = false;
            this.dataGridViewMain.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.ColumnHeadersVisible = false;
            this.dataGridViewMain.Enabled = false;
            this.dataGridViewMain.Location = new System.Drawing.Point(33, 41);
            this.dataGridViewMain.MultiSelect = false;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.RowHeadersVisible = false;
            this.dataGridViewMain.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMain.Size = new System.Drawing.Size(130, 117);
            this.dataGridViewMain.TabIndex = 6;
            this.dataGridViewMain.Text = "dataGridView1";
            this.dataGridViewMain.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridViewMain_CellPainting);
            // 
            // dataGridViewTop
            // 
            this.dataGridViewTop.AllowUserToAddRows = false;
            this.dataGridViewTop.AllowUserToDeleteRows = false;
            this.dataGridViewTop.AllowUserToResizeColumns = false;
            this.dataGridViewTop.AllowUserToResizeRows = false;
            this.dataGridViewTop.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTop.ColumnHeadersVisible = false;
            this.dataGridViewTop.Enabled = false;
            this.dataGridViewTop.Location = new System.Drawing.Point(63, 0);
            this.dataGridViewTop.MultiSelect = false;
            this.dataGridViewTop.Name = "dataGridViewTop";
            this.dataGridViewTop.RowHeadersVisible = false;
            this.dataGridViewTop.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewTop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewTop.Size = new System.Drawing.Size(53, 22);
            this.dataGridViewTop.TabIndex = 7;
            this.dataGridViewTop.Text = "dataGridView1";
            this.dataGridViewTop.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValidated);
            // 
            // dataGridViewLeft
            // 
            this.dataGridViewLeft.AllowUserToAddRows = false;
            this.dataGridViewLeft.AllowUserToDeleteRows = false;
            this.dataGridViewLeft.AllowUserToResizeColumns = false;
            this.dataGridViewLeft.AllowUserToResizeRows = false;
            this.dataGridViewLeft.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLeft.ColumnHeadersVisible = false;
            this.dataGridViewLeft.Enabled = false;
            this.dataGridViewLeft.Location = new System.Drawing.Point(0, 66);
            this.dataGridViewLeft.MultiSelect = false;
            this.dataGridViewLeft.Name = "dataGridViewLeft";
            this.dataGridViewLeft.RowHeadersVisible = false;
            this.dataGridViewLeft.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewLeft.Size = new System.Drawing.Size(27, 16);
            this.dataGridViewLeft.TabIndex = 8;
            this.dataGridViewLeft.Text = "dataGridView2";
            this.dataGridViewLeft.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValidated);
            // 
            // checkBoxDelay
            // 
            this.checkBoxDelay.AutoSize = true;
            this.checkBoxDelay.Checked = true;
            this.checkBoxDelay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDelay.Location = new System.Drawing.Point(180, 439);
            this.checkBoxDelay.Name = "checkBoxDelay";
            this.checkBoxDelay.Size = new System.Drawing.Size(160, 17);
            this.checkBoxDelay.TabIndex = 9;
            this.checkBoxDelay.Text = "Step by step with delay (ms):";
            this.checkBoxDelay.UseVisualStyleBackColor = true;
            this.checkBoxDelay.CheckedChanged += new System.EventHandler(this.CheckBoxDelay_CheckedChanged);
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.Location = new System.Drawing.Point(363, 438);
            this.numericUpDownDelay.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownDelay.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownDelay.TabIndex = 10;
            this.numericUpDownDelay.ThousandsSeparator = true;
            this.numericUpDownDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDelay.ValueChanged += new System.EventHandler(this.NumericUpDownDelay_ValueChanged);
            // 
            // panelWithDGVs
            // 
            this.panelWithDGVs.AutoScroll = true;
            this.panelWithDGVs.Controls.Add(this.dataGridViewLeft);
            this.panelWithDGVs.Controls.Add(this.dataGridViewTop);
            this.panelWithDGVs.Controls.Add(this.dataGridViewMain);
            this.panelWithDGVs.Location = new System.Drawing.Point(180, 12);
            this.panelWithDGVs.Name = "panelWithDGVs";
            this.panelWithDGVs.Size = new System.Drawing.Size(592, 421);
            this.panelWithDGVs.TabIndex = 11;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(488, 438);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(284, 10);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 12;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.BackColor = System.Drawing.Color.Transparent;
            this.labelProgress.Location = new System.Drawing.Point(488, 448);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(16, 13);
            this.labelProgress.TabIndex = 13;
            this.labelProgress.Text = "   ";
            // 
            // buttonPrevSolution
            // 
            this.buttonPrevSolution.Location = new System.Drawing.Point(420, 438);
            this.buttonPrevSolution.Name = "buttonPrevSolution";
            this.buttonPrevSolution.Size = new System.Drawing.Size(23, 23);
            this.buttonPrevSolution.TabIndex = 14;
            this.buttonPrevSolution.Text = "<";
            this.buttonPrevSolution.UseVisualStyleBackColor = true;
            this.buttonPrevSolution.Visible = false;
            this.buttonPrevSolution.Click += new System.EventHandler(this.buttonPrevSolution_Click);
            // 
            // buttonNextSolution
            // 
            this.buttonNextSolution.Location = new System.Drawing.Point(459, 438);
            this.buttonNextSolution.Name = "buttonNextSolution";
            this.buttonNextSolution.Size = new System.Drawing.Size(23, 23);
            this.buttonNextSolution.TabIndex = 15;
            this.buttonNextSolution.Text = ">";
            this.buttonNextSolution.UseVisualStyleBackColor = true;
            this.buttonNextSolution.Visible = false;
            this.buttonNextSolution.Click += new System.EventHandler(this.buttonNextSolusion_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonNextSolution);
            this.Controls.Add(this.buttonPrevSolution);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelWithDGVs);
            this.Controls.Add(this.numericUpDownDelay);
            this.Controls.Add(this.checkBoxDelay);
            this.Controls.Add(this.panelInputs);
            this.Controls.Add(this.checkBoxActionLog);
            this.Controls.Add(this.richTextBoxActionLog);
            this.Controls.Add(this.panelLang);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.panelLang.ResumeLayout(false);
            this.panelLang.PerformLayout();
            this.panelInputs.ResumeLayout(false);
            this.panelInputs.PerformLayout();
            this.panelManualInput.ResumeLayout(false);
            this.panelManualInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCountTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCountLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthMainZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightMainZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            this.panelWithDGVs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelLang;
        private System.Windows.Forms.RadioButton radioButtonEN;
        private System.Windows.Forms.RadioButton radioButtonRU;
        private System.Windows.Forms.Button buttonOpenExcel;
        private System.Windows.Forms.RichTextBox richTextBoxActionLog;
        private System.Windows.Forms.CheckBox checkBoxActionLog;
        private System.Windows.Forms.Panel panelInputs;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.RadioButton radioButtonInputManual;
        private System.Windows.Forms.RadioButton radioButtonInputExcel;
        private System.Windows.Forms.Panel panelManualInput;
        private System.Windows.Forms.Label labelWidthMainZone;
        private System.Windows.Forms.Label labelHeightMainZone;
        private System.Windows.Forms.NumericUpDown numericUpDownWidthMainZone;
        private System.Windows.Forms.NumericUpDown numericUpDownHeightMainZone;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxCountTop;
        private System.Windows.Forms.Label labelLengthConditionTop;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxCountLeft;
        private System.Windows.Forms.Label labelLengthConditionLeft;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.DataGridView dataGridViewTop;
        private System.Windows.Forms.DataGridView dataGridViewLeft;
        private System.Windows.Forms.CheckBox checkBoxDelay;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.Panel panelWithDGVs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonPrevSolution;
        private System.Windows.Forms.Button buttonNextSolution;
    }
}

