namespace Panchrukhi
{
    partial class frmManualAttendance
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
            this.lblAttnTime = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSlotName = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colNATTENDSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVEMPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDATTENDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTINTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNATTENTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colREMARKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNAMFLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAttendDate = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoIN = new System.Windows.Forms.RadioButton();
            this.rdoOut = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAttendDate = new System.Windows.Forms.DateTimePicker();
            this.txtEmpID = new System.Windows.Forms.MaskedTextBox();
            this.txtAttendTime = new System.Windows.Forms.DateTimePicker();
            this.gbxSerachOption = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpLstDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFstDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.cbxSearch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gbxSerachOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(396, 90);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 23);
            this.btnDelete.TabIndex = 73;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(396, 63);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(89, 23);
            this.btnReset.TabIndex = 72;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnSaveAndUpdate
            // 
            this.btnSaveAndUpdate.Location = new System.Drawing.Point(396, 36);
            this.btnSaveAndUpdate.Name = "btnSaveAndUpdate";
            this.btnSaveAndUpdate.Size = new System.Drawing.Size(89, 23);
            this.btnSaveAndUpdate.TabIndex = 71;
            this.btnSaveAndUpdate.Text = "Save";
            this.btnSaveAndUpdate.UseVisualStyleBackColor = true;
            this.btnSaveAndUpdate.Click += new System.EventHandler(this.BtnSaveAndUpdate_Click);
            // 
            // lblAttnTime
            // 
            this.lblAttnTime.AutoSize = true;
            this.lblAttnTime.Location = new System.Drawing.Point(92, 95);
            this.lblAttnTime.Name = "lblAttnTime";
            this.lblAttnTime.Size = new System.Drawing.Size(30, 13);
            this.lblAttnTime.TabIndex = 68;
            this.lblAttnTime.Text = "Time";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(396, 117);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 23);
            this.btnClose.TabIndex = 94;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnFrmSlotClose_Click);
            // 
            // lblSlotName
            // 
            this.lblSlotName.AutoSize = true;
            this.lblSlotName.Location = new System.Drawing.Point(92, 40);
            this.lblSlotName.Name = "lblSlotName";
            this.lblSlotName.Size = new System.Drawing.Size(44, 13);
            this.lblSlotName.TabIndex = 95;
            this.lblSlotName.Text = "EMP ID";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNATTENDSL,
            this.colVEMPID,
            this.colDATTENDATE,
            this.colTINTIME,
            this.colNATTENTYPE,
            this.colREMARKS,
            this.colNAMFLAG});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(4, 275);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(546, 465);
            this.dataGridView.TabIndex = 97;
            this.dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // colNATTENDSL
            // 
            this.colNATTENDSL.HeaderText = "SL";
            this.colNATTENDSL.Name = "colNATTENDSL";
            this.colNATTENDSL.Visible = false;
            // 
            // colVEMPID
            // 
            this.colVEMPID.HeaderText = "Emp ID";
            this.colVEMPID.Name = "colVEMPID";
            this.colVEMPID.Width = 70;
            // 
            // colDATTENDATE
            // 
            this.colDATTENDATE.HeaderText = "Date";
            this.colDATTENDATE.Name = "colDATTENDATE";
            this.colDATTENDATE.ReadOnly = true;
            this.colDATTENDATE.Width = 70;
            // 
            // colTINTIME
            // 
            this.colTINTIME.HeaderText = "Time";
            this.colTINTIME.MinimumWidth = 6;
            this.colTINTIME.Name = "colTINTIME";
            this.colTINTIME.ReadOnly = true;
            this.colTINTIME.Width = 70;
            // 
            // colNATTENTYPE
            // 
            this.colNATTENTYPE.HeaderText = "Type";
            this.colNATTENTYPE.Name = "colNATTENTYPE";
            this.colNATTENTYPE.Width = 70;
            // 
            // colREMARKS
            // 
            this.colREMARKS.HeaderText = "REMARKS";
            this.colREMARKS.Name = "colREMARKS";
            this.colREMARKS.Width = 120;
            // 
            // colNAMFLAG
            // 
            this.colNAMFLAG.HeaderText = "Flag";
            this.colNAMFLAG.Name = "colNAMFLAG";
            this.colNAMFLAG.Width = 50;
            // 
            // lblAttendDate
            // 
            this.lblAttendDate.AutoSize = true;
            this.lblAttendDate.Location = new System.Drawing.Point(92, 68);
            this.lblAttendDate.Name = "lblAttendDate";
            this.lblAttendDate.Size = new System.Drawing.Size(30, 13);
            this.lblAttendDate.TabIndex = 100;
            this.lblAttendDate.Text = "Date";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(184, 118);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(191, 20);
            this.txtRemarks.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Ref/Remarks";
            // 
            // rdoIN
            // 
            this.rdoIN.AutoSize = true;
            this.rdoIN.Location = new System.Drawing.Point(185, 13);
            this.rdoIN.Name = "rdoIN";
            this.rdoIN.Size = new System.Drawing.Size(36, 17);
            this.rdoIN.TabIndex = 102;
            this.rdoIN.TabStop = true;
            this.rdoIN.Text = "IN";
            this.rdoIN.UseVisualStyleBackColor = true;
            // 
            // rdoOut
            // 
            this.rdoOut.AutoSize = true;
            this.rdoOut.Location = new System.Drawing.Point(227, 13);
            this.rdoOut.Name = "rdoOut";
            this.rdoOut.Size = new System.Drawing.Size(48, 17);
            this.rdoOut.TabIndex = 103;
            this.rdoOut.TabStop = true;
            this.rdoOut.Text = "OUT";
            this.rdoOut.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 104;
            this.label1.Text = "Attndance Type";
            // 
            // txtAttendDate
            // 
            this.txtAttendDate.Location = new System.Drawing.Point(185, 66);
            this.txtAttendDate.Name = "txtAttendDate";
            this.txtAttendDate.Size = new System.Drawing.Size(190, 20);
            this.txtAttendDate.TabIndex = 105;
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(184, 39);
            this.txtEmpID.Mask = "000000";
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(191, 20);
            this.txtEmpID.TabIndex = 106;
            this.txtEmpID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpID_KeyPress);
            // 
            // txtAttendTime
            // 
            this.txtAttendTime.Location = new System.Drawing.Point(184, 91);
            this.txtAttendTime.Name = "txtAttendTime";
            this.txtAttendTime.Size = new System.Drawing.Size(190, 20);
            this.txtAttendTime.TabIndex = 107;
            // 
            // gbxSerachOption
            // 
            this.gbxSerachOption.Controls.Add(this.btnSearch);
            this.gbxSerachOption.Controls.Add(this.lblDate);
            this.gbxSerachOption.Controls.Add(this.dtpLstDate);
            this.gbxSerachOption.Controls.Add(this.dtpFstDate);
            this.gbxSerachOption.Controls.Add(this.label4);
            this.gbxSerachOption.Controls.Add(this.label3);
            this.gbxSerachOption.Controls.Add(this.txtSearchID);
            this.gbxSerachOption.Location = new System.Drawing.Point(95, 177);
            this.gbxSerachOption.Name = "gbxSerachOption";
            this.gbxSerachOption.Size = new System.Drawing.Size(390, 89);
            this.gbxSerachOption.TabIndex = 108;
            this.gbxSerachOption.TabStop = false;
            this.gbxSerachOption.Text = "Search Option";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(296, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 157;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(160, 29);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(22, 13);
            this.lblDate.TabIndex = 156;
            this.lblDate.Text = "TO";
            // 
            // dtpLstDate
            // 
            this.dtpLstDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLstDate.Location = new System.Drawing.Point(184, 25);
            this.dtpLstDate.Name = "dtpLstDate";
            this.dtpLstDate.Size = new System.Drawing.Size(78, 20);
            this.dtpLstDate.TabIndex = 155;
            // 
            // dtpFstDate
            // 
            this.dtpFstDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFstDate.Location = new System.Drawing.Point(81, 25);
            this.dtpFstDate.Name = "dtpFstDate";
            this.dtpFstDate.Size = new System.Drawing.Size(78, 20);
            this.dtpFstDate.TabIndex = 154;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Date";
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
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(81, 58);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(181, 20);
            this.txtSearchID.TabIndex = 0;
            // 
            // cbxSearch
            // 
            this.cbxSearch.AutoSize = true;
            this.cbxSearch.Location = new System.Drawing.Point(98, 153);
            this.cbxSearch.Name = "cbxSearch";
            this.cbxSearch.Size = new System.Drawing.Size(96, 17);
            this.cbxSearch.TabIndex = 109;
            this.cbxSearch.Text = "Enable Search";
            this.cbxSearch.UseVisualStyleBackColor = true;
            this.cbxSearch.CheckedChanged += new System.EventHandler(this.cbxSearch_CheckedChanged);
            // 
            // frmManualAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 743);
            this.Controls.Add(this.cbxSearch);
            this.Controls.Add(this.gbxSerachOption);
            this.Controls.Add(this.txtAttendTime);
            this.Controls.Add(this.txtEmpID);
            this.Controls.Add(this.txtAttendDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoOut);
            this.Controls.Add(this.rdoIN);
            this.Controls.Add(this.lblAttendDate);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.lblSlotName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveAndUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAttnTime);
            this.Name = "frmManualAttendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Attendance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAttendanceData_FormClosed);
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
        private System.Windows.Forms.Label lblAttnTime;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSlotName;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblAttendDate;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoIN;
        private System.Windows.Forms.RadioButton rdoOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNATTENDSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVEMPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDATTENDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTINTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNATTENTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colREMARKS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNAMFLAG;
        private System.Windows.Forms.DateTimePicker txtAttendDate;
        private System.Windows.Forms.MaskedTextBox txtEmpID;
        private System.Windows.Forms.DateTimePicker txtAttendTime;
        private System.Windows.Forms.GroupBox gbxSerachOption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpLstDate;
        private System.Windows.Forms.DateTimePicker dtpFstDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.CheckBox cbxSearch;
        private System.Windows.Forms.Button btnSearch;
    }
}