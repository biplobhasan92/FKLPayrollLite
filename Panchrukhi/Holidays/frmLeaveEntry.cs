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

namespace Panchrukhi.Holidays
{
    public partial class frmLeaveEntry : Form
    {
        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS;
        private DataTable DT;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getHID = 0;

        public frmLeaveEntry()
        {
            InitializeComponent();
            dtPkrFormDate.Format =  DateTimePickerFormat.Custom;
            dtPkrFormDate.CustomFormat = "dd/MM/yyyy";
            LoadData();
        }



        // To Load Class Combo in CLASS Form. calling from Load Form.
        private void LoadLeavetCombo()
        {
            string CommandText = "SELECT * from TBL_LEAVE_SETTINGS ";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cbxLeaveCat.DisplayMember = "Text";
            cbxLeaveCat.ValueMember = "Value";
            cbxLeaveCat.SelectedItem = 0;
            cbxLeaveCat.SelectedText = "--select--";
            foreach (DataRow dr in DT.Rows)
            {
                cbxLeaveCat.Items.Add(new { Text = dr["LEAVE_NAME"].ToString(), Value = dr["SL"].ToString() }); //NCATID               
            }
        }


        private void LoadLeavGridCombo()
        {
            string CommandText = "SELECT SL, LEAVE_NAME from TBL_LEAVE_SETTINGS ";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DT = new DataTable();
            // DB.Fill(dsSLOT);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            //colCombo.DisplayMember = "LEAVE_NAME";
            //colCombo.ValueMember = "SL";
            //this.colCombo.DisplayMember = "LEAVE_NAME";
            //this.colCombo2.DisplayMember = "LEAVE_NAME";
            //this.colCombo.ValueMember = "SL";
            //this.colCombo2.ValueMember = "SL";
            //this.colCombo.DataSource = DT;
            //this.colCombo2.DataSource = DT;
        }


        private void frmLeaveEntry_Load(object sender, EventArgs e)
        {
            LoadLeavetCombo();
            // LoadLeavGridCombo();
        }



        private void btnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            string getEmpID = txtEmpID.Text.ToString();
            int leaveName = Convert.ToInt32((cbxLeaveCat.SelectedItem as dynamic).Value);
            string lvDate = dtPkrFormDate.Text.ToString();
            string cmdText = "";

            try
            {
                if (getHID == 0)
                {

                    /*
                    for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                    {
                        foreach (DataGridViewRow dr in dataGridview.Rows)
                        {
                            if (Convert.ToDateTime(dr.Cells[colDate.Index].Value) == date)
                            {
                                DialogResult dResult = MessageBox.Show(this, "Date conflicts on " + date.ToString() + ". Do you still want to update?", "Date conflict", MessageBoxButtons.YesNo);
                                if (dResult == DialogResult.No) return;
                            }
                        }
                    }
                    */


                    for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                    {
                        cmdText = 
                            " Insert into TBL_LEAVE_ENTRY (" +
                                 " SL_LEAVE, " +
                                 " EMP_ID, " +
                                 " LEAVE_NAME, "+
                                 " LEAVE_DATE "+
                            " ) " +
                            " values (" +
                                 "(select CASE WHEN max(SL_LEAVE) >= 0 THEN max(SL_LEAVE) +1 ELSE 1 END FROM TBL_LEAVE_ENTRY), " +
                                 " '" + getEmpID + "'," +
                                 "  IFNULL("+leaveName+", 0)," +
                                 " '" + date.ToShortDateString()+ "' )";
                    }

                    if (txtEmpID.Text != "" && DBConn.ExecutionQuery(cmdText))
                    {
                        MessageBox.Show("Insert Succeed !");
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
                    cmdText = "  UPDATE TBL_LEAVE_ENTRY " +
                                "  set EMP_ID  = '" + getEmpID + "', " +
                                "   LEAVE_NAME =  " + leaveName + ",  " +
                                "   LEAVE_DATE =  '" + lvDate + "' " +
                                "  where SL_LEAVE=  " + getHID;

                    if (DBConn.ExecutionQuery(cmdText))
                    {
                        MessageBox.Show("Data Update Succeed !");
                        ClearData();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Something is wrong in Update ??? !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /*
            private void BtnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            if (getHID == 0)
            {
                string cmdText = "";
                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    foreach (DataGridViewRow dr in dataGridView.Rows)
                    {
                        if(Convert.ToDateTime(dr.Cells[colHDate.Index].Value)== date)
                        {
                            DialogResult dResult = MessageBox.Show(this, "Date conflicts on " + date.ToString() + ". Do you still want to update?", "Date conflict", MessageBoxButtons.YesNo);
                            if (dResult == DialogResult.No) return;  
                        }
                    }
                }

                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    cmdText += "Delete from TBL_HOLIDAY where DDATE between '"+ dtPkrFormDate.Text + "' and '"+ dtPkrToDate.Text + "'; ";
                }


                for (DateTime date = Convert.ToDateTime(dtPkrFormDate.Text); date.Date <= Convert.ToDateTime(dtPkrToDate.Text); date = date.AddDays(1))
                {
                    cmdText += " INSERT INTO TBL_HOLIDAY(NHID, DDATE, N_HCATID) "+
                               " VALUES( "+
                               "   (select CASE WHEN max(NHID) >= 0 THEN max(NHID)+1 ELSE 1 END FROM TBL_HOLIDAY)," +
                               " '"+date.ToShortDateString()+ "', " +
                               " IFNULL(" + (cmbHolidayCat.SelectedItem as dynamic).Value + ", 1) " +
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
             
             
        */


        public void ClearData()
        {
            txtEmpID.Text ="";
            cbxLeaveCat.Text = "";
            dtPkrFormDate.Text = "";
            getHID = 0;
        }




        private void LoadData()
        {
            try
            {
                DS = new DataSet();
                DT = new DataTable();
                DBConn.SetConnection();
                DBConn.sql_conn.Open();
                string CommandText = @"select
                                               le.SL_LEAVE,
                                               le.EMP_ID as EMP_ID,
                                               (select s.LEAVE_NAME from TBL_LEAVE_SETTINGS s where s.SL =le.LEAVE_NAME) as LEAVE_NAME,
                                               le.LEAVE_DATE as LEAVE_DATE 
                                        from TBL_LEAVE_ENTRY le;";
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DS.Reset();
                DB.Fill(DS);
                //DT = DS.Tables[0];
                /*
                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.Name = "cmb";
                cmb.MaxDropDownItems = 4;
                cmb.Items.Add("True");
                cmb.Items.Add("False"); 
                dataGridview.Columns.Add(cmb);*/
                string[] stringList = {"AB1", "AB2", "AB3"};
                dataGridview.Rows.Clear();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
                    {
                        dataGridview.Rows.Add();
                        var cellSample = new DataGridViewComboBoxCell();
                        cellSample.DataSource = stringList;
                        dataGridview.Rows[dataGridview.Rows.Count - 1].Cells[colSL.Index].Value = dataGridview.Rows.Count;
                        dataGridview.Rows[dataGridview.Rows.Count - 1].Cells[colEmpID.Index].Value = dr["EMP_ID"].ToString();
                        dataGridview.Rows[dataGridview.Rows.Count - 1].Cells[colLEAVE_NAME.Index].Value = dr["LEAVE_NAME"].ToString();
                        dataGridview.Rows[dataGridview.Rows.Count - 1].Cells[colDate.Index].Value = dr["LEAVE_DATE"].ToString();
                        dataGridview.Rows[dataGridview.Rows.Count - 1].Cells[colCombo.Index] = cellSample;
                    }
                }
                DBConn.sql_conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DBConn.DeleteTableRowInt("TBL_LEAVE_ENTRY", "SL_LEAVE", getHID))
            {
                MessageBox.Show("Row Deleted !");
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("", "Unable to delete!!!! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void dataGridview_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getHID = Convert.ToInt32(dataGridview.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtEmpID.Text = dataGridview.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbxLeaveCat.Text = dataGridview.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtPkrFormDate.Text = dataGridview.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dataGridview_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridview.SelectedRows.Count == 0 || dataGridview.SelectedRows[0].Index == dataGridview.Rows.Count) return;
            getHID = Convert.ToInt32(dataGridview[0, dataGridview.SelectedRows[0].Index].Value.ToString());
            txtEmpID.Text = dataGridview[1, dataGridview.SelectedRows[0].Index].Value.ToString();
            cbxLeaveCat.Text = dataGridview[2, dataGridview.SelectedRows[0].Index].Value.ToString();
            dtPkrFormDate.Text= dataGridview[3, dataGridview.SelectedRows[0].Index].Value.ToString();
        }
    }
}
