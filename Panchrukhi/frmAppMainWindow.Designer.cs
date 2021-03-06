namespace Panchrukhi
{
    partial class frmMainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainWindow));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.personalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripAddEditPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripDesignation = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripSections = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripSlots = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMachineToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iPConfigureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setHolyDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addHolyCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWeekendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cLSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attendanceSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processAttendanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualAttendanceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.promoteAndDemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promotionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripPersonalInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.salaryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.superAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.personalDataToolStripMenuItem,
            this.basicSettingToolStripMenuItem,
            this.setHolyDayToolStripMenuItem,
            this.attendanceSettingsToolStripMenuItem,
            this.promoteAndDemoteToolStripMenuItem,
            this.reportingToolStripMenuItem,
            this.toolStripMenuItem1,
            this.superAdminToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1110, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // this.mnuMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuMain_ItemClicked);
            // 
            // personalDataToolStripMenuItem
            // 
            this.personalDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstripAddEditPerson,
            this.tstripCategories,
            this.tstripDesignation,
            this.tstripClasses,
            this.tstripSections,
            this.tstripSlots,
            this.leaveSettingsToolStripMenuItem});
            this.personalDataToolStripMenuItem.Name = "personalDataToolStripMenuItem";
            this.personalDataToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.personalDataToolStripMenuItem.Text = "Basic Settings";
            // 
            // tstripAddEditPerson
            // 
            this.tstripAddEditPerson.Name = "tstripAddEditPerson";
            this.tstripAddEditPerson.Size = new System.Drawing.Size(149, 22);
            this.tstripAddEditPerson.Text = "Person Data";
            this.tstripAddEditPerson.Click += new System.EventHandler(this.tstripAddEditPerson_Click);
            // 
            // tstripCategories
            // 
            this.tstripCategories.Name = "tstripCategories";
            this.tstripCategories.Size = new System.Drawing.Size(149, 22);
            this.tstripCategories.Text = "Category";
            this.tstripCategories.Click += new System.EventHandler(this.tstripCategories_Click);
            // 
            // tstripDesignation
            // 
            this.tstripDesignation.Name = "tstripDesignation";
            this.tstripDesignation.Size = new System.Drawing.Size(149, 22);
            this.tstripDesignation.Text = "Designation";
            this.tstripDesignation.Click += new System.EventHandler(this.designationToolStripMenuItem_Click);
            // 
            // tstripClasses
            // 
            this.tstripClasses.Name = "tstripClasses";
            this.tstripClasses.Size = new System.Drawing.Size(149, 22);
            this.tstripClasses.Text = "Department";
            this.tstripClasses.Click += new System.EventHandler(this.tstripClasses_Click);
            // 
            // tstripSections
            // 
            this.tstripSections.Name = "tstripSections";
            this.tstripSections.Size = new System.Drawing.Size(149, 22);
            this.tstripSections.Text = "Section";
            this.tstripSections.Click += new System.EventHandler(this.tstripSections_Click);
            // 
            // tstripSlots
            // 
            this.tstripSlots.Name = "tstripSlots";
            this.tstripSlots.Size = new System.Drawing.Size(149, 22);
            this.tstripSlots.Text = "Shift";
            this.tstripSlots.Click += new System.EventHandler(this.tstripSlots_Click);
            // 
            // leaveSettingsToolStripMenuItem
            // 
            this.leaveSettingsToolStripMenuItem.Name = "leaveSettingsToolStripMenuItem";
            this.leaveSettingsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.leaveSettingsToolStripMenuItem.Text = "Leave Settings";
            this.leaveSettingsToolStripMenuItem.Click += new System.EventHandler(this.leaveSettingsToolStripMenuItem_Click);
            // 
            // basicSettingToolStripMenuItem
            // 
            this.basicSettingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewMachineToolStripMenuItem1,
            this.iPConfigureToolStripMenuItem});
            this.basicSettingToolStripMenuItem.Name = "basicSettingToolStripMenuItem";
            this.basicSettingToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.basicSettingToolStripMenuItem.Text = "Machine Info";
            // 
            // addNewMachineToolStripMenuItem1
            // 
            this.addNewMachineToolStripMenuItem1.Name = "addNewMachineToolStripMenuItem1";
            this.addNewMachineToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.addNewMachineToolStripMenuItem1.Text = "Add New Machine";
            this.addNewMachineToolStripMenuItem1.Click += new System.EventHandler(this.addNewMachineToolStripMenuItem1_Click);
            // 
            // iPConfigureToolStripMenuItem
            // 
            this.iPConfigureToolStripMenuItem.Name = "iPConfigureToolStripMenuItem";
            this.iPConfigureToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.iPConfigureToolStripMenuItem.Text = "IP Configure";
            this.iPConfigureToolStripMenuItem.Click += new System.EventHandler(this.iPConfigureToolStripMenuItem_Click);
            // 
            // setHolyDayToolStripMenuItem
            // 
            this.setHolyDayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addHolyCategoryToolStripMenuItem,
            this.addToolStripMenuItem,
            this.addWeekendToolStripMenuItem,
            this.cLSLToolStripMenuItem});
            this.setHolyDayToolStripMenuItem.Name = "setHolyDayToolStripMenuItem";
            this.setHolyDayToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.setHolyDayToolStripMenuItem.Text = "Holidays";
            // 
            // addHolyCategoryToolStripMenuItem
            // 
            this.addHolyCategoryToolStripMenuItem.Name = "addHolyCategoryToolStripMenuItem";
            this.addHolyCategoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addHolyCategoryToolStripMenuItem.Text = "Holiday Category";
            this.addHolyCategoryToolStripMenuItem.Click += new System.EventHandler(this.addHolyCategoryToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addToolStripMenuItem.Text = "Holiday";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addWeekendToolStripMenuItem
            // 
            this.addWeekendToolStripMenuItem.Name = "addWeekendToolStripMenuItem";
            this.addWeekendToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addWeekendToolStripMenuItem.Text = "Weekend";
            this.addWeekendToolStripMenuItem.Click += new System.EventHandler(this.addWeekendToolStripMenuItem_Click);
            // 
            // cLSLToolStripMenuItem
            // 
            this.cLSLToolStripMenuItem.Name = "cLSLToolStripMenuItem";
            this.cLSLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.cLSLToolStripMenuItem.Text = "Set CLSL";
            this.cLSLToolStripMenuItem.Click += new System.EventHandler(this.cLSLToolStripMenuItem_Click);
            // 
            // attendanceSettingsToolStripMenuItem
            // 
            this.attendanceSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processAttendanceToolStripMenuItem,
            this.manualAttendanceToolStripMenuItem1});
            this.attendanceSettingsToolStripMenuItem.Name = "attendanceSettingsToolStripMenuItem";
            this.attendanceSettingsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.attendanceSettingsToolStripMenuItem.Text = "Attendance";
            // 
            // processAttendanceToolStripMenuItem
            // 
            this.processAttendanceToolStripMenuItem.Name = "processAttendanceToolStripMenuItem";
            this.processAttendanceToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.processAttendanceToolStripMenuItem.Text = "Process Attendance";
            this.processAttendanceToolStripMenuItem.Click += new System.EventHandler(this.processAttendanceToolStripMenuItem_Click);
            // 
            // manualAttendanceToolStripMenuItem1
            // 
            this.manualAttendanceToolStripMenuItem1.Name = "manualAttendanceToolStripMenuItem1";
            this.manualAttendanceToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.manualAttendanceToolStripMenuItem1.Text = "Manual Attendance";
            this.manualAttendanceToolStripMenuItem1.Click += new System.EventHandler(this.manualAttendanceToolStripMenuItem1_Click);
            // 
            // promoteAndDemoteToolStripMenuItem
            // 
            this.promoteAndDemoteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.promotionToolStripMenuItem});
            this.promoteAndDemoteToolStripMenuItem.Name = "promoteAndDemoteToolStripMenuItem";
            this.promoteAndDemoteToolStripMenuItem.Size = new System.Drawing.Size(135, 20);
            this.promoteAndDemoteToolStripMenuItem.Text = "Promote And Demote";
            // 
            // promotionToolStripMenuItem
            // 
            this.promotionToolStripMenuItem.Name = "promotionToolStripMenuItem";
            this.promotionToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.promotionToolStripMenuItem.Text = "Promotion";
            this.promotionToolStripMenuItem.Click += new System.EventHandler(this.promotionToolStripMenuItem_Click_1);
            // 
            // reportingToolStripMenuItem
            // 
            this.reportingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstripPersonalInfo,
            this.salaryReportToolStripMenuItem});
            this.reportingToolStripMenuItem.Name = "reportingToolStripMenuItem";
            this.reportingToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportingToolStripMenuItem.Text = "Report";
            // 
            // tstripPersonalInfo
            // 
            this.tstripPersonalInfo.Name = "tstripPersonalInfo";
            this.tstripPersonalInfo.Size = new System.Drawing.Size(152, 22);
            this.tstripPersonalInfo.Text = "Person Info";
            this.tstripPersonalInfo.Click += new System.EventHandler(this.tstripPersonalInfo_Click);
            // 
            // salaryReportToolStripMenuItem
            // 
            this.salaryReportToolStripMenuItem.Name = "salaryReportToolStripMenuItem";
            this.salaryReportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salaryReportToolStripMenuItem.Text = "Salary Report";
            this.salaryReportToolStripMenuItem.Click += new System.EventHandler(this.salaryReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // superAdminToolStripMenuItem
            // 
            this.superAdminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.superAdminToolStripMenuItem.Name = "superAdminToolStripMenuItem";
            this.superAdminToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.superAdminToolStripMenuItem.Text = "Super Admin";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 65.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCompanyName.Location = new System.Drawing.Point(227, 202);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(0, 100);
            this.lblCompanyName.TabIndex = 17;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Location = new System.Drawing.Point(64, 193);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(112, 117);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogo.TabIndex = 18;
            this.pbxLogo.TabStop = false;
            // 
            // frmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 521);
            this.Controls.Add(this.pbxLogo);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem personalDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tstripAddEditPerson;
        private System.Windows.Forms.ToolStripMenuItem tstripCategories;
        private System.Windows.Forms.ToolStripMenuItem tstripDesignation;
        private System.Windows.Forms.ToolStripMenuItem tstripSlots;
        private System.Windows.Forms.ToolStripMenuItem tstripSections;
        private System.Windows.Forms.ToolStripMenuItem tstripClasses;
        private System.Windows.Forms.ToolStripMenuItem reportingToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tstripPersonalInfo;
        private System.Windows.Forms.ToolStripMenuItem setHolyDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addHolyCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWeekendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMachineToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iPConfigureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attendanceSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processAttendanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualAttendanceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem promoteAndDemoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promotionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cLSLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salaryReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem superAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.PictureBox pbxLogo;
    }
}