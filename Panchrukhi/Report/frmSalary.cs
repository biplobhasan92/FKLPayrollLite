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

        /*

         string fstDate = dtpFstDate.Value.ToString("yyyyMMdd");
         string lstDate = dtpLstDate.Value.ToString("yyyyMMdd");
         AndConditions += " substr(TA.DATTENDATE,7)||substr(TA.DATTENDATE,4,2)||substr(TA.DATTENDATE,1,2) between '" + fstDate + "' AND '" + lstDate + "' ";
             
         */

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
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSL.Index].Value    = dataGridView.Rows.Count;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["PERSONID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["VNAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCat.Index].Value   = dr["CATEGORY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDesig.Index].Value = dr["Designation"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSalary.Index].Value= dr["NSALARY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTD.Index].Value    = days;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colAbsent.Index].Value= calculateAbsent(dr["PERSONID"].ToString(), yearMonth);
                }
            }
        }


        private int calculateAbsent(string empID, string yearMonth) {

            //empID = "12032";
            //yearMonth = "202011";
            presents= absents= holidays=0;
            DT = new DataTable();
            DS = new DataSet();
            string CommandText = @"Select
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
                                  substr(TA.DATTENDATE,7)||substr(TA.DATTENDATE,4,2) between '"+yearMonth+"' AND '"+yearMonth+"'  AND P.PERSONID IN ('"+ empID + "')  AND P.NSTATUS == 1  ORDER BY TA.DATTENDATE;";
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DBConn.ExecutionQuery(CommandText);
            DS.Reset();
            DB.Fill(DS);
            
            foreach (DataRow dr in DS.Tables[0].Rows)
            {
                
                bool absentCounted = false;
                string getID     = dr["ID"].ToString();
                string strIntime = dr["INTIME"].ToString();
                string strOuttime= dr["OUTTIME"].ToString();
                bool getHolidayDays = DBConn.checkDataIfItUsedOtherTableStr("TBL_HOLIDAY", "DDATE", dr["DATE"].ToString());
                bool getWeekend = DBConn.getWeekends(dr["DATE"].ToString(), getID);
                string getLvClSl = DBConn.getLeaveClsl(dr["DATE"].ToString(), getID);

                if (strIntime.Equals("") || strOuttime.Equals(""))
                {
                    absents++; absentCounted = true;
                }
                else
                {
                    presents++;
                }

                if (getHolidayDays || getWeekend)
                {
                    if (getHolidayDays)
                    {

                    }
                    else if (getWeekend && getLvClSl == "")
                    {
                    }
                    else if (getLvClSl != "")
                    {
                       
                    }
                    if (absentCounted) absents--;
                    holidays++;
                }
            }
            return absents;
        }
    }
}
