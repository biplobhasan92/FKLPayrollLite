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

namespace Panchrukhi.Basic_Settings
{
    public partial class frmLeaveSettings : Form
    {
        public frmLeaveSettings()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            LoadData();
        }

        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getSecID = 0;


        private void btnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            string lvName = txtLeaveName.Text.ToString().Trim();
            int totalDays = Convert.ToInt16(txtLeaveTtlDay.Text.ToString().Trim());
            if (getSecID == 0)
            {

                string cmdText = " Insert into TBL_LEAVE_SETTINGS (" +
                                     " SL,  " +
                                     "LEAVE_NAME, " +
                                     "TOTAL_DAYS" +
                                   ")" +
                                 " values (" +
                                     "(select CASE WHEN max(SL) >= 0 THEN max(SL) +1 ELSE 1 END FROM TBL_LEAVE_SETTINGS), " +
                                     " '" + lvName + "'," +
                                     "  " + totalDays + " ) ";

                if (txtLeaveName.Text != "")
                {
                    DBConn.ExecutionQuery(cmdText);
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
                string cmdText = "  UPDATE TBL_LEAVE_SETTINGS " +
                                   "  set LEAVE_NAME = '" + lvName + "', " +
                                   "   TOTAL_DAYS =  " + totalDays+"  " +
                                 "  where SL =    " + getSecID;

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



        private void LoadData()
        {
            try
            {
                DBConn.SetConnection();
                DBConn.sql_conn.Open();
                sql_cmd = DBConn.sql_conn.CreateCommand();
                string CommandText = @"Select * from TBL_LEAVE_SETTINGS";
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                dataGridView1.DataSource = DT;
                DBConn.sql_conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ClearData()
        {
            txtLeaveName.Text = "";
            txtLeaveTtlDay.Text = "";
        }



        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getSecID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtLeaveName.Text   = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLeaveTtlDay.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count) return;
            getSecID = Convert.ToInt32(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString());
            txtLeaveName.Text = dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString();
            txtLeaveTtlDay.Text=dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool checkDataBeforeDelete = DBConn.checkDataIfItUsedOtherTable("TBL_LEAVE_ENTRY", "LEAVE_NAME", getSecID);
            if (!checkDataBeforeDelete && DBConn.DeleteTableRowInt("TBL_LEAVE_SETTINGS", "SL", getSecID))
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
    }
}
