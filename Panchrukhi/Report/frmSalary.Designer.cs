namespace Panchrukhi.Report
{
    partial class frmSalary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBasic = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbsent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdvCut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMobileBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOthers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCutSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPayable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_payslip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtBasic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Month: ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(104, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(116, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Basic";
            // 
            // txtBasic
            // 
            this.txtBasic.Location = new System.Drawing.Point(104, 45);
            this.txtBasic.Name = "txtBasic";
            this.txtBasic.Size = new System.Drawing.Size(116, 20);
            this.txtBasic.TabIndex = 3;
            this.txtBasic.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "%";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "Calculate Salary";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSL,
            this.colEmpID,
            this.colEmpName,
            this.colCat,
            this.colDesig,
            this.colSalary,
            this.colAbsent,
            this.colTD,
            this.colAdvCut,
            this.colMobileBill,
            this.colOthers,
            this.colCutSalary,
            this.colTotalPayable});
            this.dataGridView.Location = new System.Drawing.Point(8, 181);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1037, 448);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_DefaultValuesNeeded);
            // 
            // colSL
            // 
            this.colSL.HeaderText = "SL";
            this.colSL.Name = "colSL";
            this.colSL.ReadOnly = true;
            this.colSL.Width = 40;
            // 
            // colEmpID
            // 
            this.colEmpID.HeaderText = "EmpID";
            this.colEmpID.Name = "colEmpID";
            this.colEmpID.ReadOnly = true;
            this.colEmpID.Width = 60;
            // 
            // colEmpName
            // 
            this.colEmpName.HeaderText = "Emp Name";
            this.colEmpName.Name = "colEmpName";
            this.colEmpName.ReadOnly = true;
            // 
            // colCat
            // 
            this.colCat.HeaderText = "Category";
            this.colCat.Name = "colCat";
            this.colCat.ReadOnly = true;
            this.colCat.Width = 60;
            // 
            // colDesig
            // 
            this.colDesig.HeaderText = "Designation";
            this.colDesig.Name = "colDesig";
            this.colDesig.ReadOnly = true;
            this.colDesig.Width = 90;
            // 
            // colSalary
            // 
            this.colSalary.HeaderText = "Salary";
            this.colSalary.Name = "colSalary";
            this.colSalary.ReadOnly = true;
            this.colSalary.Width = 60;
            // 
            // colAbsent
            // 
            this.colAbsent.HeaderText = "Absent";
            this.colAbsent.Name = "colAbsent";
            this.colAbsent.ReadOnly = true;
            this.colAbsent.Width = 50;
            // 
            // colTD
            // 
            this.colTD.HeaderText = "Working Days";
            this.colTD.Name = "colTD";
            this.colTD.ReadOnly = true;
            // 
            // colAdvCut
            // 
            this.colAdvCut.HeaderText = "Adv. Cut";
            this.colAdvCut.Name = "colAdvCut";
            this.colAdvCut.ReadOnly = true;
            this.colAdvCut.Width = 80;
            // 
            // colMobileBill
            // 
            this.colMobileBill.HeaderText = "MobileBill";
            this.colMobileBill.Name = "colMobileBill";
            this.colMobileBill.ReadOnly = true;
            this.colMobileBill.Width = 60;
            // 
            // colOthers
            // 
            this.colOthers.HeaderText = "Others";
            this.colOthers.Name = "colOthers";
            this.colOthers.ReadOnly = true;
            this.colOthers.Width = 60;
            // 
            // colCutSalary
            // 
            this.colCutSalary.HeaderText = "Salary Cut";
            this.colCutSalary.Name = "colCutSalary";
            this.colCutSalary.ReadOnly = true;
            this.colCutSalary.Width = 90;
            // 
            // colTotalPayable
            // 
            this.colTotalPayable.HeaderText = "Total Payable";
            this.colTotalPayable.Name = "colTotalPayable";
            this.colTotalPayable.ReadOnly = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(313, 46);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Make ReadOnly";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(966, 155);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(75, 23);
            this.btn_print.TabIndex = 8;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_payslip
            // 
            this.btn_payslip.Location = new System.Drawing.Point(866, 155);
            this.btn_payslip.Name = "btn_payslip";
            this.btn_payslip.Size = new System.Drawing.Size(75, 23);
            this.btn_payslip.TabIndex = 9;
            this.btn_payslip.Text = "payslip";
            this.btn_payslip.UseVisualStyleBackColor = true;
            this.btn_payslip.Click += new System.EventHandler(this.btn_payslip_Click);
            // 
            // frmSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 635);
            this.Controls.Add(this.btn_payslip);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBasic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "frmSalary";
            this.Text = "frmSalary";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSalary_FormClosed);
            this.Load += new System.EventHandler(this.frmSalary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBasic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtBasic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_payslip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbsent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdvCut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMobileBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOthers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCutSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPayable;
    }
}