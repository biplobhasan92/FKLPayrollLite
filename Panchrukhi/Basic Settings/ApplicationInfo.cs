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
    public partial class ApplicationInfo : Form
    {
        public ApplicationInfo()
        {
            InitializeComponent();
        }

        DatabaseConnection db = new DatabaseConnection();
        SQLiteCommand cmd;
        DataTable dt;
        SQLiteDataAdapter da;
        DataSet ds;
        int selectID = 0;

        private void ApplicationInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            try
            {
                db.SetConnection();
                string s = " select * from TBL_COMPANY ";
                da = new SQLiteDataAdapter(s, db.sql_conn);
                ds = new DataSet();
                dt = new DataTable();
                db.sql_conn.Open();
                da.Fill(ds);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    selectID = Convert.ToInt32(dr["NCOMPANYID"].ToString());
                    txtCompanyName.Text = dr["VCOMPANY_NAME"].ToString();
                    txtAddress.Text = dr["VCOMPANY_ADDRESS"].ToString();
                    txtContact.Text = dr["VCONTACT"].ToString();
                }
                db.sql_conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: "+ex.Message.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectID==0)
            {
                MessageBox.Show("close form and load again");
                return;
            }
            string name    = txtCompanyName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string contact = txtContact.Text.Trim();

            try
            {
                string s = @" update TBL_COMPANY SET
                                    VCOMPANY_NAME = '"+name+"', VCOMPANY_ADDRESS='"+address+"', VCONTACT = '"+contact+"' " +
                            " where NCOMPANYID = "+selectID+" ";                
                db.ExecutionQuery(s);
                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(" Exception : "+ex.Message.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }


        void Clear()
        {
            txtCompanyName.Text =
            txtAddress.Text =
            txtContact.Text = "";
        }

        private void btnResetDatabse_Click(object sender, EventArgs e)
        {
            try
            {
                string s = @"delete from TBLATTENDANCE;delete from TBLATTENDANCE_PROCESS_DATA;
                             delete from TBLATTENSLOT where NSLOTID <> 1;
                             delete from TBLCATEGORY where NCATID <> 1;
                             delete from TBLCLASS where NCLASSID <> 1;
                             delete from TBLDESIGNATION where NDESIGID <> 1;
                             delete from TBLPERSON;
                             delete from TBLSECTION where NSECID <> 1;
                             delete from TBL_ATTEN_DEVICE;
                             delete from TBL_DEDUC_CAT;
                             delete from TBL_HOLIDAY;
                             delete from TBL_HOLIDAY_CATEGORY where N_HCATID <> 1;
                             delete from TBL_LEAVE_ENTRY;
                             delete from TBL_LEAVE_SETTINGS;
                             delete from TBL_PROCESSED_SALARY;
                             delete from TBL_PROMOTION_LOG;
                             delete from TBL_SALARY_DEDUCTION;
                             delete from TBL_WEEKEND;";
                 var v = MessageBox.Show("Data of All Table will be empty", "Are you Sure ? do you want to reset Database ?", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (v==DialogResult.Yes)
                {
                    db.ExecutionQuery(s);
                    MessageBox.Show("Database is empty !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Exception "+ex.Message.ToString());
            }
        }
    }
}
