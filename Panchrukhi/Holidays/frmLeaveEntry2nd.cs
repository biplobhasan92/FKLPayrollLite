using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panchrukhi.DAO;
using System.Data.SQLite;
using Panchrukhi.Report;

namespace Panchrukhi
{
    public partial class frmLeaveEntry2nd : Form
    {


        public frmLeaveEntry2nd()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.AutoSize    = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            ClearData();
            LoadData();
            dtPkrFormDate.Format = dtPkrToDate.Format = dtpFstDate.Format = DateTimePickerFormat.Custom;
            dtPkrFormDate.CustomFormat = dtPkrToDate.CustomFormat = "dd/MM/yyyy";
            dtpFstDate.CustomFormat = "yyyy";
            dtpFstDate.ShowUpDown = true;
        }


        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS;
        private DataTable DT;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private SQLiteConnection conn;
        int getHID=0;
        string pat = Application.StartupPath;




        // Slot Form Close Button
        private void btnFrmSlotClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void BtnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            string txtEmpId = txtEmpID.Text.Trim();

            if(!DBConn.checkDataIfItUsedOtherTableStr("TBLPERSON", "PERSONID", txtEmpId))
            {
                MessageBox.Show(txtEmpId+" ID is not found ");
                return;
            }

            if (string.IsNullOrEmpty(txtEmpId) || (cbxLeaveCat.SelectedItem == null)){
                MessageBox.Show(" Insert data properly ", " Emp ID Or Leave Cat Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error);  return;
            }

            if(!isLeaveAvailable(txtEmpId, Convert.ToInt32((cbxLeaveCat.SelectedItem as dynamic).Value)))
            {
                MessageBox.Show(" There is no Leave Available for This ID : " + txtEmpId);
                ClearData();
                return;
            }
            
            if (getHID == 0)
            {
                string cmdText = "";
                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    foreach (DataGridViewRow dr in dataGridView.Rows)
                    {
                        if (Convert.ToDateTime(dr.Cells[colHDate.Index].Value.ToString()) == date && dr.Cells[colEmpID.Index].Value.ToString()==txtEmpId) /// Problem Here 
                        {
                            DialogResult dResult = MessageBox.Show(this, "Date conflicts on " + date.ToString() + ". Do you still want to update?", "Date conflict", MessageBoxButtons.YesNo);
                            if (dResult == DialogResult.No) return;  
                        }
                    }
                }

                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    cmdText += " Delete from TBL_LEAVE_ENTRY where LEAVE_DATE between '" + dtPkrFormDate.Text + "' and '"+ dtPkrToDate.Text + "' and emp_id = " +
                        " '"+txtEmpId+ "' ; ";
                }


                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    cmdText += " INSERT INTO TBL_LEAVE_ENTRY(SL_LEAVE, EMP_ID, LEAVE_NAME, LEAVE_DATE) " +
                               " VALUES( "+
                               "   (select CASE WHEN max(SL_LEAVE) >= 0 THEN max(SL_LEAVE)+1 ELSE 1 END FROM TBL_LEAVE_ENTRY)," +
                               " '"+txtEmpID.Text.ToString().Trim()+ "', " +
                               " IFNULL(" + (cbxLeaveCat.SelectedItem as dynamic).Value + ", 1), " +
                               " '"+date.ToShortDateString()+"' "+
                               ");  ";
                }
                if (DBConn.ExecutionQuery(cmdText))
                {
                    ClearData();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Something is wrong ??? !");
                }
            }
            else
            {
                
            }
        }




        
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }





        // Form Clear Method
        void ClearData()
        {
            txtEmpID.Text = cbxLeaveCat.Text = dtPkrFormDate.Text = dtPkrToDate.Text = "";
            getHID = 0;
        }





        // Load Data From SQlite Database
        private void LoadData()
        {
            DT = new DataTable();
            DS = new DataSet();
            string CommandText = @"select
                                     le.SL_LEAVE,
                                     le.EMP_ID as EMP_ID,
                                     le.LEAVE_NAME as LEAVE_CAT_ID,
                                     (select s.LEAVE_NAME from TBL_LEAVE_SETTINGS s where s.SL = le.LEAVE_NAME) as LEAVE_NAME,
                                     le.LEAVE_DATE as LEAVE_DATE,
                                     (select p.VNAME from TBLPERSON p where p.PERSONID=le.EMP_ID) as EMP_NAME
                                   from TBL_LEAVE_ENTRY le;";
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
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNHID.Index].Value = dataGridView.Rows.Count;
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDBSL.Index].Value  = dr["SL_LEAVE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["EMP_ID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHDate.Index].Value = dr["LEAVE_DATE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHCategory.Index].Value  = dr["LEAVE_NAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colLeaveCatID.Index].Value = dr["LEAVE_CAT_ID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpName.Index].Value = dr["EMP_NAME"].ToString();
                }
            }
        }



      

        private void FrmData_Load(object sender, EventArgs e)
        {
            // this.Owner.Enabled = false;
            LoadHolyDayCatCombo();
            LoadData();
            gbxSerachOption.Enabled = false;
        }




        private void DataGridData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getHID            = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
            txtEmpID.Text     = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbxLeaveCat.Text  = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            dtPkrFormDate.Text= dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        }


        
        private void DataGridData_SelectionChanged(object sender, EventArgs e)
        {            
            if (dataGridView.SelectedRows.Count == 0 || dataGridView.SelectedRows[0].Index == dataGridView.Rows.Count) return;
            getHID            = Convert.ToInt32(dataGridView[5, dataGridView.SelectedRows[0].Index].Value.ToString());
            txtEmpID.Text     = dataGridView[1, dataGridView.SelectedRows[0].Index].Value.ToString();
            dtPkrFormDate.Text= dataGridView[2, dataGridView.SelectedRows[0].Index].Value.ToString();
            cbxLeaveCat.Text  = dataGridView[3, dataGridView.SelectedRows[0].Index].Value.ToString();            
        }




        // To Load Class Combo in CLASS Form. calling from Load Form.
        private void LoadHolyDayCatCombo()
        {
            string CommandText = "SELECT * from TBL_LEAVE_SETTINGS";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cbxLeaveCat.DisplayMember= "Text";
            cbxLeaveCat.ValueMember  = "Value";
            cbxSrcLvCat.DisplayMember= "Text";
            cbxSrcLvCat.ValueMember  = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cbxLeaveCat.Items.Add(new { Text = dr["LEAVE_NAME"].ToString(), Value = dr["SL"].ToString() });//NCATID
                cbxSrcLvCat.Items.Add(new { Text = dr["LEAVE_NAME"].ToString(), Value = dr["SL"].ToString() });//NCATID
            }
        }






        private void BtnDelete_Click(object sender, EventArgs e)
        {            
            if (DBConn.DeleteTableRowInt("TBL_LEAVE_ENTRY", "SL_LEAVE", getHID))
            {
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("", "Unable to delete!!!! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Boolean isLeaveAvailable(string empID, int leavCat)
        {
            bool leave = false;
            int totalLeav  = 0;
            int leaveCount = 0; 
            try
            {
                string cmdText = @"select "+
                                      "count(*) as count_lv, "+
                                      "(select total_days from tbl_leave_settings where sl = "+leavCat+") as  total_days"+
                                    " from TBL_LEAVE_ENTRY e where e.EMP_ID = '"+ empID + "' and e.LEAVE_NAME = " + leavCat + "; ";
                DB = new SQLiteDataAdapter(cmdText, DBConn.sql_conn);
                DBConn.ExecutionQuery(cmdText);
                DS.Reset();
                DB.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
                    {
                        leaveCount = Convert.ToInt32(dr["count_lv"].ToString()); // person consumed total leave
                        totalLeav  = Convert.ToInt32(dr["total_days"].ToString());// total leave
                    }
                }

                if (leaveCount < totalLeav)
                {
                    leave = true;
                }
                else
                {
                    leave = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return leave;
        }



        private void frmSetHolyDay_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Owner.Enabled = true;
        }


        private void ckEnableSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEnableSearch.Checked)
            {
                gbxSerachOption.Enabled = true;
            }
            else
            {
                gbxSerachOption.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string txtYear = dtpFstDate.Value.Year.ToString();
            string empID = txtSrcID.Text.ToString();
            if (empID == "" || (cbxSrcLvCat.SelectedItem == null))
            {
                MessageBox.Show(" Insert data properly ", " Emp ID Or Leave Cat Missing ", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            try
            {
                DT = new DataTable();
                DS = new DataSet();
                string CommandText =
                     @"select "+
                        "le.SL_LEAVE, "+
                        "le.EMP_ID as EMP_ID,"+
                        "le.LEAVE_NAME as LEAVE_CAT_ID,"+
                        "(select s.LEAVE_NAME from TBL_LEAVE_SETTINGS s where s.SL = le.LEAVE_NAME) as LEAVE_NAME,"+
                        "le.LEAVE_DATE as LEAVE_DATE"+
                        " from TBL_LEAVE_ENTRY le"+
                        " where "+
                        " le.EMP_ID = '"+empID+"' "+
                      " AND  substr(le.LEAVE_DATE,7,9) = '"+ txtYear + "' AND le.LEAVE_NAME = "+ (cbxSrcLvCat.SelectedItem as dynamic).Value + " ;";
                DBConn.ExecutionQuery(CommandText);
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DBConn.ExecutionQuery(CommandText);
                DS.Reset();
                DB.Fill(DS);
                dataGridView.Rows.Clear();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in DS.Tables[0].Rows)
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNHID.Index].Value  = dataGridView.Rows.Count;
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDBSL.Index].Value  = dr["SL_LEAVE"].ToString();
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["EMP_ID"].ToString();
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHDate.Index].Value = dr["LEAVE_DATE"].ToString();
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colHCategory.Index].Value = dr["LEAVE_NAME"].ToString();
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colLeaveCatID.Index].Value= dr["LEAVE_CAT_ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message : "+ex.Message);
            }
        }


        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            string txt = txtEmpID.Text;
            if (txtEmpID.Text != "")
            {
                int result = Int32.Parse(txt.Substring(0, 1));
                if (result == 0)
                {
                    MessageBox.Show("Invalid Input", "0 is not allowed as first digit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmpID.Clear();
                }
            }
        }



        private void btnLeaveReport_Click(object sender, EventArgs e)
        {
            string txtYear = dtpFstDate.Value.Year.ToString();
            try
            {
                string query =@" SELECT "+
                                    " P.PERSONID AS ID, "+
                                    " P.VNAME AS NAME, "+
                                    " (select count(s.LEAVE_NAME) from TBL_LEAVE_ENTRY s where s.EMP_ID = p.PERSONID and s.LEAVE_NAME = 1 and substr(s.LEAVE_DATE,7,9) = '"+txtYear+"') as CL, "+
                                    " (select count(s.LEAVE_NAME) from TBL_LEAVE_ENTRY s where s.EMP_ID = p.PERSONID and s.LEAVE_NAME = 2 and substr(s.LEAVE_DATE,7,9) = '" + txtYear + "') as SL, " +
                                    " (select count(s.LEAVE_NAME) from TBL_LEAVE_ENTRY s where s.EMP_ID = p.PERSONID and s.LEAVE_NAME = 3 and substr(s.LEAVE_DATE,7,9) = '" + txtYear + "') as AL, " +
                                    " (select count(s.LEAVE_NAME) from TBL_LEAVE_ENTRY s where s.EMP_ID = p.PERSONID and substr(s.LEAVE_DATE,7,9) = '" + txtYear + "') as TOTAL " +
                               " FROM " +
                                   " TBLPERSON P; ";

                conn = new SQLiteConnection(DBConn.connectionString);
                sql_cmd = new SQLiteCommand(query, conn);
                DB = new SQLiteDataAdapter(sql_cmd);
                DS = new DataSet();
                DT = new DataTable();
                DB.Fill(DS);
                Rpt_Leave_Summary cr = new Rpt_Leave_Summary();
                frmCrystalReportViewer crpt = new frmCrystalReportViewer();
                crpt.crptViewer.ReportSource = null;
                crpt.crptViewer.ReportSource = cr;
                DataRow dr = DBConn.getCompanyNameAndAddress();
                cr.SetDataSource(DS.Tables[0]);
                cr.SetParameterValue(0, dr["VCOMPANY_ADDRESS"]);
                cr.SetParameterValue(1, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(2, txtYear);
                cr.SetParameterValue(3, pat + "\\" + dr["VFILE_NAME"]);
                crpt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Exception "+ex.Message);
            }
        }
    }
}