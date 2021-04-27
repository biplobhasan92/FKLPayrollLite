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
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getSelectID = 0;
        int presents, absents, holidays;


        private void button1_Click(object sender, EventArgs e)
        {

            int month = Convert.ToInt32(dateTimePicker1.Value.Month.ToString());
            int year  = Convert.ToInt32(dateTimePicker1.Value.Year.ToString());
            int days  = DateTime.DaysInMonth(year, month);
            int basic = Convert.ToInt32(txtBasic.Value.ToString());
            string mmYY = dateTimePicker1.Value.ToString("yyyyMM");
            deleteBefourInsert(dateTimePicker1.Value.ToString("yyyy/MM"));
            InsertSalPorcessData(days, mmYY, basic);
            MessageBox.Show("Process completed. Click Load Data.");
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool b = checkBox1.Checked;
            if (b){
                txtBasic.Enabled = false;
                dateTimePicker1.Enabled = false;
                btnSalProcess.Enabled = false;
            }
            else{
                txtBasic.Enabled = true;
                dateTimePicker1.Enabled = true;
                btnSalProcess.Enabled = true;
            }
        }


        private void deleteBefourInsert(string yearMontrh)
        {
            try
            {
                string cmdText = " delete from TBL_PROCESSED_SALARY where YEAR_MONTH = '"+yearMontrh+"' ";
                DBConn.ExecutionQuery(cmdText);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem in DeleteTableRowInt()");
            }
        }


        private void InsertSalPorcessData(int days, string yearMonth, int basic)
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
                                         (SELECT VSLOTNAME ||  '(' || VINTIME || '-' ||  VOUTTIME || ')'  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = P.NSLOTID)) as                  SHIFT,
                                         P.NSALARY       
                                    FROM 
                                         TBLPERSON P where p.NCATID != '3';";
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
                    // int abs = calculateAbsent(dr["PERSONID"].ToString(), yearMonth);                  
                    int weekend = getCountOfWeekendInMonthUsing(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());                    
                    int leaveCL = getCountOfCLInMnthUsingID(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());
                    int leaveSL = getCountOfSLInMnthUsingID(dateTimePicker1.Value.ToString("yyyy/MM"), dr["PERSONID"].ToString());
                    int workedDays = days - weekend;
                    double salaryA = Convert.ToDouble(dr["NSALARY"].ToString());

                    // category wise advance salary cutting function                    
                    //int getAdvCut  = getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 1); // cat 1 for avd salary cut
                    //int getMobBill = getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 2); // cat 2 for mobile bill salary cut
                    //int getOthers  = getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 3); // cat 3 for others salary cut
                    //int advDeduction = (getAdvCut+getMobBill+getOthers);
                    // double totalPayable = calculateSalary(salaryA, abs) - advDeduction;
                    // double salaryCutAmount = salaryA - totalPayable;
                    /*
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSL.Index].Value    = dataGridView.Rows.Count;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["PERSONID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["VNAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCat.Index].Value   = dr["CATEGORY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDesig.Index].Value = dr["Designation"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSalary.Index].Value= salaryA;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTD.Index].Value    = workedDays;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCasualLeave.Index].Value = leaveCL;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSickLeave.Index].Value = leaveSL;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsent.Index].Value= abs;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAdvCut.Index].Value= getAdvCut;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMobileBill.Index].Value = getMobBill;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colWeekend.Index].Value = weekend;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colOthers.Index].Value= getOthers;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCutSalary.Index].Value = System.Math.Round(salaryCutAmount); 
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalPayable.Index].Value = System.Math.Round(totalPayable);
                    */
                    SaveDataOfProcessedSalary(dr["PERSONID"].ToString(), dr["VNAME"].ToString(), dr["Designation"].ToString(), workedDays, weekend, leaveCL, leaveSL, dateTimePicker1.Value.ToString("yyyy/MM"), Convert.ToInt32(dr["NSALARY"].ToString()));
                }
            }
        }





        public void LoadGridData()
        {
            DT = new DataTable();
            DS = new DataSet();
            string CommandText = @" select d.* from TBL_PROCESSED_SALARY d where d.YEAR_MONTH = '"+ dateTimePicker1.Value.ToString("yyyy/MM") + "'; ";
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
                    
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSL.Index].Value = Convert.ToInt32(dr["SL"].ToString());
                    // dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSLL.Index].Value = dr["SL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value   = dr["EMP_ID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["EMP_NAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDesig.Index].Value   = dr["EMP_DESIG"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colWorkingDays.Index].Value   = dr["EMP_WORKING_DAYS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHolidays.Index].Value = dr["EMP_HOLIDAYS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSalary.Index].Value = dr["EMP_TOTAL_SAL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colPresent.Index].Value = dr["EMP_PRESENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCasualLeave.Index].Value = dr["EMP_CASUAL_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSickLeave.Index].Value = dr["EMP_SICK_LEAVE"].ToString();

                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAnualLeave.Index].Value= dr["EMP_ANNUAL_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsent.Index].Value    = dr["EMP_ABSENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colBasicSal.Index].Value  = dr["EMP_BASIC_SAL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHouseRent.Index].Value = dr["EMP_HOUSE_RENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTransport.Index].Value = dr["EMP_TRANSPORT_ALLOW"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMedical.Index].Value   = dr["EMP_MEDICAL_ALLOW"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsentCut.Index].Value = dr["EMP_ABSENT_SAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAdvCut.Index].Value = dr["EMP_ADV_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMobileBill.Index].Value= dr["EMP_MOBILE_BILL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colOthers.Index].Value = dr["EMP_OTHERS_SAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTax.Index].Value = dr["EMP_TAX"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colRevenueTikit.Index].Value = dr["EMP_REVENUE_TICKET"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalCut.Index].Value = dr["EMP_TOTAL_CUT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalGivenSal.Index].Value = dr["EMP_TOTAL_GIVEN_SALARY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colGivenSalAndallownce.Index].Value = dr["EMP_TOTAL_GIVEN_SALARY_AND_ALLOW"].ToString();
                }
            }
        }



        public void SaveDataOfProcessedSalary(string empID, string empName, string designation, int workingDay, int holidays,  int cl, int sl, string YEAR_MONTH, int sal)
        {
            try
            {
                string cmdText = " insert into TBL_PROCESSED_SALARY(" +
                        "SL," +
                        "EMP_ID," +
                        "EMP_NAME," + 
                        "EMP_DESIG," +
                        "EMP_WORKING_DAYS," +
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
                        "EMP_TOTAL_GIVEN_SALARY_AND_ALLOW, " +
                        "YEAR_MONTH " +
                    ") " +
                     " values (" +
                         "(select CASE WHEN max(SL) >= 0 THEN max(SL) +1 ELSE 1 END FROM TBL_PROCESSED_SALARY), "+
                         "  '" + empID+ "',     " +
                         "  '" + empName + "',   " +
                         "  '" + designation + "',   " +
                         "  " + workingDay + ",   " +
                         "  " + holidays + ",   " +
                         "  " + cl + ", " +
                         "  " + sl + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + sal + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 10 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  " + 0 + ", " +
                         "  '" + YEAR_MONTH + "' " +
                     " ) ";
                DBConn.ExecutionQuery(cmdText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: "+ex.Message);
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



        // DBConn.getWeekends(date.ToString("dd/MM/yyyy"), dr["PERSONID"].ToString());

        private void frmSalary_Load(object sender, EventArgs e)
        {
            // this.Owner.Enabled = false;
            //this.dataGridView.Columns["salary"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }



        private void frmSalary_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Owner.Enabled = true;
        }




        // string sql = "select IFNULL(d.DEDUCTED_SALARY, 0) as mobileBill from TBL_SALARY_DEDUCTION d where d.EMP_ID = '11002' and substr(YEAR_MONTH,4,4)|| substr(YEAR_MONTH, 1, 2) = '202101' and d.CAT_ID = 1 ";
        private int getCatWiseAdvSalCut(string empID, string yearMonth, int catID)
        {
            int deducted_Amount = 0;
            try
            {
                string CommandText =
                            @"select IFNULL(d.DEDUCTED_SALARY, 0) as mobileBill from TBL_SALARY_DEDUCTION d where d.EMP_ID = '"+empID+"' and substr(YEAR_MONTH,4,4)|| substr(YEAR_MONTH, 1, 2) = '"+yearMonth+"' and d.CAT_ID = "+catID+" ";                
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



        private void btn_print_Click(object sender, EventArgs e) // MonthlySummaryReport.xml
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
            DT.Columns.Add("Working Day", typeof(string));
            DT.Columns.Add("Holidays", typeof(string));
            DT.Columns.Add("Present", typeof(string));
            DT.Columns.Add("CL", typeof(string));
            DT.Columns.Add("SL", typeof(string));
            DT.Columns.Add("AL", typeof(string));
            DT.Columns.Add("absent", typeof(string));
            DT.Columns.Add("Basic", typeof(string));
            DT.Columns.Add("House Rent", typeof(string));
            DT.Columns.Add("Transport", typeof(string));
            DT.Columns.Add("Medical", typeof(string));
            DT.Columns.Add("Salary", typeof(string));
            DT.Columns.Add("Absent Cut", typeof(string));
            DT.Columns.Add("Advance Cut", typeof(string));
            DT.Columns.Add("Mobile Bill", typeof(string));
            DT.Columns.Add("Others Cut", typeof(string));
            DT.Columns.Add("Tax", typeof(string));
            DT.Columns.Add("Rev Ticket", typeof(string));
            DT.Columns.Add("Total Cut", typeof(string));
            DT.Columns.Add("Total Givent Sal", typeof(string));
            DT.Columns.Add("Total Givent Sal and Allow", typeof(string));
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
                        dgv.Cells[25].Value
                    );
                }
                DS.Tables.Add(DT);
                DS.WriteXmlSchema("MonthlySummaryReport.xml"); 
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                // MonthlySummaryReport cr = new MonthlySummaryReport();
                Rpt_SalarySheet_FKLSpinning_Bangla cr = new Rpt_SalarySheet_FKLSpinning_Bangla();
                cr.SetDataSource(DS);
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(2, DateTime.Today.ToString("MMM") + ", "+ DateTime.Now.Year.ToString());
                frm.crptViewer.ReportSource = cr;
                frm.crptViewer.Refresh();
                frm.Show();
            }// string month_name = date.ToString("MMM");
            else
            {
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
            DT.Columns.Add("Working Day", typeof(string));
            DT.Columns.Add("Holidays", typeof(string));
            DT.Columns.Add("Present", typeof(string));
            DT.Columns.Add("CL", typeof(string));
            DT.Columns.Add("SL", typeof(string));
            DT.Columns.Add("AL", typeof(string));
            DT.Columns.Add("absent", typeof(string));
            DT.Columns.Add("Basic", typeof(string));
            DT.Columns.Add("House Rent", typeof(int));
            DT.Columns.Add("Transport", typeof(int));
            DT.Columns.Add("Medical", typeof(int));
            DT.Columns.Add("Salary", typeof(int));
            DT.Columns.Add("Absent Cut", typeof(string));
            DT.Columns.Add("Advance Cut", typeof(string));
            DT.Columns.Add("Mobile Bill", typeof(string));
            DT.Columns.Add("Others Cut", typeof(string));
            DT.Columns.Add("Tax", typeof(string));
            DT.Columns.Add("Rev Ticket", typeof(string));
            DT.Columns.Add("Total Cut", typeof(int));
            DT.Columns.Add("Total Givent Sal", typeof(int));
            DT.Columns.Add("Total Givent Sal and Allow", typeof(string));
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
                        dgv.Cells[25].Value
                    );
                }
                DS.Tables.Add(DT);
                DS.WriteXmlSchema("crptToken_Report.xml");
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                crptToken_Report cr = new crptToken_Report();
                cr.SetDataSource(DS);
                cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
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
            // e.Row.Cells[10].Value = 0;
            e.Row.Cells[colAdvCut.Index].Value = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isExecute = false;
            try
            {
                string cmdText =
                "UPDATE " +
                        " TBL_PROCESSED_SALARY " +
                " set " +
                        " EMP_WORKING_DAYS = " + txtWorkingDay.Text.Trim() + ",     " +
                        " EMP_HOLIDAYS    = " + txtHolidays.Text.Trim() + ",   " +
                        " EMP_PRESENT  = IFNULL(" + txtPresent.Text.Trim() + ", 0), " +
                        " EMP_CASUAL_LEAVE = " + txtCasualLeave.Text.Trim() + ", " +
                        " EMP_SICK_LEAVE     = " + txtSickLeave.Text.Trim() + ",     " +
                        " EMP_ANNUAL_LEAVE   = " + txtAnnualLeave.Text.Trim() + ",  " +
                        " EMP_ABSENT   = " + txtAbsent.Text.Trim() + ",   " +
                        " EMP_BASIC_SAL  = IFNULL(" + txtBasickSal.Text.Trim() + ", 0), " +
                        " EMP_HOUSE_RENT     = " + txtHouseRent.Text.Trim() + ",   " +
                        " EMP_TRANSPORT_ALLOW   = IFNULL(" + txtTransportAlwnc.Text.Trim() + ", 0),  " +
                        " EMP_MEDICAL_ALLOW = IFNULL(" + txtMedicalAllow.Text.Trim() + ", 0),  " +
                        " EMP_TOTAL_SAL  = " + txtTotalSal.Text.Trim() + ",    " +
                        " EMP_ABSENT_SAL_CUT  = IFNULL(" + txtAbsentSalCut.Text.Trim() + ",   0),  " +
                        " EMP_ADV_CUT = IFNULL(" + txtAvdSalCut.Text.Trim() + ", 0),  " +
                        " EMP_MOBILE_BILL   = IFNULL(" + txtMobileBill.Text.Trim() + ", 0),  " +
                        " EMP_OTHERS_SAL_CUT        = " + txtOthersSalCut.Text.Trim() + ",   " +
                        " EMP_TAX        = " + txtTax.Text.Trim() + ",   " +
                        " EMP_REVENUE_TICKET        = " + txtRevTicket.Text.Trim() + ",   " +
                        " EMP_TOTAL_CUT        = " + txtTotalCut.Text.Trim() + ",   " +
                        " EMP_TOTAL_GIVEN_SALARY        = " + txtTotalGivenSal.Text.Trim() + ", "+
                        " EMP_TOTAL_GIVEN_SALARY_AND_ALLOW = " + txtGivenSalAndAllow.Text.Trim() + " "+
                " where  " +
                        " EMP_ID = '"+ txtEmpID.Text +"' ";

                isExecute = DBConn.ExecutionQuery(cmdText);
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
        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0 || dataGridView.SelectedRows[0].Index == dataGridView.Rows.Count) return;
            getSelectID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtEmpID.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtWorkingDay.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtHolidays.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPresent.Text  = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtCasualLeave.Text = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtSickLeave.Text = dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtAnnualLeave.Text = dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtAbsent.Text = dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtBasickSal.Text = dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtHouseRent.Text = dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtTransportAlwnc.Text = dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtMedicalAllow.Text = dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtTotalSal.Text = dataGridView.Rows[e.RowIndex].Cells[16].Value.ToString();
            txtAbsentSalCut.Text = dataGridView.Rows[e.RowIndex].Cells[17].Value.ToString();
            txtAvdSalCut.Text    = dataGridView.Rows[e.RowIndex].Cells[18].Value.ToString();
            txtMobileBill.Text   = dataGridView.Rows[e.RowIndex].Cells[19].Value.ToString();
            txtOthersSalCut.Text = dataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
            txtTax.Text = dataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();
            txtRevTicket.Text = dataGridView.Rows[e.RowIndex].Cells[22].Value.ToString();
            txtTotalCut.Text = dataGridView.Rows[e.RowIndex].Cells[23].Value.ToString();
            txtTotalGivenSal.Text = dataGridView.Rows[e.RowIndex].Cells[24].Value.ToString();
            txtGivenSalAndAllow.Text = dataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
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
                for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
                {
                    // Console.WriteLine("Date : "+date.ToShortDateString());
                    bool getHolidayDays = DBConn.checkDataIfItUsedOtherTableStr("TBL_HOLIDAY", "DDATE", date.ToShortDateString());
                    bool getWeekend = DBConn.getWeekends(date.ToShortDateString(), empID);
                    bool getLvClSl = DBConn.isLeaveClsl(date.ToShortDateString(), empID);
                    if (getHolidayDays) { holidays++;}
                    if (getWeekend) { weekend++;}
                    if (getLvClSl) { getclsl++;}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
            }
            return (holidays + weekend + getclsl);
        }




        private void ClearData()
        {
            txtEmpID.Text =
            txtWorkingDay.Text = 
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
            txtAvdSalCut.Text = 
            txtMobileBill.Text = 
            txtOthersSalCut.Text = 
            txtTax.Text = 
            txtRevTicket.Text = 
            txtTotalCut.Text = 
            txtTotalGivenSal.Text =
            txtGivenSalAndAllow.Text = "";
        }

        private double calculateSalary(double salary, int absent){

            double totalPayableSalary = 0.0d;
            try
            {
                int month = Convert.ToInt32(dateTimePicker1.Value.Month.ToString());
                int year  = Convert.ToInt32(dateTimePicker1.Value.Year.ToString());
                int basic = Convert.ToInt32(txtBasic.Value.ToString());
                int TDOM  = DateTime.DaysInMonth(year, month); // total days of month
                //salary = 14500;
                
                double da  = (salary / 100) * basic; // Deductable amount
                double pdb = (da / TDOM); // per day basic = ( Deductable amount / Total day of month)
                // total payable for employee = ( per day basic * absent );
                totalPayableSalary = salary - (pdb * absent); 
            }
            catch (Exception e)
            {

            }
            return totalPayableSalary;
        }
    }
}
