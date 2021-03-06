using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Panchrukhi.DAO;
using Panchrukhi.Report;
using CrystalDecisions.CrystalReports.Engine;

namespace Panchrukhi.Report
{
    public partial class frmSalary : Form
    {
        public frmSalary()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
        }




        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS;
        private DataTable DT;
        DataTable table = new DataTable();
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getSelectID = 0;
        int presents, absents, holidays, rowIndex;


        /*  ====== Remarks ======
            Salary Process button. 
            Basic work: to insert data of employee with dummy data inside salary processed table with date month.
            1. checked salary processed table is empty or not using date month.
                1.1. if have data display confirmation message that data will be deleted. user will click YesNo.
                1.2. if user click Yes: previous data will be deleted and insert raw data.
                1.3. if user click No : Process will Tarminated. 
            2. if salary processed table is empty then only insert will be happend.
        */

        private void button1_Click(object sender, EventArgs e)
        {
            int month = Convert.ToInt32(dateTimePicker1.Value.Month.ToString());
            int year  = Convert.ToInt32(dateTimePicker1.Value.Year.ToString());
            int days  = DateTime.DaysInMonth(year, month);
            // int basic = Convert.ToInt32(txtBasic.Value.ToString());
            string mmYY = dateTimePicker1.Value.ToString("yyyy/MM");
            // (1)
            if (DBConn.checkDataIfItUsedOtherTableStr("TBL_PROCESSED_SALARY", "YEAR_MONTH", dateTimePicker1.Value.ToString("yyyy/MM")))
            {
                // (1.1)
                if (MessageBox.Show("Do you want to Process ? ", " Your All Data of selected month will be deleted ", MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    // (1.2)
                    deleteBefourInsert(mmYY);
                    InsertSalPorcessData(days, mmYY);
                    MessageBox.Show("Process completed. Click Load Data.");
                }
                else
                {
                    // (1.3)
                    return;
                }
            }
            else
            {
                // (2)
                InsertSalPorcessData(days, mmYY);
                MessageBox.Show("Process completed. Click Load Data.");
            }
        }



        /*
            it's some time disturbing that salary process button remove 
            all update data for user mistake.
        */
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool b = checkBox1.Checked;
            if (!b){                
                //dateTimePicker1.Enabled = false;
                btnSalProcess.Enabled = false;
            }else{
                //dateTimePicker1.Enabled = true;
                btnSalProcess.Enabled = true;
            }
        }


        private void deleteBefourInsert(string yearMontrh)
        {
            try
            {
                string cmdText = " delete from TBL_PROCESSED_SALARY where YEAR_MONTH = '" + yearMontrh + "' ";
                DBConn.ExecutionQuery(cmdText);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem in DeleteTableRowInt()");
            }
        }




        private void InsertSalPorcessData(int days, string yearMonth)
        {
        
            // bool getHolidayDays = DBConn.checkDataIfItUsedOtherTableStr("TBL_HOLIDAY", "DDATE", dataGridView.Rows[i].Cells[1].Value.ToString());
            // bool getWeekend = DBConn.getWeekends(dataGridView.Rows[i].Cells[1].Value.ToString(), getID);
            // string getLvClSl = DBConn.getLeaveClsl(dataGridView.Rows[i].Cells[1].Value.ToString(), getID);


            int WeekendCount = 0;
            int HolidayCount = 0;
            int clCount = 0;
            int slCount = 0;
            int holidaysAnualLv = 0;

            DT = new DataTable();
            DS = new DataSet();
            string CommandText = @"SELECT
                                         P.PERSONID,
                                         P.VNAME,
                                         (select VCATEGORY from TBLCATEGORY where P.NCATID = TBLCATEGORY.NCATID) as CATEGORY,
                                         (select VDESIGNATIONNAME from TBLDESIGNATION d where d.NDESIGID = P.NDESIGID ) as Designation,
                                         (SELECT VCLASSNAME FROM TBLCLASS TC WHERE TC.NCLASSID = P.NCLASSID) as CLASS,
                                         (SELECT VSECTION   FROM TBLSECTION TS WHERE TS.NSECID = P.NSECID) as SECTION,
                                         (SELECT VSLOTNAME ||  '(' || VINTIME || '-' ||  VOUTTIME || ')'  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = P.NSLOTID)) as SHIFT,
                                         P.NSALARY,
                                         P.NBASIC,
                                         P.NHRENT,
                                         P.NMEDICAL,
                                         P.NTRANSPORT
                                    FROM
                                         TBLPERSON P order by P.NEMPID ;";
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DBConn.ExecutionQuery(CommandText);
            DS.Reset();
            DB.Fill(DS);
            dataGridView.Rows.Clear();            
            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DS.Tables[0].Rows)
                {                 
                    int weekend = getCountOfWeekendInMonthUsing(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());                    
                    int leaveCL = getCountOfCLInMnthUsingID(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());
                    int leaveSL = getCountOfSLInMnthUsingID(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());
                    int workedDays = days - weekend;
                    int salaryA   = Convert.ToInt32(dr["NSALARY"].ToString());
                    int basicSal  = Convert.ToInt32(dr["NBASIC"].ToString());
                    int hRnt = Convert.ToInt32(dr["NHRENT"].ToString());
                    int medical   = Convert.ToInt32(dr["NMEDICAL"].ToString());
                    int transport = Convert.ToInt32(dr["NTRANSPORT"].ToString());                    
                    SaveDataOfProcessedSalary( dr["PERSONID"].ToString(), workedDays, weekend, leaveCL, leaveSL, dateTimePicker1.Value.ToString("yyyy/MM"), salaryA, basicSal, hRnt, transport, medical);
                }
            }
        }





        /*
            Save Process Salary Dummy Data from Employee Table. 
        */
        public void SaveDataOfProcessedSalary(string empID, int workingDay, int holidays, int cl, int sl, string YEAR_MONTH, int sal, int basic, int hrnt, int trnsprt, int mdcl)
        {

            int presentDay = 0;
            if (cbxAttenEntry.Checked)
            {
                workingDay = Convert.ToInt32(txtMnulWorkingDay.Text.Trim());
                holidays = Convert.ToInt32(txtMnulHoliday.Text.Trim());
                presentDay = Convert.ToInt32(txtMnulPresentDay.Text.Trim());
            }
            else
            {
                presentDay = workingDay;
            }

            try
            {
                string cmdText = " insert into TBL_PROCESSED_SALARY(" +
                        "SL," +
                        "EMP_ID," +
                        "EMP_NAME," +
                        "EMP_DESIG,"+
                        "EMP_WORKING_DAYS,"+
                        "EMP_HOLIDAYS," +
                        "EMP_CASUAL_LEAVE," +
                        "EMP_SICK_LEAVE, " +
                        "EMP_PRESENT, " +
                        "EMP_ANNUAL_LEAVE, " +
                        "EMP_ABSENT, " +
                        "EMP_BASIC_SAL, " +
                        "EMP_HOUSE_RENT, " +
                        "EMP_TRANSPORT_ALLOW, " +
                        "EMP_MEDICAL_ALLOW, " +
                        "EMP_TOTAL_SAL, " +
                        "EMP_ABSENT_SAL_CUT, " +
                        "EMP_ADV_CUT, " +
                        "EMP_MOBILE_BILL, " +
                        "EMP_OTHERS_SAL_CUT, " +
                        "EMP_TAX, " +
                        "EMP_REVENUE_TICKET, " +
                        "EMP_TOTAL_CUT, " +
                        "EMP_TOTAL_GIVEN_SALARY, " +
                        "EMP_OTHERS_ALLOW, " +
                        "EMP_TOTAL_GIVEN_SALARY_AND_ALLOW, " +
                        "EMP_HOLIDAY_WORK, " +
                        "YEAR_MONTH " +
                    " ) " +
                     " values (" +
                         "(select CASE WHEN max(SL) >= 0 THEN max(SL) +1 ELSE 1 END FROM TBL_PROCESSED_SALARY), " +
                         "  '" + empID + "',     " +
                         "  (select p.VNAME from TBLPERSON p where p.PERSONID='"+empID+"'),  " +
                         "  (select d.VDESIGNATIONNAME from TBLDESIGNATION d where d.NDESIGID = (select p.NDESIGID from TBLPERSON p where p.PERSONID ='"+empID+"')), " +
                         "  " + workingDay + ", " +
                         "  " + holidays + ", " +
                         "  " + cl + ", "+
                         "  " + sl + ", "+
                         "  " + presentDay + ", " +
                         "  " + getCountOfALInMnthUsingID(YEAR_MONTH, empID) + ", " +
                         "  " + 0 + ", " +
                         "  " + basic + ", "+
                         "  " + hrnt + ", " +
                         "  " + trnsprt + ","+
                         "  " + mdcl + ", " +
                         "  " + sal + ","+
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 10 + "," +
                         "  " + 10 + "," +
                         "  " + (sal - 10) + ", " +
                         "  " + 0 + ", " +
                         "  " + (sal - 10) + ", " +
                         "  " + 0 + ", " +
                         "  '" + YEAR_MONTH + "' " +
                     " ) ";
                DBConn.ExecutionQuery(cmdText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }



        public void LoadGridData()
        {
            bool b = false;
            DT = new DataTable();
            DS = new DataSet();
            string CommandText = @" select d.* from TBL_PROCESSED_SALARY d where d.YEAR_MONTH = '"+ dateTimePicker1.Value.ToString("yyyy/MM") + "'; ";
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DBConn.ExecutionQuery(CommandText);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGridView.Rows.Clear();
            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSL.Index].Value = Convert.ToInt32(dr["SL"].ToString());
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value   = dr["EMP_ID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["EMP_NAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDesig.Index].Value   = dr["EMP_DESIG"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colWorkingDays.Index].Value   = dr["EMP_WORKING_DAYS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHolidays.Index].Value= dr["EMP_HOLIDAYS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHolidayWork.Index].Value = dr["EMP_HOLIDAY_WORK"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSalary.Index].Value  = dr["EMP_TOTAL_SAL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colPresent.Index].Value = dr["EMP_PRESENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCasualLeave.Index].Value = dr["EMP_CASUAL_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSickLeave.Index].Value = dr["EMP_SICK_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAnualLeave.Index].Value= dr["EMP_ANNUAL_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsent.Index].Value  = dr["EMP_ABSENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colBasicSal.Index].Value= dr["EMP_BASIC_SAL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHouseRent.Index].Value = dr["EMP_HOUSE_RENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTransport.Index].Value = dr["EMP_TRANSPORT_ALLOW"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMedical.Index].Value = dr["EMP_MEDICAL_ALLOW"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsentCut.Index].Value = dr["EMP_ABSENT_SAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAdvCut.Index].Value  = dr["EMP_ADV_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMobileBill.Index].Value= dr["EMP_MOBILE_BILL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colOthers.Index].Value  = dr["EMP_OTHERS_SAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTax.Index].Value = dr["EMP_TAX"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colRevenueTikit.Index].Value = dr["EMP_REVENUE_TICKET"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalCut.Index].Value = dr["EMP_TOTAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalGivenSal.Index].Value  = dr["EMP_TOTAL_GIVEN_SALARY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colOthersAllownce.Index].Value = dr["EMP_OTHERS_ALLOW"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colGivenSalAndallownce.Index].Value = dr["EMP_TOTAL_GIVEN_SALARY_AND_ALLOW"].ToString();
                }
            }
        }



        



        public int getCountOfWeekendInMonthUsing(string dateMonth, string empID)
        {
            DateTime now = Convert.ToDateTime(dateMonth);
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int counter = 0;
            for (DateTime date = Convert.ToDateTime(startDate); date.Date <= Convert.ToDateTime(endDate); date = date.AddDays(1))
            {
                if (DBConn.getWeekends(date.ToString("dd/MM/yyyy"), empID)) { counter++; }
            }
            return counter;
        }



        public int getCountOfCLInMnthUsingID(string dateMonth, string empID)
        {
            DateTime now = Convert.ToDateTime(dateMonth);
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int counter = 0;
            for (DateTime date = Convert.ToDateTime(startDate); date.Date <= Convert.ToDateTime(endDate); date = date.AddDays(1))
            {
                if (DBConn.getLeaveClsl(date.ToString("dd/MM/yyyy"), empID).Equals("CL")) { counter++; }
            }
            return counter;
        }



        public int getCountOfSLInMnthUsingID(string dateMonth, string empID)
        {
            DateTime now = Convert.ToDateTime(dateMonth);
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int counter = 0;
            for (DateTime date = Convert.ToDateTime(startDate); date.Date <= Convert.ToDateTime(endDate); date = date.AddDays(1))
            {
                if (DBConn.getLeaveClsl(date.ToString("dd/MM/yyyy"), empID).Equals("SL")) { counter++; }
            }
            return counter;
        }



        public int getCountOfALInMnthUsingID(string dateMonth, string empID)
        {
            DateTime now = Convert.ToDateTime(dateMonth);
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int counter = 0;
            for (DateTime date = Convert.ToDateTime(startDate); date.Date <= Convert.ToDateTime(endDate); date = date.AddDays(1))
            {
                if (DBConn.getLeaveClsl(date.ToString("dd/MM/yyyy"), empID).Equals("AL")) { counter++; }
            }
            return counter;
        }



        // DBConn.getWeekends(date.ToString("dd/MM/yyyy"), dr["PERSONID"].ToString());

        private void frmSalary_Load(object sender, EventArgs e)
        {
            // this.Owner.Enabled = false;
            // this.dataGridView.Columns["salary"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            btnSalProcess.Enabled = false;
            this.txtMnulWorkingDay.Enabled = this.txtMnulHoliday.Enabled = this.txtMnulPresentDay.Enabled = false;
        }



        private void frmSalary_FormClosed(object sender, FormClosedEventArgs e)
        {
            // this.Owner.Enabled = true;
        }




        // string sql = "select IFNULL(d.DEDUCTED_SALARY, 0) as mobileBill from TBL_SALARY_DEDUCTION d where d.EMP_ID = '11002' and substr(YEAR_MONTH,4,4)|| substr(YEAR_MONTH, 1, 2) = '202101' and d.CAT_ID = 1 ";
        private int getCatWiseAdvSalCut(string empID, string yearMonth, int catID)
        {
            int deducted_Amount = 0;
            try
            {
                string CommandText =
                            @"select IFNULL(d.DEDUCTED_SALARY, 0) as mobileBill from TBL_SALARY_DEDUCTION d where d.EMP_ID = '"+empID+ "' and YEAR_MONTH = '" + yearMonth+"' and d.CAT_ID = "+catID+" ";                
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DBConn.ExecutionQuery(CommandText);
                DS.Reset();
                DB.Fill(DS);
                holidays = calculateHolidays(empID, yearMonth);
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    deducted_Amount = Convert.ToInt32(dr["mobileBill"].ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkUserToLogIn()");
            }
            return deducted_Amount;
        }




        private int calculateAbsent(string empID, string yearMonth)
        {
            int year = Convert.ToInt32(yearMonth.Substring(0, 4));
            int month = Convert.ToInt32(yearMonth.Substring(4));
            int dayOfmonth = DateTime.DaysInMonth(year, month);
            presents = absents = holidays = 0;
            DT = new DataTable();
            DS = new DataSet();
            try{
                string CommandText =
                            @"Select
                                  distinct P.PERSONID ID,
                                  TA.DATTENDATE DATE,
                                  IFNULL(IT.INTIME,'') INTIME,
                                  IFNULL(OT.OUTTIME,'') OUTTIME
                             FROM
                                  TBLPERSON P JOIN TBLATTENDANCE_PROCESS_DATA TA
                             LEFT OUTER JOIN
                                  (SELECT VEMPID, VINOUTTIME INTIME, DATTENDATE from TBLATTENDANCE_PROCESS_DATA WHERE NATTENTYPE = 1) IT ON (IT.VEMPID = P.PERSONID AND IT.DATTENDATE = TA.DATTENDATE) 
                             LEFT OUTER JOIN
                                  (SELECT VEMPID, VINOUTTIME OUTTIME, DATTENDATE from TBLATTENDANCE_PROCESS_DATA WHERE NATTENTYPE = 5) OT ON (OT.VEMPID = P.PERSONID AND OT.DATTENDATE = TA.DATTENDATE) 
                             WHERE  
                                  substr(TA.DATTENDATE,7)||substr(TA.DATTENDATE,4,2) between '" + yearMonth + "' AND '" + yearMonth + "'  AND P.PERSONID IN ('" + empID + "')  AND P.NSTATUS == 1  ORDER BY TA.DATTENDATE;";
                
                // DBConn.ExecutionQuery(CommandText);
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DBConn.ExecutionQuery(CommandText);
                DS.Reset();
                DB.Fill(DS);
                holidays = calculateHolidays(empID, yearMonth);
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    bool absentCounted = false;
                    string getID = dr["ID"].ToString();
                    string strIntime = dr["INTIME"].ToString();
                    string strOuttime = dr["OUTTIME"].ToString();
                    if (strIntime.Equals("") || strOuttime.Equals(""))
                    {
                        absents++; absentCounted = true;
                    }
                    else
                    {
                        presents++;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(""+e.Message);
            }
            absents = (dayOfmonth - (holidays + presents))+absents;
            return absents;
        }


        // getCashPayData

        private void btnBankPay_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.Year.ToString();
            String Month = DateTime.Now.Month.ToString();
            DataSet ds1;
            if (cbxForCash.Checked==true)
            {
                ds1 = DBConn.getCashPayData(dateTimePicker1.Value.ToString("yyyy/MM"));
            }
            else{                
                ds1 = DBConn.getBankPayData(dateTimePicker1.Value.ToString("yyyy/MM"));
            }

            if (ds1==null) { return; }
            DS = new DataSet();
            DT = new DataTable();

            DT.Columns.Add("ID", typeof(string));
            DT.Columns.Add("Name", typeof(string));
            DT.Columns.Add("DESIG", typeof(string));
            DT.Columns.Add("ACCNO", typeof(string));
            DT.Columns.Add("BANKPAY", typeof(int));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    DT.Rows.Add(
                        dr1["ID"],
                        dr1["Name"],
                        dr1["DESIG"],
                        dr1["ACCNO"],
                        dr1["BANKPAY"]
                    );
                }
            }
            DS.Tables.Add(DT);
            frmCrystalReportViewer frm = new frmCrystalReportViewer();
            DataRow dr = DBConn.getCompanyNameAndAddress();

            if (cbxForCash.Checked==true)
            {
                Rpt_CashPaymentSheetAccount2ndPart cr = new Rpt_CashPaymentSheetAccount2ndPart();
                cr.SetDataSource(DT);
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(2, dateTimePicker1.Value.ToString("MMMM") + ", " + dateTimePicker1.Value.ToString("yyyy"));
                cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
                frm.crptViewer.ReportSource = cr;
            }
            else
            {
                Rpt_BankPaymentSheetAccount2ndPart cr = new Rpt_BankPaymentSheetAccount2ndPart();
                cr.SetDataSource(DT);
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(2, dateTimePicker1.Value.ToString("MMMM") + ", " + dateTimePicker1.Value.ToString("yyyy"));
                cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
                frm.crptViewer.ReportSource = cr;
            }            
            frm.crptViewer.Refresh();
            frm.Show();
        }




        /*
            adp = new SqlDataAdapter("select * from tbl_student", con);
            adp.Fill(ds);
            ReportDocument rd = new ReportDocument();
            rd.Load(@"~\CrystalReport1.rpt");
            rd.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rd;    
             
        */



        private void btn_print_Click(object sender, EventArgs e)
        {
                string year = DateTime.Now.Year.ToString();
                String Month = DateTime.Now.Month.ToString();            
                DS = new DataSet();
                DT = new DataTable();
                string str = "";
                DT.Columns.Add("ID", typeof(string));
                DT.Columns.Add("Emp ID", typeof(string));
                DT.Columns.Add("Emp Name", typeof(string));
                DT.Columns.Add("Category", typeof(string));
                DT.Columns.Add("Designation", typeof(string));
                DT.Columns.Add("Working Day", typeof(int));
                DT.Columns.Add("Holidays", typeof(int));
                DT.Columns.Add("Holiday work", typeof(string));
                DT.Columns.Add("Present", typeof(int));
                DT.Columns.Add("CL", typeof(int));
                DT.Columns.Add("SL", typeof(int));
                DT.Columns.Add("AL", typeof(int));
                DT.Columns.Add("absent", typeof(int));
                DT.Columns.Add("Basic", typeof(int));
                DT.Columns.Add("House Rent", typeof(int));
                DT.Columns.Add("Transport", typeof(int));
                DT.Columns.Add("Medical", typeof(int));
                DT.Columns.Add("Salary", typeof(int));
                DT.Columns.Add("Absent Cut", typeof(int));
                DT.Columns.Add("Advance Cut", typeof(int));
                DT.Columns.Add("Mobile Bill", typeof(int));
                DT.Columns.Add("Others Cut", typeof(int));
                DT.Columns.Add("Tax", typeof(int));
                DT.Columns.Add("Rev Ticket", typeof(int));
                DT.Columns.Add("Total Cut", typeof(int));
                DT.Columns.Add("Total Givent Sal", typeof(int));
                DT.Columns.Add("Others Allowanc", typeof(int));
                DT.Columns.Add("Total Givent Sal and Allow", typeof(int));

                if (dataGridView.Rows.Count > 0)
                { 
                    foreach (DataGridViewRow dgv in dataGridView.Rows)
                    {
                        // DataRow drr = DBConn.getEmpInfo(dgv.Cells[0].Value.ToString());
                        DT.Rows.Add(
                            dgv.Cells[0].Value,
                            dgv.Cells[1].Value,
                            dgv.Cells[2].Value,
                            dgv.Cells[3].Value,
                            dgv.Cells[4].Value,
                            dgv.Cells[5].Value,
                            dgv.Cells[6].Value,
                            dgv.Cells[7].Value,
                            dgv.Cells[8].Value,
                            dgv.Cells[9].Value,
                            dgv.Cells[10].Value,
                            dgv.Cells[11].Value,
                            dgv.Cells[12].Value,
                            dgv.Cells[13].Value,
                            dgv.Cells[14].Value,
                            dgv.Cells[15].Value,
                            dgv.Cells[16].Value,
                            dgv.Cells[17].Value,
                            dgv.Cells[18].Value,
                            dgv.Cells[19].Value,
                            dgv.Cells[20].Value,
                            dgv.Cells[21].Value,
                            dgv.Cells[22].Value,
                            dgv.Cells[23].Value,
                            dgv.Cells[24].Value,
                            dgv.Cells[25].Value,
                            dgv.Cells[26].Value,
                            dgv.Cells[27].Value
                        );
                    }
                    DS.Tables.Add(DT);
                    DS.WriteXmlSchema("MonthlySummaryReport.xml");
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                Rpt_SalarySheet_FKLSpinning_Bangla cr = new Rpt_SalarySheet_FKLSpinning_Bangla();
                DataRow dr = DBConn.getCompanyNameAndAddress();
                cr.SetDataSource(DS);                
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(2, dateTimePicker1.Value.ToString("MMMM") + ", " + dateTimePicker1.Value.ToString("yyyy"));
                cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
                frm.crptViewer.ReportSource = cr;
                frm.crptViewer.Refresh();
                frm.Show();
            }else{
                MessageBox.Show("Load Grid First");
            }
        }


        private void btn_payslip_Click(object sender, EventArgs e)
        {
            DS = new DataSet();
            DT = new DataTable();
            string str = "";
            DT.Columns.Add("ID", typeof(string));
            DT.Columns.Add("Emp ID", typeof(string));
            DT.Columns.Add("Emp Name", typeof(string));
            DT.Columns.Add("Category", typeof(string));
            DT.Columns.Add("Designation", typeof(string));
            DT.Columns.Add("Working Day", typeof(int));
            DT.Columns.Add("Holidays", typeof(int));
            DT.Columns.Add("Holiday work", typeof(string));
            DT.Columns.Add("Present", typeof(int));
            DT.Columns.Add("CL", typeof(int));
            DT.Columns.Add("SL", typeof(int));
            DT.Columns.Add("AL", typeof(int));
            DT.Columns.Add("absent", typeof(int));
            DT.Columns.Add("Basic", typeof(int));
            DT.Columns.Add("House Rent", typeof(int));
            DT.Columns.Add("Transport", typeof(int));
            DT.Columns.Add("Medical", typeof(int));
            DT.Columns.Add("Salary", typeof(int));
            DT.Columns.Add("Absent Cut", typeof(int));
            DT.Columns.Add("Advance Cut", typeof(int));
            DT.Columns.Add("Mobile Bill", typeof(int));
            DT.Columns.Add("Others Cut", typeof(int));
            DT.Columns.Add("Tax", typeof(int));
            DT.Columns.Add("Rev Ticket", typeof(int));
            DT.Columns.Add("Total Cut", typeof(int));
            DT.Columns.Add("Total Givent Sal", typeof(int));
            DT.Columns.Add("Others Allowanc", typeof(int));
            DT.Columns.Add("Total Givent Sal and Allow", typeof(int));
            if (dataGridView.Rows.Count > 0)
            {
                DataRow dr = DBConn.getCompanyNameAndAddress();
                foreach (DataGridViewRow dgv in dataGridView.Rows)
                {
                    // DataRow drr = DBConn.getEmpInfo(dgv.Cells[0].Value.ToString());
                    DT.Rows.Add(
                        dgv.Cells[0].Value,
                            dgv.Cells[1].Value,
                            dgv.Cells[2].Value,
                            dgv.Cells[3].Value,
                            dgv.Cells[4].Value,
                            dgv.Cells[5].Value,
                            dgv.Cells[6].Value,
                            dgv.Cells[7].Value,
                            dgv.Cells[8].Value,
                            dgv.Cells[9].Value,
                            dgv.Cells[10].Value,
                            dgv.Cells[11].Value,
                            dgv.Cells[12].Value,
                            dgv.Cells[13].Value,
                            dgv.Cells[14].Value,
                            dgv.Cells[15].Value,
                            dgv.Cells[16].Value,
                            dgv.Cells[17].Value,
                            dgv.Cells[18].Value,
                            dgv.Cells[19].Value,
                            dgv.Cells[20].Value,
                            dgv.Cells[21].Value,
                            dgv.Cells[22].Value,
                            dgv.Cells[23].Value,
                            dgv.Cells[24].Value,
                            dgv.Cells[25].Value,
                            dgv.Cells[26].Value,
                            dgv.Cells[27].Value
                    );
                }
                DS.Tables.Add(DT);
                DS.WriteXmlSchema("crptToken_Report.xml");
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                Rpt_SalaryPayslip_Executive_Bank_Unicode cr =
                    new Rpt_SalaryPayslip_Executive_Bank_Unicode();
                cr.SetDataSource(DS);
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(2, dateTimePicker1.Value.ToString("MMMM") + ", " + dateTimePicker1.Value.ToString("yyyy"));
                cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
                frm.crptViewer.ReportSource = cr;
                frm.crptViewer.Refresh();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Load Grid First");
            }
        }




        private void dataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[colAdvCut.Index].Value = 0;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(txtEmpID.Text)) { MessageBox.Show("Please select records"); return; }
            if (string.IsNullOrEmpty(txtWorkingDay.Text.Trim())) {errorProFrmSalary.SetError(txtWorkingDay, errorProFrmSalary.Icon.ToString()); return; } else {errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtHolidays.Text.Trim())) {errorProFrmSalary.SetError(txtHolidays, errorProFrmSalary.Icon.ToString()); return; } else {errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtHolidayWork.Text.Trim())) {errorProFrmSalary.SetError(txtHolidayWork, errorProFrmSalary.Icon.ToString()); return; } else {errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtPresent.Text.Trim())) {errorProFrmSalary.SetError(txtPresent, errorProFrmSalary.Icon.ToString()); return; } else {errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtAbsent.Text.Trim())) {errorProFrmSalary.SetError(txtAbsent, errorProFrmSalary.Icon.ToString()); return; } else {errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtAvdSalCut.Text.Trim())) {errorProFrmSalary.SetError(txtAvdSalCut, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtMobileBill.Text.Trim())) { errorProFrmSalary.SetError(txtMobileBill, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtOthersSalCut.Text.Trim())) { errorProFrmSalary.SetError(txtOthersSalCut, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtTax.Text.Trim())) { errorProFrmSalary.SetError(txtTax, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtRevTicket.Text.Trim())) { errorProFrmSalary.SetError(txtRevTicket, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}
            if (string.IsNullOrEmpty(txtOthersAlnc.Text.Trim())) { errorProFrmSalary.SetError(txtOthersAlnc, errorProFrmSalary.Icon.ToString()); return; } else { errorProFrmSalary.Clear();}

            
            bool isExecute= false;
            int absentCut = (Convert.ToInt32(txtBasickSal.Text.Trim()) / 30) * Convert.ToInt32(txtAbsent.Text.Trim());
            int totalCut  =  (absentCut + Convert.ToInt32(txtAvdSalCut.Text.Trim())+ Convert.ToInt32(txtMobileBill.Text.Trim()) + Convert.ToInt32(txtOthersSalCut.Text.Trim()) + Convert.ToInt32(txtTax.Text.Trim()) + Convert.ToInt32(txtRevTicket.Text.Trim()));
            int totalGivnSal = 0;
            double othersAllowance = 0;

            if(Convert.ToInt32(txtTotalSal.Text.Trim()) > totalCut){totalGivnSal = (Convert.ToInt32(txtTotalSal.Text.Trim()) - totalCut);}            
            else
            {
                MessageBox.Show(" Total cut must be less than Salary ");
                return;
            }

            int totalGvnSalAndAllownc = (totalGivnSal+Convert.ToInt32(txtOthersAlnc.Text.Trim()));
            double percentageOfHlidayWork = ((Math.Round(Convert.ToDouble(txtTotalSal.Text.Trim())) / 30) * 120) / 100;
            if (Convert.ToInt32(txtHolidayWork.Text.Trim())>0)
            {
                othersAllowance = percentageOfHlidayWork * Convert.ToInt32(txtHolidayWork.Text.Trim());
                if (othersAllowance > 1000)
                {
                    othersAllowance = 1000;
                }
            }

            try
            {
                string cmdText =
                " UPDATE " +
                        " TBL_PROCESSED_SALARY " +
                " SET " +
                        " EMP_WORKING_DAYS  = " + txtWorkingDay.Text.Trim() + ", " +
                        " EMP_HOLIDAYS      = " + txtHolidays.Text.Trim() + ",   " +
                        " EMP_PRESENT  = IFNULL(" + txtPresent.Text.Trim() + ", 0), " +
                        " EMP_CASUAL_LEAVE  = " + txtCasualLeave.Text.Trim() + ", " +
                        " EMP_SICK_LEAVE    = " + txtSickLeave.Text.Trim() + ",   " +
                        " EMP_ANNUAL_LEAVE  = " + txtAnnualLeave.Text.Trim() + ", " +
                        " EMP_ABSENT   = " + txtAbsent.Text.Trim() + ",   " +
                        " EMP_BASIC_SAL  = IFNULL(" + txtBasickSal.Text.Trim() + ", 0), " +
                        " EMP_HOUSE_RENT    = " + txtHouseRent.Text.Trim() + ", " +
                        " EMP_TRANSPORT_ALLOW   = IFNULL(" + txtTransportAlwnc.Text.Trim() + ", 0),  " +
                        " EMP_MEDICAL_ALLOW = IFNULL(" + txtMedicalAllow.Text.Trim() + ", 0),  " +
                        " EMP_TOTAL_SAL     = " + txtTotalSal.Text.Trim() + ",    " +
                        " EMP_ABSENT_SAL_CUT= IFNULL(" + absentCut + ",   0),  " +
                        " EMP_ADV_CUT = IFNULL(" + txtAvdSalCut.Text.Trim() + ", 0),  " +
                        " EMP_MOBILE_BILL   = IFNULL(" + txtMobileBill.Text.Trim() + ", 0),  " +
                        " EMP_OTHERS_SAL_CUT= " + txtOthersSalCut.Text.Trim() + ", " +
                        " EMP_TAX        = " + txtTax.Text.Trim() + ", " +
                        " EMP_REVENUE_TICKET= " + txtRevTicket.Text.Trim() + ", "+
                        " EMP_TOTAL_CUT     = " + totalCut + ",   "+
                        " EMP_TOTAL_GIVEN_SALARY = " + totalGivnSal + ", "+
                        " EMP_HOLIDAY_WORK  = " + txtHolidayWork.Text.Trim()+", "+
                        " EMP_OTHERS_ALLOW  = " + othersAllowance + ", "+ 
                        " EMP_TOTAL_GIVEN_SALARY_AND_ALLOW = " + totalGvnSalAndAllownc + " "+
                " WHERE " +
                        " EMP_ID = '"+ txtEmpID.Text + "' AND "+
                        " YEAR_MONTH = '"+dateTimePicker1.Value.ToString("yyyy/MM")+"' ";


                string updateManual =
                    "UPDATE " +
                            " TBL_PROCESSED_SALARY " +
                    " SET " +
                            " EMP_WORKING_DAYS  = " + txtWorkingDay.Text.Trim() + ",     " +
                            " EMP_HOLIDAYS      = " + txtHolidays.Text.Trim() + ",   " +
                            " EMP_PRESENT  = IFNULL(" + txtPresent.Text.Trim() + ", 0), " +
                            " EMP_CASUAL_LEAVE  = " + txtCasualLeave.Text.Trim() + ", " +
                            " EMP_SICK_LEAVE    = " + txtSickLeave.Text.Trim() + ",   " +
                            " EMP_ANNUAL_LEAVE  = " + txtAnnualLeave.Text.Trim() + ", " +
                            " EMP_ABSENT   = " + txtAbsent.Text.Trim() + ",   " +
                            " EMP_BASIC_SAL  = IFNULL(" + txtBasickSal.Text.Trim() + ", 0), " +
                            " EMP_HOUSE_RENT    = " + txtHouseRent.Text.Trim() + ", " +
                            " EMP_TRANSPORT_ALLOW   = IFNULL(" + txtTransportAlwnc.Text.Trim() + ", 0),  " +
                            " EMP_MEDICAL_ALLOW = IFNULL(" + txtMedicalAllow.Text.Trim() + ", 0),  " +
                            " EMP_TOTAL_SAL     = " + txtTotalSal.Text.Trim() + ",    " +
                            " EMP_ABSENT_SAL_CUT= IFNULL(" + txtAbsentSalCut.Text.Trim() + ",   0),  " +
                            " EMP_ADV_CUT = IFNULL(" + txtAvdSalCut.Text.Trim() + ", 0),  " +
                            " EMP_MOBILE_BILL   = IFNULL(" + txtMobileBill.Text.Trim() + ", 0),  " +
                            " EMP_OTHERS_SAL_CUT= " + txtOthersSalCut.Text.Trim() + ", " +
                            " EMP_TAX        = " + txtTax.Text.Trim() + ", " +
                            " EMP_REVENUE_TICKET= " + txtRevTicket.Text.Trim() + ", " +
                            " EMP_TOTAL_CUT     = " + txtTotalCut.Text.Trim() + ",   " +
                            " EMP_TOTAL_GIVEN_SALARY = " + txtTotalGivenSal.Text.Trim() + ", " +
                            " EMP_HOLIDAY_WORK  = " + txtHolidayWork.Text.Trim() + ", " +
                            " EMP_OTHERS_ALLOW  = " + txtOthersAlnc.Text.Trim() + ", " +
                            " EMP_TOTAL_GIVEN_SALARY_AND_ALLOW = " + txtGivenSalAndAllow.Text.Trim() + " " +
                    " WHERE " +
                            " EMP_ID = '" + txtEmpID.Text + "' AND " +
                            " YEAR_MONTH = '" + dateTimePicker1.Value.ToString("yyyy/MM") + "' ";

                if (cbxManual.Checked)
                {
                    isExecute = DBConn.ExecutionQuery(updateManual);
                }
                else
                {
                    isExecute = DBConn.ExecutionQuery(cmdText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception to Update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (isExecute)
            {
                LoadGridData();
                ClearData();
            }
        }



        private void btnLoadGrid_Click(object sender, EventArgs e)
        {
            LoadGridData();
            // LoadTable();
        }




        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0 || dataGridView.SelectedRows[0].Index == dataGridView.Rows.Count) return;
            getSelectID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtEmpID.Text      = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtWorkingDay.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtHolidays.Text   = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtHolidayWork.Text= dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();            
            txtPresent.Text    = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtCasualLeave.Text= dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtSickLeave.Text  = dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtAnnualLeave.Text= dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtAbsent.Text     = dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtBasickSal.Text  = dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtHouseRent.Text  = dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtTransportAlwnc.Text = dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtMedicalAllow.Text = dataGridView.Rows[e.RowIndex].Cells[16].Value.ToString();
            txtTotalSal.Text   = dataGridView.Rows[e.RowIndex].Cells[17].Value.ToString();
            txtAbsentSalCut.Text = dataGridView.Rows[e.RowIndex].Cells[18].Value.ToString();
            txtAvdSalCut.Text  = dataGridView.Rows[e.RowIndex].Cells[19].Value.ToString();
            txtMobileBill.Text = dataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
            txtOthersSalCut.Text = dataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();
            txtTax.Text        = dataGridView.Rows[e.RowIndex].Cells[22].Value.ToString();
            txtRevTicket.Text  = dataGridView.Rows[e.RowIndex].Cells[23].Value.ToString();
            txtTotalCut.Text   = dataGridView.Rows[e.RowIndex].Cells[24].Value.ToString();
            txtTotalGivenSal.Text = dataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
            txtOthersAlnc.Text = dataGridView.Rows[e.RowIndex].Cells[26].Value.ToString();
            txtGivenSalAndAllow.Text = dataGridView.Rows[e.RowIndex].Cells[27].Value.ToString(); 
        }




        private int calculateHolidays(string empID, string yearMonth)
        {
            int year  = Convert.ToInt32(yearMonth.Substring(0, 4));
            int month = Convert.ToInt32(yearMonth.Substring(4));
            int dayOfmonth = DateTime.DaysInMonth(year, month);
            int presents = absents = holidays = 0;
            int weekend = 0;
            int getclsl = 0;
            try
            {
                for(var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
                {
                    bool getHolidayDays = DBConn.checkDataIfItUsedOtherTableStr("TBL_HOLIDAY", "DDATE", date.ToShortDateString());
                    bool getWeekend = DBConn.getWeekends(date.ToShortDateString(), empID);
                    bool getLvClSl = DBConn.isLeaveClsl(date.ToShortDateString(), empID);
                    if (getHolidayDays) {holidays++;}
                    if (getWeekend) {weekend++;}
                    if (getLvClSl) {getclsl++;}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
            }
            return (holidays + weekend + getclsl);
        }




        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }


        string pat = Application.StartupPath;





        private void cbxManual_CheckedChanged(object sender, EventArgs e)
        {
            bool b = cbxManual.Checked;
            if(b)
            {
                txtCasualLeave.ReadOnly = txtSickLeave.ReadOnly = txtAnnualLeave.ReadOnly =  txtAbsentSalCut.ReadOnly = txtAvdSalCut.ReadOnly = txtTotalCut.ReadOnly = txtTotalGivenSal.ReadOnly =
                txtGivenSalAndAllow.ReadOnly = txtOthersAlnc.ReadOnly
                = false;
            }else{
                txtCasualLeave.ReadOnly = txtSickLeave.ReadOnly = txtAnnualLeave.ReadOnly = txtAbsentSalCut.ReadOnly = txtAvdSalCut.ReadOnly = txtTotalCut.ReadOnly = txtTotalGivenSal.ReadOnly =
                txtGivenSalAndAllow.ReadOnly = txtOthersAlnc.ReadOnly
                = true;
            }
        }


        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Selected = true;
            }
            catch { }
        }



        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Selected = true;                
            }
            catch { }
        }

        private void txtAvdSalCut_TextChanged(object sender, EventArgs e)
        {

        }




        private void btnTopSheet_Click(object sender, EventArgs e)
        {
            DataSet ds1 = DBConn.getSalaryTopSheet(dateTimePicker1.Value.ToString("yyyy/MM"));

            if (ds1 == null) { return; }
            DS = new DataSet();
            DT = new DataTable();

            DT.Columns.Add("PARTICULARS", typeof(string));
            DT.Columns.Add("PERSONS", typeof(int));
            DT.Columns.Add("GROSS", typeof(int));
            DT.Columns.Add("PAID", typeof(int));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    DT.Rows.Add(
                        dr1["PARTICULARS"],
                        dr1["PERSONS"],
                        dr1["GROSS"],
                        dr1["PAID"]
                    );
                }
            }
            DS.Tables.Add(DT);
            frmCrystalReportViewer frm = new frmCrystalReportViewer();
            DataRow dr = DBConn.getCompanyNameAndAddress();
            Rep_TopSheetSummary cr = new Rep_TopSheetSummary();
            cr.SetDataSource(DT);
            cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
            cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
            cr.SetParameterValue(2, dateTimePicker1.Value.ToString("MMMM") + ", " + dateTimePicker1.Value.ToString("yyyy"));
            cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
            frm.crptViewer.ReportSource = cr;
            frm.crptViewer.Refresh();
            frm.Show();
        }

        private void cbxAttenEntry_Click(object sender, EventArgs e)
        {
            if (cbxAttenEntry.Checked)
            {
                this.txtMnulWorkingDay.Enabled = this.txtMnulHoliday.Enabled = this.txtMnulPresentDay.Enabled = true;
            }
            else
            {
                this.txtMnulWorkingDay.Enabled = this.txtMnulHoliday.Enabled = this.txtMnulPresentDay.Enabled = false;
                this.txtMnulWorkingDay.Text = this.txtMnulHoliday.Text = this.txtMnulPresentDay.Text = "";
            }
        }

        private void ClearData()
        {
            txtMnulPresentDay.Text =
            txtMnulHoliday.Text =
            txtMnulWorkingDay.Text =
            txtEmpID.Text =
            txtWorkingDay.Text = 
            txtHolidayWork.Text=
            txtHolidays.Text = 
            txtPresent.Text = 
            txtCasualLeave.Text = 
            txtSickLeave.Text = 
            txtAnnualLeave.Text = 
            txtAbsent.Text = 
            txtBasickSal.Text = 
            txtHouseRent.Text = 
            txtTransportAlwnc.Text = 
            txtMedicalAllow.Text =
            txtTotalSal.Text = 
            txtAbsentSalCut.Text = 
            txtAvdSalCut.Text  = 
            txtMobileBill.Text = 
            txtOthersSalCut.Text =
            txtTax.Text =
            txtRevTicket.Text =
            txtTotalCut.Text =
            txtTotalGivenSal.Text =
            txtOthersAlnc.Text=
            txtGivenSalAndAllow.Text = "";
        }


        void LoadTable()
        {

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Emp ID", typeof(string));
            table.Columns.Add("Emp Name", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Designation", typeof(string));
            table.Columns.Add("Working Day", typeof(int));
            table.Columns.Add("Holidays", typeof(int));
            table.Columns.Add("Holiday work", typeof(string));
            table.Columns.Add("Present", typeof(int));
            table.Columns.Add("CL", typeof(int));
            table.Columns.Add("SL", typeof(int));
            table.Columns.Add("AL", typeof(int));
            table.Columns.Add("absent", typeof(int));
            table.Columns.Add("Basic", typeof(int));
            table.Columns.Add("House Rent", typeof(int));
            table.Columns.Add("Transport", typeof(int));
            table.Columns.Add("Medical", typeof(int));
            table.Columns.Add("Salary", typeof(int));
            table.Columns.Add("Absent Cut", typeof(int));
            table.Columns.Add("Advance Cut", typeof(int));
            table.Columns.Add("Mobile Bill", typeof(int));
            table.Columns.Add("Others Cut", typeof(int));
            table.Columns.Add("Tax", typeof(int));
            table.Columns.Add("Rev Ticket", typeof(int));
            table.Columns.Add("Total Cut", typeof(int));
            table.Columns.Add("Total Givent Sal", typeof(int));
            table.Columns.Add("Others Allowanc", typeof(int));
            table.Columns.Add("Total Givent Sal and Allow", typeof(int));

            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgv in dataGridView.Rows)
                {
                    table.Rows.Add(
                        dgv.Cells[0].Value,
                        dgv.Cells[1].Value,
                        dgv.Cells[2].Value,
                        dgv.Cells[3].Value,
                        dgv.Cells[4].Value,
                        dgv.Cells[5].Value,
                        dgv.Cells[6].Value,
                        dgv.Cells[7].Value,
                        dgv.Cells[8].Value,
                        dgv.Cells[9].Value,
                        dgv.Cells[10].Value,
                        dgv.Cells[11].Value,
                        dgv.Cells[12].Value,
                        dgv.Cells[13].Value,
                        dgv.Cells[14].Value,
                        dgv.Cells[15].Value,
                        dgv.Cells[16].Value,
                        dgv.Cells[17].Value,
                        dgv.Cells[18].Value,
                        dgv.Cells[19].Value,
                        dgv.Cells[20].Value,
                        dgv.Cells[21].Value,
                        dgv.Cells[22].Value,
                        dgv.Cells[23].Value,
                        dgv.Cells[24].Value,
                        dgv.Cells[25].Value,
                        dgv.Cells[26].Value,
                        dgv.Cells[27].Value
                    );
                }                
            }
            else
            {
                MessageBox.Show("Load Grid First");
            }
        }



        private double calculateSalary(double salary, int absent){

            double totalPayableSalary = 0.0d;
            try
            {
                int month = Convert.ToInt32(dateTimePicker1.Value.Month.ToString());
                int year  = Convert.ToInt32(dateTimePicker1.Value.Year.ToString());
                int TDOM  = DateTime.DaysInMonth(year, month);
            }
            catch (Exception e)
            {

            }
            return totalPayableSalary;
        }
    }
}
