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
            this.colTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbsent = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.button1.Location = new System.Drawing.Point(145, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Summary";
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
            this.colTD,
            this.colAbsent});
            this.dataGridView.Location = new System.Drawing.Point(4, 138);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1031, 494);
            this.dataGridView.TabIndex = 6;
            // 
            // colSL
            // 
            this.colSL.HeaderText = "SL";
            this.colSL.Name = "colSL";
            this.colSL.ReadOnly = true;
            // 
            // colEmpID
            // 
            this.colEmpID.HeaderText = "Emp ID";
            this.colEmpID.Name = "colEmpID";
            this.colEmpID.ReadOnly = true;
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
            // 
            // colDesig
            // 
            this.colDesig.HeaderText = "Designation";
            this.colDesig.Name = "colDesig";
            this.colDesig.ReadOnly = true;
            // 
            // colSalary
            // 
            this.colSalary.HeaderText = "Salary";
            this.colSalary.Name = "colSalary";
            this.colSalary.ReadOnly = true;
            // 
            // colTD
            // 
            this.colTD.HeaderText = "Total Days";
            this.colTD.Name = "colTD";
            this.colTD.ReadOnly = true;
            // 
            // colAbsent
            // 
            this.colAbsent.HeaderText = "Absent";
            this.colAbsent.Name = "colAbsent";
            this.colAbsent.ReadOnly = true;
            // 
            // frmSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 635);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBasic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "frmSalary";
            this.Text = "frmSalary";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbsent;
    }
}