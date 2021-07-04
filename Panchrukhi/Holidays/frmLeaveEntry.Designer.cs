namespace Panchrukhi.Holidays
{
    partial class frmLeaveEntry
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSaveAndUpdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxLeaveCat = new System.Windows.Forms.ComboBox();
            this.dtPkrFormDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateForm = new System.Windows.Forms.Label();
            this.dataGridview = new System.Windows.Forms.DataGridView();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLEAVE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCombo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEmpID = new System.Windows.Forms.MaskedTextBox();
            this.dtPkrToDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(392, 134);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 23);
            this.btnClose.TabIndex = 111;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(392, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 23);
            this.btnDelete.TabIndex = 110;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(392, 70);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(101, 23);
            this.btnReset.TabIndex = 109;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveAndUpdate
            // 
            this.btnSaveAndUpdate.Location = new System.Drawing.Point(392, 38);
            this.btnSaveAndUpdate.Name = "btnSaveAndUpdate";
            this.btnSaveAndUpdate.Size = new System.Drawing.Size(101, 23);
            this.btnSaveAndUpdate.TabIndex = 108;
            this.btnSaveAndUpdate.Text = "Save";
            this.btnSaveAndUpdate.UseVisualStyleBackColor = true;
            this.btnSaveAndUpdate.Click += new System.EventHandler(this.btnSaveAndUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Leave Cat:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "Emp ID:";
            // 
            // cbxLeaveCat
            // 
            this.cbxLeaveCat.FormattingEnabled = true;
            this.cbxLeaveCat.Location = new System.Drawing.Point(70, 81);
            this.cbxLeaveCat.Name = "cbxLeaveCat";
            this.cbxLeaveCat.Size = new System.Drawing.Size(176, 21);
            this.cbxLeaveCat.TabIndex = 112;
            // 
            // dtPkrFormDate
            // 
            this.dtPkrFormDate.Location = new System.Drawing.Point(70, 117);
            this.dtPkrFormDate.Name = "dtPkrFormDate";
            this.dtPkrFormDate.Size = new System.Drawing.Size(95, 20);
            this.dtPkrFormDate.TabIndex = 114;
            // 
            // lblDateForm
            // 
            this.lblDateForm.AutoSize = true;
            this.lblDateForm.Location = new System.Drawing.Point(5, 122);
            this.lblDateForm.Name = "lblDateForm";
            this.lblDateForm.Size = new System.Drawing.Size(59, 13);
            this.lblDateForm.TabIndex = 113;
            this.lblDateForm.Text = "Date From:";
            // 
            // dataGridview
            // 
            this.dataGridview.AllowUserToAddRows = false;
            this.dataGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSL,
            this.colEmpID,
            this.colLEAVE_NAME,
            this.colDate,
            this.colCombo});
            this.dataGridview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridview.Location = new System.Drawing.Point(6, 173);
            this.dataGridview.MultiSelect = false;
            this.dataGridview.Name = "dataGridview";
            this.dataGridview.Size = new System.Drawing.Size(541, 261);
            this.dataGridview.TabIndex = 117;
            this.dataGridview.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridview_RowHeaderMouseClick);
            this.dataGridview.SelectionChanged += new System.EventHandler(this.dataGridview_SelectionChanged);
            // 
            // colSL
            // 
            this.colSL.DataPropertyName = "SL_LEAVE";
            this.colSL.HeaderText = "SL";
            this.colSL.Name = "colSL";
            this.colSL.Visible = false;
            // 
            // colEmpID
            // 
            this.colEmpID.DataPropertyName = "EMP_ID";
            this.colEmpID.HeaderText = "Emp ID";
            this.colEmpID.Name = "colEmpID";
            this.colEmpID.ReadOnly = true;
            // 
            // colLEAVE_NAME
            // 
            this.colLEAVE_NAME.DataPropertyName = "LEAVE_NAME";
            this.colLEAVE_NAME.HeaderText = "Leave Cat";
            this.colLEAVE_NAME.Name = "colLEAVE_NAME";
            this.colLEAVE_NAME.Width = 170;
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "LEAVE_DATE";
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            // 
            // colCombo
            // 
            this.colCombo.HeaderText = "Combo";
            this.colCombo.Name = "colCombo";
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(70, 48);
            this.txtEmpID.Mask = "000000";
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(176, 20);
            this.txtEmpID.TabIndex = 118;
            // 
            // dtPkrToDate
            // 
            this.dtPkrToDate.Location = new System.Drawing.Point(200, 117);
            this.dtPkrToDate.Name = "dtPkrToDate";
            this.dtPkrToDate.Size = new System.Drawing.Size(95, 20);
            this.dtPkrToDate.TabIndex = 119;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(173, 120);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(20, 13);
            this.lblDateTo.TabIndex = 120;
            this.lblDateTo.Text = "To";
            // 
            // frmLeaveEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 446);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtPkrToDate);
            this.Controls.Add(this.txtEmpID);
            this.Controls.Add(this.dataGridview);
            this.Controls.Add(this.dtPkrFormDate);
            this.Controls.Add(this.lblDateForm);
            this.Controls.Add(this.cbxLeaveCat);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveAndUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmLeaveEntry";
            this.Text = "frmLeaveEntry";
            this.Load += new System.EventHandler(this.frmLeaveEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSaveAndUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLeaveCat;
        private System.Windows.Forms.DateTimePicker dtPkrFormDate;
        private System.Windows.Forms.Label lblDateForm;
        private System.Windows.Forms.DataGridView dataGridview;
        private System.Windows.Forms.MaskedTextBox txtEmpID;
        private System.Windows.Forms.DateTimePicker dtPkrToDate;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLEAVE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCombo;
    }
}