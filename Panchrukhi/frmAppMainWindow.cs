using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Panchrukhi.DAO;
using Panchrukhi.Report;
using Panchrukhi.Basic_Settings;
using System.IO;

namespace Panchrukhi
{
    public partial class frmMainWindow : Form
    {
        int UserType;
        public frmMainWindow(int uType)
        {
            this.UserType = uType;            // values preserved after close                   
            InitializeComponent();
            if (UserType == 2)
            {
                personalDataToolStripMenuItem.Visible       = false;
                setHolyDayToolStripMenuItem.Visible         = false;
                basicSettingToolStripMenuItem.Visible       = false;
                attendanceSettingsToolStripMenuItem.Visible = false;
                promoteAndDemoteToolStripMenuItem.Visible   = false;
            }
            if (UserType == 3)
            {
                superAdminToolStripMenuItem.Visible = true;
            }
            else
            {
                superAdminToolStripMenuItem.Visible = false;
            }
        }


        DatabaseConnection DBConn = new DatabaseConnection();





        /*
            This function is called when window is Load
            function Work for:
                1. Student Promotion form only for school
                2. Get Company info from fixed table call function
                3. Set Company Name in form title
                4. Set Company Name as a big header.
                5. Get picture byte from table.
                6. hold it in memory stream
                7. set picture inside main window as logo.
        */
        private void frmMainWindow_Load(object sender, EventArgs e)
        {
            promoteAndDemoteToolStripMenuItem.Enabled = false; // 1
            DataRow dr = DBConn.getCompanyNameAndAddress();    // 2 
            this.Text = dr["VCOMPANY_NAME"].ToString();        // 3
            lblCompanyName.Text = dr["VCOMPANY_NAME"].ToString(); // 4
            byte[] picObj = (byte[])dr["BLOGO"]; // 5
            MemoryStream ms = new MemoryStream(picObj); // 6
            pbxLogo.Image = Image.FromStream(ms); // 7
        }





        /*
            Open window to entry employee designation.
            Designation menu is under basic settings.
            List of designation is display in employee entry form(frmPData.cs)
        */

        private void designationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDesigData fDesig = new frmDesigData();
            fDesig.Owner = this;
            fDesig.Show();
        }



        /*
            Open window to Register the Eployee.
            Update and Delete Employee.
        */
        private void tstripAddEditPerson_Click(object sender, EventArgs e)
        {
            frmPData fPerson = new frmPData();
            fPerson.Owner = this;
            fPerson.Show(this);
        }




        private void tstripSlots_Click(object sender, EventArgs e)
        {
            frmSlotData fSlot = new frmSlotData();
            fSlot.Owner = this;
            fSlot.Show();
        }




        private void tstripCategories_Click(object sender, EventArgs e)
        {
            frmCategory fCat = new frmCategory();
            fCat.Owner = this;
            fCat.Show();
        }



        /*
            for school this is class entry form.
            for company this is department entry from.
            this class / department will display inside employee entry form.
        */
        private void tstripClasses_Click(object sender, EventArgs e)
        {
            frmClassData fClsData = new frmClassData();
            fClsData.Owner = this;
            fClsData.Show();
        }



        /*
            the form is used for school, to entry
            like B section, A Section.
         */
        private void tstripSections_Click(object sender, EventArgs e)
        {
            frmSectionData fSdata = new frmSectionData();
            fSdata.Owner = this;
            fSdata.Show();
        }




        private void tstripCheckIPStatus(object sender, EventArgs e)
        {
            IPStatusCheck fIPCheck = new IPStatusCheck();
            fIPCheck.Owner = this;
            fIPCheck.Show();
        }




        private void systemAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAttendanceProcess fSys = new frmAttendanceProcess();
            fSys.Owner = this;
            fSys.Show();
        }



        /*
            the function is use to active mid child in frmMain
            to load flash screen.
        */
        private void frmMainWindow_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 1 ) {
                foreach (Form childForm in this.MdiChildren) {
                    if ( childForm != this.ActiveMdiChild ) {
                        childForm.Close();
                        return;
                    }
                }
            }
        }



        /*
             this form is design to display employee wise 
             attendance report using date range.
        */
        private void tstripPersonalInfo_Click(object sender, EventArgs e)
        {
            frmReportPersonalInfo fPrsnRprt = new frmReportPersonalInfo();
            fPrsnRprt.Owner = this;
            fPrsnRprt.Show();
        }







        /*
            this form is use to entry holiday catagory, it's not like weekend.
            like eid, puja, pohela boishak etc.
            this list will display inside the comob of Set Holiday form.
        */
        private void addHolyCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHolidayCat fHcat = new frmHolidayCat();
            fHcat.Owner = this;
            fHcat.Show();
        }




        /*
            menu > Holidays > Holiday
        */
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetHoliday fSHDay = new frmSetHoliday();
            fSHDay.Owner = this;
            fSHDay.Show();
        }





        /*
             Open window to set weekend holidays.
             one of the complex most form.
             feature:
                1. set general weekend for all employee
                2. set employee ID wise special weekend
                3. under holiday tab.
                4. 
        */

        private void addWeekendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetWeekend fSWknd = new frmSetWeekend();
            fSWknd.Owner = this;
            fSWknd.Show();
        }





        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }






        /*
            Add new machine is use for enlisted the attendance device 
            using IP address.
        */
        private void addNewMachineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddNewMachine fAddM = new frmAddNewMachine();
            fAddM.Owner = this;
            fAddM.Show();
        }



        /*
             IPStatusCheck form is used to ping IP
             address using button click. Menu > Machine Info > IP Configure
        */
        private void iPConfigureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPStatusCheck fIPCheck = new IPStatusCheck();
            fIPCheck.Owner = this;
            fIPCheck.Show();
        }




        /*
            process attendance form is use to get data 
            form attendance device. one of the most complex form.
            Menu > Attendance > processAttendance.
        */
        private void processAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAttendanceProcess fSys = new frmAttendanceProcess();
            fSys.Owner = this;
            fSys.Show();
        }




        /*
            manual attendance form is use to input employee attendance manually,
            for those who are miss punch attendance in device.
        */
        private void manualAttendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManualAttendance fAttndData = new frmManualAttendance();
            fAttndData.Owner = this;
            fAttndData.Show();
        }




        /*
            student promotion only use for panchrukhi school.
            form is used to promot student one class to another.
            currently the form is disabled.
        */
        private void promotionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmPersonalPromotion fpreP = new frmPersonalPromotion();
            fpreP.Owner = this;
            fpreP.Show();
        }





        /*
            Open window to entry employee CLSL
            Employee Leave Entry form is under Report Menu            
        */
        private void cLSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLeaveEntry2nd frmCL = new frmLeaveEntry2nd();
            frmCL.Owner = this;
            frmCL.Show();
        }






        /*
            Open window to entry employee leave Settings
            Leave settings menu is under Basic Settings
            List of Leave is display inside combo of frmLeaveEntry2nd.cs form
        */
        private void leaveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLeaveSettings frmLvsttng = new frmLeaveSettings();
            frmLvsttng.Owner = this;
            frmLvsttng.Show();
        }





        /*
            Open Salary form. it is one of the most
            complex form of application.
            use to salary calculation, payslip and salarysheet
        */
        private void salaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frmSalary frmLvsttng = new frmSalary();
            frmLvsttng.Owner = this;
            frmLvsttng.Show();
        }






        /*
             Salary Deduction form use to entry catagory wise
             Deducted Amount of salary.
             form is currently not used.
        */

        private void salaryDeductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // frmSalaryDeduction frmSalDud = new frmSalaryDeduction();
           // frmSalDud.Owner = this;
           // frmSalDud.Show();
        }






        /* 
            Under Super Admin Menu Settings will be open.
            Purpose:
                1. Reset Database, Delete All Data from Application.
                2. Change logo, company name and address option 
                3. To deploy application within minimum time.
        */

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInfo appInfo = new ApplicationInfo();
            appInfo.Owner = this;
            appInfo.Show();
        }
    }
}
