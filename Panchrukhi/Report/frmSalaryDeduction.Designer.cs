namespace Panchrukhi.Report
{
    partial class frmSalaryDeduction
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtEmpID = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YEAR_MONTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEDUC_CAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEDUCTED_SALARY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveAndUpdate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtDeductAmount = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(197, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(146, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Month: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Category: ";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(197, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(197, 78);
            this.txtEmpID.Mask = "000000";
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(146, 20);
            this.txtEmpID.TabIndex = 6;
            this.txtEmpID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpID_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Emp ID:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SL,
            this.YEAR_MONTH,
            this.DEDUC_CAT,
            this.EMP_ID,
            this.DEDUCTED_SALARY,
            this.CAT_ID});
            this.dataGridView1.Location = new System.Drawing.Point(10, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(528, 247);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // SL
            // 
            this.SL.DataPropertyName = "SL";
            this.SL.HeaderText = "SL";
            this.SL.Name = "SL";
            this.SL.ReadOnly = true;
            this.SL.Visible = false;
            // 
            // YEAR_MONTH
            // 
            this.YEAR_MONTH.DataPropertyName = "YEAR_MONTH";
            this.YEAR_MONTH.HeaderText = "Month";
            this.YEAR_MONTH.Name = "YEAR_MONTH";
            this.YEAR_MONTH.ReadOnly = true;
            // 
            // DEDUC_CAT
            // 
            this.DEDUC_CAT.DataPropertyName = "DEDUC_CAT";
            this.DEDUC_CAT.HeaderText = "Category";
            this.DEDUC_CAT.Name = "DEDUC_CAT";
            this.DEDUC_CAT.ReadOnly = true;
            // 
            // EMP_ID
            // 
            this.EMP_ID.DataPropertyName = "EMP_ID";
            this.EMP_ID.HeaderText = "Emp ID";
            this.EMP_ID.Name = "EMP_ID";
            this.EMP_ID.ReadOnly = true;
            // 
            // DEDUCTED_SALARY
            // 
            this.DEDUCTED_SALARY.DataPropertyName = "DEDUCTED_SALARY";
            this.DEDUCTED_SALARY.HeaderText = "Deducted Salary";
            this.DEDUCTED_SALARY.Name = "DEDUCTED_SALARY";
            this.DEDUCTED_SALARY.ReadOnly = true;
            this.DEDUCTED_SALARY.Width = 150;
            // 
            // CAT_ID
            // 
            this.CAT_ID.DataPropertyName = "CAT_ID";
            this.CAT_ID.HeaderText = "CAT_ID";
            this.CAT_ID.Name = "CAT_ID";
            this.CAT_ID.ReadOnly = true;
            this.CAT_ID.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Deducted Amount :";
            // 
            // btnSaveAndUpdate
            // 
            this.btnSaveAndUpdate.Location = new System.Drawing.Point(405, 21);
            this.btnSaveAndUpdate.Name = "btnSaveAndUpdate";
            this.btnSaveAndUpdate.Size = new System.Drawing.Size(91, 29);
            this.btnSaveAndUpdate.TabIndex = 19;
            this.btnSaveAndUpdate.Text = "Save";
            this.btnSaveAndUpdate.UseVisualStyleBackColor = true;
            this.btnSaveAndUpdate.Click += new System.EventHandler(this.btnSaveAndUpdate_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(405, 72);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(91, 27);
            this.btnReset.TabIndex = 20;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtDeductAmount
            // 
            this.txtDeductAmount.Location = new System.Drawing.Point(197, 111);
            this.txtDeductAmount.Mask = "000000";
            this.txtDeductAmount.Name = "txtDeductAmount";
            this.txtDeductAmount.Size = new System.Drawing.Size(146, 20);
            this.txtDeductAmount.TabIndex = 23;
            // 
            // frmSalaryDeduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 424);
            this.Controls.Add(this.txtDeductAmount);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveAndUpdate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmpID);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmSalaryDeduction";
            this.Text = "Salary Deduction";
            this.Load += new System.EventHandler(this.frmSalaryDeduction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MaskedTextBox txtEmpID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveAndUpdate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.MaskedTextBox txtDeductAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn YEAR_MONTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEDUC_CAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEDUCTED_SALARY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAT_ID;
    }
}