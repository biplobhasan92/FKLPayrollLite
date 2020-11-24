namespace Panchrukhi.Basic_Settings
{
    partial class frmLeaveSettings
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtLeaveName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeaveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTtlDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSaveAndUpdate = new System.Windows.Forms.Button();
            this.txtLeaveTtlDay = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeaveTtlDay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leave Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Day";
            // 
            // txtLeaveName
            // 
            this.txtLeaveName.Location = new System.Drawing.Point(112, 35);
            this.txtLeaveName.Name = "txtLeaveName";
            this.txtLeaveName.Size = new System.Drawing.Size(129, 20);
            this.txtLeaveName.TabIndex = 97;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSL,
            this.colLeaveName,
            this.colTtlDay});
            this.dataGridView1.Location = new System.Drawing.Point(12, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(373, 150);
            this.dataGridView1.TabIndex = 99;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // colSL
            // 
            this.colSL.DataPropertyName = "SL";
            this.colSL.HeaderText = "SL";
            this.colSL.Name = "colSL";
            this.colSL.ReadOnly = true;
            // 
            // colLeaveName
            // 
            this.colLeaveName.DataPropertyName = "LEAVE_NAME";
            this.colLeaveName.HeaderText = "Leave Name";
            this.colLeaveName.Name = "colLeaveName";
            this.colLeaveName.ReadOnly = true;
            // 
            // colTtlDay
            // 
            this.colTtlDay.DataPropertyName = "TOTAL_DAYS";
            this.colTtlDay.HeaderText = "Total Day";
            this.colTtlDay.Name = "colTtlDay";
            this.colTtlDay.ReadOnly = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(296, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 23);
            this.btnClose.TabIndex = 103;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(296, 81);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 23);
            this.btnDelete.TabIndex = 102;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(296, 50);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(89, 23);
            this.btnReset.TabIndex = 101;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveAndUpdate
            // 
            this.btnSaveAndUpdate.Location = new System.Drawing.Point(296, 20);
            this.btnSaveAndUpdate.Name = "btnSaveAndUpdate";
            this.btnSaveAndUpdate.Size = new System.Drawing.Size(89, 23);
            this.btnSaveAndUpdate.TabIndex = 100;
            this.btnSaveAndUpdate.Text = "Save";
            this.btnSaveAndUpdate.UseVisualStyleBackColor = true;
            this.btnSaveAndUpdate.Click += new System.EventHandler(this.btnSaveAndUpdate_Click);
            // 
            // txtLeaveTtlDay
            // 
            this.txtLeaveTtlDay.Location = new System.Drawing.Point(112, 71);
            this.txtLeaveTtlDay.Name = "txtLeaveTtlDay";
            this.txtLeaveTtlDay.Size = new System.Drawing.Size(129, 20);
            this.txtLeaveTtlDay.TabIndex = 104;
            // 
            // frmLeaveSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 317);
            this.Controls.Add(this.txtLeaveTtlDay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveAndUpdate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtLeaveName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmLeaveSettings";
            this.Text = "frmLeaveSettings";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeaveTtlDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLeaveName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSaveAndUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeaveName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTtlDay;
        private System.Windows.Forms.NumericUpDown txtLeaveTtlDay;
    }
}