//using ERP.ModuleManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Panchrukhi.DAO;
using System.Data.SQLite;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panchrukhi
{
    public partial class frmAttendanceProcess : Form
    {

        // SQLite Database 
        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS;
        private DataTable DT = new DataTable();
        private SQLiteDataAdapter DB;
                
        private SQLiteCommand sql_cmd;

        //ITaskManager objTask = ModuleManagers.GetTaskManager();
        string strProcessIndex = "";
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        private int iMachineNumber = 0;//the serial number of the device.After connecting the device ,this value will be changed.
        string _strIP = "";
        string strPort = "";
        int Port = 4370;
        string io = "0";
        DataGridViewRow dgvrMain = null;
        SortedList slData = new SortedList();
        string SqLQry = "";
        bool blSaveStat = false;
        string strProcessStatus = "-1";
        public frmAttendanceProcess()
        {
            InitializeComponent();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (strProcessIndex)
            {
                case "1":
                    //LoadConnection();
                    LoadRecord(1, _strIP, Port);
                    break;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                ResetAfterComplete();
            }
            else if (e.Cancelled)
            {
                MessageBox.Show(e.Error.Message);
                ResetAfterComplete();
            }
            else
            {
                switch (strProcessIndex)
                {
                    case "1":
                        FillConnection();
                        break;
                }
            }
        }
        private void LoadConnection()
        {
            try
            {
                //dsCompany = objCommon.LoadCompanyInformation("");
                //dsEmpType = objCommon.LoadEmpTypeInformation("");
                //dsDepartment = objCommon.LoadDepartmentInformation("");
                //dsDesignation = objCommon.LoadDesignationInformation();
                //dsUnit = objCommon.LoadUnitInformation("");
                //dsLine = objCommon.LoadLineInformation();
                //dsGrade = objCommon.LoadGradeInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FillConnection()
        {
            ResetAfterComplete();
        }


        private void frmAttendanceProcess_Load(object sender, EventArgs e)
        {

            this.Owner.Enabled = false;
            LoadMachineInfo();
        }

        private void LoadMachineInfo()
        {
            dgvfrmMachines.Rows.Clear();

            try
            {
                var Query = "";
                Query = "select NID, VMACHINEIP, VLOCATION, VMACHINEPORT, NIN_OUT_STATUS, CASE NIN_OUT_STATUS WHEN 0 then 'Both' WHEN 1 then 'In' WHEN 5 then 'Out' ELSE 'NA' END as INOUT from TBL_ATTEN_DEVICE where  MACHINE_STATUS = 1 order by NID";
                DataSet dsLoad = new DataSet();
                DBConn.ExecutionQuery(Query);
                DB = new SQLiteDataAdapter(Query, DBConn.sql_conn);
                dsLoad.Reset();
                DB.Fill(dsLoad);
                if (dsLoad.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsLoad.Tables[0].Rows)
                    {
                        dgvfrmMachines.Rows.Add();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colSL.Index].Value        = dgvfrmMachines.Rows.Count;
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colCHK.Index].Value       = false;
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colIP.Index].Value        = dr["VMACHINEIP"].ToString();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colLocation.Index].Value  = dr["VLOCATION"].ToString();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colPort.Index].Value      = dr["VMACHINEPORT"].ToString();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colInOut.Index].Value     = dr["INOUT"].ToString();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colCode.Index].Value      = dr["NID"].ToString();
                        dgvfrmMachines.Rows[dgvfrmMachines.Rows.Count - 1].Cells[colInOutCode.Index].Value = dr["NIN_OUT_STATUS"].ToString();
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + " database connectivity problem ");
            }
        }


        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadMachineInfo();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to connect and load record, It takes more times", "Data Road", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            lblstartTime.Text = DateTime.Now.ToString();
            dgvLog.Rows.Clear();
            strProcessStatus = "4";
            foreach (DataGridViewRow dgvr in dgvfrmMachines.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[colCHK.Index].Value))
                {
                    _strIP  = dgvr.Cells[colIP.Index].Value.ToString();
                    strPort = dgvr.Cells[colPort.Index].Value.ToString();
                    io      = dgvr.Cells[colInOutCode.Index].Value.ToString();
                    iMachineNumber = 1;
                    dgvrMain = dgvr;
                    LoadRecord(1, _strIP, Port);
                    //BeginProcess("1", "Connecting and Data Loading for machine..." + _strIP);
                }
            }
            lblEndTime.Text    = DateTime.Now.ToString();
            lblTotalData.Text  = "Total Record : " + dgvLog.Rows.Count;
        }


        private void LoadRecord(int _MachineNumber, string _MachineID, int Port)
        {
            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;
            int idwErrorCode = 0;
            int iGLCount = 0;
            iMachineNumber = _MachineNumber;
            // l();
            bool bIsConnected = false;

            bIsConnected = axCZKEM1.Connect_Net(_MachineID, Port);
            if (bIsConnected == false)
            {
                dgvrMain.Cells[colStatus.Index].Value = "Diconnected";
                return;
            }
            else
            {
                dgvrMain.Cells[colStatus.Index].Value = "Connected";
                dgvrMain.Cells[colStatusCode.Index].Value = "1";
            }
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber,
                                                out sdwEnrollNumber,
                                                out idwVerifyMode,
                                                out idwInOutMode,
                                                out idwYear,
                                                out idwMonth,
                                                out idwDay,
                                                out idwHour,
                                                out idwMinute,
                                                out idwSecond,
                                                ref idwWorkcode))//get records from the memory
                {
                    
                    string UserID     = sdwEnrollNumber;
                    string VerifyMode = idwVerifyMode.ToString();
                    string InOutMode  = idwInOutMode.ToString();
                    string AttDate    = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                    string WorkCode   = idwWorkcode.ToString();
                    string Reserved   = "";

                    string strY = "";
                    string strM = "";
                    string strD = "";

                    strY = dtpDate.Value.Year.ToString();
                    strM = dtpDate.Value.Month.ToString();
                    strD = dtpDate.Value.Day.ToString();

                    string AttendayTypeCode = "";
                    string AttendancDisplay = "";
                    string strAttendanceDate = "";
                    string strAttendanceTime = "";


                    //Get Max In Time and Min Out Time By Employee Id
                    
                    #region Main
                    //===================================================== Develop==========================================
                    if (idwYear.ToString() == strY && idwMonth.ToString() == strM && idwDay.ToString() == strD)
                    {
                        string minOutTime = "";
                        string query = @"SELECT slt.VMINOUTTIME FROM TBLATTENSLOT slt JOIN TBLPERSON p ON slt.NSLOTID=p.NSLOTID WHERE p.PERSONID='" + UserID + "'";

                        DataSet dsLoad = new DataSet();
                        DBConn.ExecutionQuery(query);
                        DB = new SQLiteDataAdapter(query, DBConn.sql_conn);
                        dsLoad.Reset();
                        DB.Fill(dsLoad);
                        if (dsLoad.Tables[0].Rows.Count > 0)
                        {
                            minOutTime = dsLoad.Tables[0].Rows[0]["VMINOUTTIME"].ToString();
                        }

                        strAttendanceDate = idwDay.ToString("00") + "/" + idwMonth.ToString("00") + "/" + idwYear.ToString("0000");
                        strAttendanceTime = idwHour.ToString("00") + ":" + idwMinute.ToString("00") + ":" + idwSecond.ToString("00");

                        if (minOutTime != "")
                        {
                            if (io == "0")
                            {
                                DateTime attendanceTime = DateTime.Parse(strAttendanceTime);
                                DateTime minimumOutTime = DateTime.Parse(minOutTime);

                                int val = DateTime.Compare(minimumOutTime, attendanceTime);
                                if (val >= 0)
                                {
                                    AttendayTypeCode = "1";
                                    AttendancDisplay = "IN";
                                }
                                else
                                {
                                    AttendayTypeCode = "5";
                                    AttendancDisplay = "Exit";
                                }
                            }
                            else
                            {
                                AttendayTypeCode = io;
                                if (io == "1")
                                    AttendancDisplay = "IN";
                                else if (io == "5")
                                    AttendancDisplay = "Exit";
                            }

                            dgvLog.Rows.Add();
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colSLLog.Index].Value = dgvLog.Rows.Count;
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colID.Index].Value = Convert.ToDouble(UserID).ToString("000000");
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colDate.Index].Value = idwDay.ToString("00") + "/" + idwMonth.ToString("00") + "/" + idwYear.ToString("0000");
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colTime.Index].Value = idwHour.ToString("00") + ":" + idwMinute.ToString("00") + ":" + idwSecond.ToString("00");
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colIOType.Index].Value = AttendancDisplay;
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colIO.Index].Value = AttendayTypeCode;
                            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[colMachineIP.Index].Value = _MachineID;
                            //dgvLog.Rows.Add(dgvLog.Rows.Count + 1, UserID, VerifyMode, InOutMode, AttDate, WorkCode, idwYear.ToString(), idwMonth.ToString(), idwDay.ToString(), idwHour.ToString(), idwMinute.ToString(), idwSecond.ToString(), _MachineID);

                        }

                    }

                    //=======================================================================================================
                    #endregion

                }
            }
            lblLoad.Text = "";
           
        }

        private void BeginProcess(string PIndex, string Titletex)
        {
            if (backgroundWorker.IsBusy)
            {
                MessageBox.Show("System Currently Busy Please Wait");
                return;
            }
            btnConnect.Enabled = false;
            strProcessIndex = PIndex;
            lblLoad.Text = Titletex;
            backgroundWorker.RunWorkerAsync();
        }

        private void ResetAfterComplete()
        {

            btnConnect.Enabled = true;
            strProcessIndex = "";
            lblLoad.Text = "";
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to process now?", "Attendance Prcoess", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(chkDateCorrection.Checked)
                    if (MessageBox.Show("Are you sure to change date " + dtpDate.Text + " and replaced by " +dtpAlterDate.Text + "?", "Alternative date selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    AttnDataArrangeforSave();
            }
        }

        private void AttnDataArrangeforSave()
        {
            try
            {
                //Here for ping color(ID less) only card number status=1, status=0 with ID, status=3 Finger print Virdi, status=4 Finger print ZK
                SortedList slDate = new SortedList();
                slData.Clear();
                /*
                SqLQry = "MERGE INTO HRM_AttendanceProcessMaster A " +
                         "USING (select to_date('" + (chkDateCorrection.Checked? dtpAlterDate.Text : dtpDate.Text) + "','dd/mm/yyyy') cdate,'1' comp from MergeDataTemp) B " +
                         "ON (A.nCompCode=B.comp and to_date(A.dProcessDate,'dd/mm/yyyy') = to_date(B.cdate,'dd/mm/yyyy'))" +
                         "WHEN MATCHED THEN " +
                         "UPDATE SET " +
                              "A.nProcessStatus=0 " +
                              "where A.dProcessDate=to_date('" + (chkDateCorrection.Checked ? dtpAlterDate.Text : dtpDate.Text) + "','dd/mm/yyyy') and A.nCompCode=1 " +
                         "WHEN NOT MATCHED THEN " +
                         "INSERT (dProcessDate,nProcessStatus,nCompCode) " +
                         "VALUES (" +
                         "to_date('" + (chkDateCorrection.Checked ? dtpAlterDate.Text : dtpDate.Text) + "','dd/mm/yyyy')," +
                         "0," +
                         "1" +
                         ")";  */


                //colSLLog, colID, colDate, colTime, colIOType,colIO,colMachineIP

                SqLQry = "delete from TBLATTENDANCE";
                slData.Add(slData.Count + 1, SqLQry);
                foreach (DataGridViewRow dr in dgvLog.Rows)
                {
                    /*
                    SqLQry = "delete from TBLATTENDANCE where VEMPID = '" + dr.Cells[colID.Index].Value.ToString() + "' " +
                             " and DATTENDATE = '" + (chkDateCorrection.Checked ? dtpAlterDate.Text : dr.Cells[colDate.Index].Value.ToString()) + "' "+
                             " and VINOUTTIME = '" + dr.Cells[colTime.Index].Value.ToString() + "' "+
                             " and NAMFLAG    = "  + 0 + "";
                     */


                    SqLQry = "INSERT INTO TBLATTENDANCE(NATTENDSL, VEMPID, DATTENDATE, VINOUTTIME, NATTENTYPE, NAMFLAG) VALUES(" +
                             " (select CASE WHEN max(NATTENDSL) >= 0 THEN max(NATTENDSL) +1 ELSE 1 END FROM TBLATTENDANCE), " +
                             " '" + dr.Cells[colID.Index].Value.ToString() + "', " +
                             " '" + (chkDateCorrection.Checked ? dtpAlterDate.Text : dr.Cells[colDate.Index].Value.ToString()) + "', " +
                             " '" + dr.Cells[colTime.Index].Value.ToString() + "', " +
                             "  " + dr.Cells[colIO.Index].Value.ToString() + ", ";
                    SqLQry = SqLQry + "" + 0 + ")";
                    slData.Add(slData.Count + 1, SqLQry); 
                }

                if (slData.Count == 0)
                    throw new ApplicationException("No Record Available to Process");
                blSaveStat = DBConn.ExecutionQuery(slData);
                if (blSaveStat)
                {
                    ProcessAttendancData();
                    MessageBox.Show("Data has been process successfully", "Data process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvLog.Rows.Clear();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void ProcessAttendancData()
        {
            try
            {
                SortedList slDate = new SortedList();
                slData.Clear();

                var Query = "";
                Query = "SELECT  VEMPID, DATTENDATE, MIN(VINOUTTIME) VINOUTTIME, REMARKS, NATTENTYPE, NAMFLAG FROM TBLATTENDANCE WHERE NATTENTYPE = 1 GROUP BY VEMPID "+
                        "UNION "+
                        "SELECT VEMPID, DATTENDATE, MAX(VINOUTTIME) VINOUTTIME, REMARKS, NATTENTYPE, NAMFLAG FROM TBLATTENDANCE WHERE NATTENTYPE = 5 GROUP BY VEMPID";
                DataSet dsLoad = new DataSet();
                DBConn.ExecutionQuery(Query);
                DB = new SQLiteDataAdapter(Query, DBConn.sql_conn);
                dsLoad.Reset();
                DB.Fill(dsLoad);
                DT = dsLoad.Tables[0];

                SqLQry = "delete from TBLATTENDANCE_PROCESS_DATA where DATTENDATE = '" + dsLoad.Tables[0].Rows[0]["DATTENDATE"].ToString() + "';";

                slData.Add(slData.Count + 1, SqLQry);
                if (dsLoad.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsLoad.Tables[0].Rows) // ltrim('00125410025', '0')
                    {                        
                        SqLQry = "INSERT INTO TBLATTENDANCE_PROCESS_DATA(VEMPID, DATTENDATE, VINOUTTIME, NATTENTYPE, NAMFLAG) VALUES(" +
                                 " ltrim('" + dr["VEMPID"].ToString()+"','0'), " +
                                 " '" + dr["DATTENDATE"].ToString() + "', " +
                                 " '" + dr["VINOUTTIME"].ToString() + "', " +
                                 "  " + dr["NATTENTYPE"].ToString() + ", ";
                        SqLQry = SqLQry + "" + 0 + ")";
                        slData.Add(slData.Count + 1, SqLQry);
                    }
                    DBConn.ExecutionQuery(slData);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + " database connectivity problem");
            }
        }
        

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvLog.Rows.Clear();
            lblTotalData.Text = "...";
            lblstartTime.Text = "...";
            lblEndTime.Text = "...";
        }


        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                foreach (DataGridViewRow dgvr in dgvfrmMachines.Rows)
                {
                    dgvr.Cells[colCHK.Index].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvfrmMachines.Rows)
                {
                    dgvr.Cells[colCHK.Index].Value = false;
                }
            }
        }

        
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void btnProcess1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you want to process now?", "Attn. Prcoess", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
              //  AttnDataArrangeforSave1();
            }
        }


        private void AttnDataArrangeforSave1()
        {
            try
            {
                //Hare for ping color(ID less) only card number status=1,status=0 with ID, status=3 Finger print Virdi, status=4 Finger print ZK

                string _date = Convert.ToDateTime(dtpDate.Text).AddDays(-1).ToString("dd/MM/yyyy");
                SortedList slDate = new SortedList();
                slData.Clear();
                SqLQry = "MERGE INTO HRM_AttendanceProcessMaster A " +
                         "USING (select to_date('" + _date + "','dd/mm/yyyy') cdate,'1' comp from MergeDataTemp) B " +
                         "ON (A.nCompCode=B.comp and to_date(A.dProcessDate,'dd/mm/yyyy')=to_date(B.cdate,'dd/mm/yyyy'))" +
                         "WHEN MATCHED THEN " +
                         "UPDATE SET " +
                         "A.nProcessStatus=0 " +
                         "where A.dProcessDate=to_date('" + _date + "','dd/mm/yyyy') and A.nCompCode=1 " +
                         "WHEN NOT MATCHED THEN " +
                         "INSERT (dProcessDate,nProcessStatus,nCompCode) " +
                         "VALUES (" +
                         "to_date('" + _date + "','dd/mm/yyyy')," +
                         "0," +
                         "1" +
                         ")";

                slData.Add(slDate.Count + 1, SqLQry);       

                if (slData.Count == 0)
                    throw new ApplicationException("No Record Available to Process");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnProcess2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to process now?", "Attn. Prcoess", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               // AttnDataArrangeforSave2();
            }
        }


        private void AttnDataArrangeforSave2()
        {
            try
            {
                //Hare for ping color(ID less) only card number status=1,status=0 with ID, status=3 Finger print Virdi, status=4 Finger print ZK
                string _date = Convert.ToDateTime(dtpDate.Text).AddDays(-2).ToString("dd/MM/yyyy");
                SortedList slDate = new SortedList();
                slData.Clear();
                SqLQry = "MERGE INTO HRM_AttendanceProcessMaster A " +
                                           "USING (select to_date('" + _date + "','dd/mm/yyyy') cdate,'1' comp from MergeDataTemp) B " +
                                           "ON (A.nCompCode=B.comp and to_date(A.dProcessDate,'dd/mm/yyyy')=to_date(B.cdate,'dd/mm/yyyy'))" +
                                           "WHEN MATCHED THEN " +
                                           "UPDATE SET " +
                                               "A.nProcessStatus=0 " +
                                               "where A.dProcessDate=to_date('" + _date + "','dd/mm/yyyy') and A.nCompCode=1 " +
                                           "WHEN NOT MATCHED THEN " +
                                           "INSERT (dProcessDate,nProcessStatus,nCompCode) " +
                                           "VALUES (" +
                                           "to_date('" + _date + "','dd/mm/yyyy')," +
                                           "0," +
                                           "1" +
                                           ")";

                slData.Add(slDate.Count + 1, SqLQry);
                
                if (slData.Count == 0)
                    throw new ApplicationException("No Record Available to Process");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void chkDateCorrection_CheckedChanged(object sender, EventArgs e)
        {
            dtpAlterDate.Enabled = chkDateCorrection.Checked;
        }


        private void frmAttendanceProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }
    }
}
