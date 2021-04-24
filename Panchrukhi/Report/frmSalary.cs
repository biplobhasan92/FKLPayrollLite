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
        int getHID = 0;
        int presents, absents, holidays;


        private void button1_Click(object sender, EventArgs e)
        {

            int month = Convert.ToInt32(dateTimePicker1.Value.Month.ToString());
            int year  = Convert.ToInt32(dateTimePicker1.Value.Year.ToString());
            int days  = DateTime.DaysInMonth(year, month);
            int basic = Convert.ToInt32(txtBasic.Value.ToString());
            string mmYY = dateTimePicker1.Value.ToString("yyyyMM");       
            LoadData(days, mmYY, basic);
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool b = checkBox1.Checked;
            if (b){
                txtBasic.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
            else{
                txtBasic.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
        }





        private void LoadData(int days, string yearMonth, int basic)
        {
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
                    int abs = calculateAbsent(dr["PERSONID"].ToString(), yearMonth);
                    int workedDays = (days - abs);
                    double salaryA = Convert.ToDouble(dr["NSALARY"].ToString());
                    // category wise advance salary cutting function
                    
                    int getAdvCut = getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 1); // cat 1 for avd salary cut
                    int getMobBill= getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 2); // cat 2 for mobile bill salary cut
                    int getOthers = getCatWiseAdvSalCut(dr["PERSONID"].ToString(), yearMonth, 3); // cat 3 for others salary cut
                    int advDeduction = (getAdvCut+getMobBill+getOthers);
                    double totalPayable = calculateSalary(salaryA, abs) - advDeduction;
                    double salaryCutAmount = salaryA - totalPayable;
                    dataGridView.Rows.Add();

                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSL.Index].Value    = dataGridView.Rows.Count;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["PERSONID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["VNAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCat.Index].Value   = dr["CATEGORY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDesig.Index].Value = dr["Designation"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSalary.Index].Value= salaryA;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTD.Index].Value    = workedDays;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsent.Index].Value= abs;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAdvCut.Index].Value= getAdvCut;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMobileBill.Index].Value = getMobBill;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colOthers.Index].Value= getOthers;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCutSalary.Index].Value = System.Math.Round(salaryCutAmount); 
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTotalPayable.Index].Value = System.Math.Round(totalPayable);
                    dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[10].DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns[11].DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                }
            }
        }




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
            DT.Columns.Add("Salary", typeof(string));
            DT.Columns.Add("absent", typeof(string));
            DT.Columns.Add("Working Day", typeof(string));
            DT.Columns.Add("Advance Cut", typeof(string));
            DT.Columns.Add("Mobile Bill", typeof(string));
            DT.Columns.Add("Others", typeof(string));
            DT.Columns.Add("Salary Cut", typeof(string));
            DT.Columns.Add("Total Payable", typeof(string));
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
                        dgv.Cells[12].Value
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
                cr.SetParameterValue(2, DateTime.Now.Month.ToString() +", "+ DateTime.Now.Year.ToString());
                frm.crptViewer.ReportSource = cr;
                frm.crptViewer.Refresh();
                frm.Show();
            }
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
            DT.Columns.Add("Salary", typeof(string));
            DT.Columns.Add("absent", typeof(string));
            DT.Columns.Add("Working Day", typeof(string));
            DT.Columns.Add("Advance Cut", typeof(string));
            DT.Columns.Add("Mobile Bill", typeof(string));
            DT.Columns.Add("Others", typeof(string));
            DT.Columns.Add("Salary Cut", typeof(string));
            DT.Columns.Add("Total Payable", typeof(string));
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
                        dgv.Cells[12].Value
                    );
                }
                DS.Tables.Add(DT);
                DS.WriteXmlSchema("crptToken_Report.xml");
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                crptToken_Report cr = new crptToken_Report();
                cr.SetDataSource(DS);
                ////cr.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                //cr.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
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
