namespace Panchrukhi
{
    partial class frmLeaveEntry2nd
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSaveAndUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblDateForm = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colNHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeaveCatID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDBSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtPkrFormDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtPkrToDate = new System.Windows.Forms.DateTimePicker();
            this.cbxLeaveCat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpID = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckEnableSearch = new System.Windows.Forms.CheckBox();
            this.gbxSerachOption = new System.Windows.Forms.GroupBox();
            this.cbxSrcLvCat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSrcID = new System.Windows.Forms.MaskedTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpFstDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gbxSerachOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(314, 78);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(314, 47);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(89, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnSaveAndUpdate
            // 
            this.btnSaveAndUpdate.Location = new System.Drawing.Point(314, 16);
            this.btnSaveAndUpdate.Name = "btnSaveAndUpdate";
            this.btnSaveAndUpdate.Size = new System.Drawing.Size(89, 23);
            this.btnSaveAndUpdate.TabIndex = 4;
            this.btnSaveAndUpdate.Text = "Save";
            this.btnSaveAndUpdate.UseVisualStyleBackColor = true;
            this.btnSaveAndUpdate.Click += new System.EventHandler(this.BtnSaveAndUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(314, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnFrmSlotClose_Click);
            // 
            // lblDateForm
            // 
            this.lblDateForm.AutoSize = true;
            this.lblDateForm.Location = new System.Drawing.Point(19, 82);
            this.lblDateForm.Name = "lblDateForm";
            this.lblDateForm.Size = new System.Drawing.Size(56, 13);
            this.lblDateForm.TabIndex = 95;
            this.lblDateForm.Text = "Date From";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNHID,
            this.colEmpID,
            this.colHDate,
            this.colHCategory,
            this.colLeaveCatID,
            this.colDBSL});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(8, 308);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(493, 310);
            this.dataGridView.TabIndex = 97;
            this.dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridData_RowHeaderMouseClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.DataGridData_SelectionChanged);
            // 
            // colNHID
            // 
            this.colNHID.HeaderText = "ID";
            this.colNHID.Name = "colNHID";
            this.colNHID.ReadOnly = true;
            this.colNHID.Width = 30;
            // 
            // colEmpID
            // 
            this.colEmpID.HeaderText = "Emp ID";
            this.colEmpID.Name = "colEmpID";
            // 
            // colHDate
            // 
            this.colHDate.HeaderText = "Date";
            this.colHDate.Name = "colHDate";
            this.colHDate.ReadOnly = true;
            this.colHDate.Width = 150;
            // 
            // colHCategory
            // 
            this.colHCategory.HeaderText = "Category";
            this.colHCategory.Name = "colHCategory";
            this.colHCategory.Width = 150;
            // 
            // colLeaveCatID
            // 
            this.colLeaveCatID.DataPropertyName = "LEAVE_CAT_ID";
            this.colLeaveCatID.HeaderText = "Levae CatID";
            this.colLeaveCatID.Name = "colLeaveCatID";
            this.colLeaveCatID.Visible = false;
            // 
            // colDBSL
            // 
            this.colDBSL.HeaderText = "DBSL";
            this.colDBSL.Name = "colDBSL";
            this.colDBSL.ReadOnly = true;
            this.colDBSL.Visible = false;
            // 
            // dtPkrFormDate
            // 
            this.dtPkrFormDate.Location = new System.Drawing.Point(101, 81);
            this.dtPkrFormDate.Name = "dtPkrFormDate";
            this.dtPkrFormDate.Size = new System.Drawing.Size(139, 20);
            this.dtPkrFormDate.TabIndex = 2;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(47, 113);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(20, 13);
            this.lblDateTo.TabIndex = 99;
            this.lblDateTo.Text = "To";
            // 
            // dtPkrToDate
            // 
            this.dtPkrToDate.Location = new System.Drawing.Point(101, 108);
            this.dtPkrToDate.Name = "dtPkrToDate";
            this.dtPkrToDate.Size = new System.Drawing.Size(139, 20);
            this.dtPkrToDate.TabIndex = 3;
            // 
            // cbxLeaveCat
            // 
            this.cbxLeaveCat.FormattingEnabled = true;
            this.cbxLeaveCat.Location = new System.Drawing.Point(101, 51);
            this.cbxLeaveCat.Name = "cbxLeaveCat";
            this.cbxLeaveCat.Size = new System.Drawing.Size(139, 21);
            this.cbxLeaveCat.TabIndex = 1;
            this.cbxLeaveCat.Text = "--- Select Type ---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 107;
            this.label2.Text = "Leave Cat:";
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(101, 22);
            this.txtEmpID.Mask = "000000";
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(139, 20);
            this.txtEmpID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Emp ID:";
            // 
            // ckEnableSearch
            // 
            this.ckEnableSearch.AutoSize = true;
            this.ckEnableSearch.Location = new System.Drawing.Point(13, 161);
            this.ckEnableSearch.Name = "ckEnableSearch";
            this.ckEnableSearch.Size = new System.Drawing.Size(96, 17);
            this.ckEnableSearch.TabIndex = 8;
            this.ckEnableSearch.Text = "Enable Search";
            this.ckEnableSearch.UseVisualStyleBackColor = true;
            this.ckEnableSearch.CheckedChanged += new System.EventHandler(this.ckEnableSearch_CheckedChanged);
            // 
            // gbxSerachOption
            // 
            this.gbxSerachOption.Controls.Add(this.cbxSrcLvCat);
            this.gbxSerachOption.Controls.Add(this.label5);
            this.gbxSerachOption.Controls.Add(this.txtSrcID);
            this.gbxSerachOption.Controls.Add(this.btnSearch);
            this.gbxSerachOption.Controls.Add(this.lblDate);
            this.gbxSerachOption.Controls.Add(this.dtpFstDate);
            this.gbxSerachOption.Controls.Add(this.label4);
            this.gbxSerachOption.Controls.Add(this.label3);
            this.gbxSerachOption.Location = new System.Drawing.Point(9, 184);
            this.gbxSerachOption.Name = "gbxSerachOption";
            this.gbxSerachOption.Size = new System.Drawing.Size(477, 89);
            this.gbxSerachOption.TabIndex = 121;
            this.gbxSerachOption.TabStop = false;
            this.gbxSerachOption.Text = "Search Option";
            // 
            // cbxSrcLvCat
            // 
            this.cbxSrcLvCat.FormattingEnabled = true;
            this.cbxSrcLvCat.Location = new System.Drawing.Point(253, 22);
            this.cbxSrcLvCat.Name = "cbxSrcLvCat";
            this.cbxSrcLvCat.Size = new System.Drawing.Size(139, 21);
            this.cbxSrcLvCat.TabIndex = 2;
            this.cbxSrcLvCat.Text = "--- Select Type ---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 159;
            this.label5.Text = "Leave Cat:";
            // 
            // txtSrcID
            // 
            this.txtSrcID.Location = new System.Drawing.Point(81, 54);
            this.txtSrcID.Mask = "000000";
            this.txtSrcID.Name = "txtSrcID";
            this.txtSrcID.Size = new System.Drawing.Size(79, 20);
            this.txtSrcID.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(253, 56);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(89, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(160, 29);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 13);
            this.lblDate.TabIndex = 156;
            // 
            // dtpFstDate
            // 
            this.dtpFstDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFstDate.Location = new System.Drawing.Point(81, 25);
            this.dtpFstDate.Name = "dtpFstDate";
            this.dtpFstDate.Size = new System.Drawing.Size(78, 20);
            this.dtpFstDate.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 101;
            this.label3.Text = "Search ID";
            // 
            // frmLeaveEntry2nd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 626);
            this.Controls.Add(this.ckEnableSearch);
            this.Controls.Add(this.gbxSerachOption);
            this.Controls.Add(this.txtEmpID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxLeaveCat);
            this.Controls.Add(this.dtPkrToDate);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtPkrFormDate);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.lblDateForm);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveAndUpdate);
            this.Name = "frmLeaveEntry2nd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSetHolyDay_FormClosed);
            this.Load += new System.EventHandler(this.FrmData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gbxSerachOption.ResumeLayout(false);
            this.gbxSerachOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSaveAndUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblDateForm;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DateTimePicker dtPkrFormDate;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtPkrToDate;
        private System.Windows.Forms.ComboBox cbxLeaveCat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtEmpID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckEnableSearch;
        private System.Windows.Forms.GroupBox gbxSerachOption;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpFstDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtSrcID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeaveCatID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDBSL;
        private System.Windows.Forms.ComboBox cbxSrcLvCat;
        private System.Windows.Forms.Label label5;
    }
}