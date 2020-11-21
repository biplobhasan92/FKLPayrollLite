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
        }

        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getSecID = 0;


        private void btnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            string lvName = txtLeaveName.ToString().Trim();
            int totalDays = Convert.ToInt32(txtLeaveTtlDay.ToString().Trim());
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
                    // ClearData();
                    // LoadData();
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
                                   "  set TOTAL_DAYS =  " + totalDays+"  " +
                                 "  where SL =    " + getSecID;

                if (DBConn.ExecutionQuery(cmdText))
                {
                    MessageBox.Show("Data Update Succeed !");
                    // ClearData();
                    // LoadData();
                }
                else
                {
                    MessageBox.Show("Something is wrong in Update ??? !");
                }
            }
        }
    }
}
