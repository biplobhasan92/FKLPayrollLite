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
using System.IO;
using System.Drawing;

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
        SQLiteConnection conn;
        DataTable dt;
        SQLiteDataAdapter da;
        DataSet ds;
        int selectID = 0;
        string imageLocation = "";


        private void ApplicationInfo_Load(object sender, EventArgs e)
        {
           LoadData();
           // LoadGrid();
        }

        void LoadGrid()
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
                    byte[] byteBLOBData = (byte[])dr["BLOGO"];
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    selectID = Convert.ToInt32(dr["NCOMPANYID"].ToString());
                    txtCompanyName.Text = dr["VCOMPANY_NAME"].ToString();
                    txtAddress.Text = dr["VCOMPANY_ADDRESS"].ToString();
                    txtContact.Text = dr["VCONTACT"].ToString();
                    pbxLogo.Image = Image.FromStream(stmBLOBData);
                }
                db.sql_conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message.ToString());
            }
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
                    byte [] byteBLOBData = (byte[])dr["BLOGO"];
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    selectID = Convert.ToInt32(dr["NCOMPANYID"].ToString());
                    txtCompanyName.Text = dr["VCOMPANY_NAME"].ToString();
                    txtAddress.Text = dr["VCOMPANY_ADDRESS"].ToString();
                    txtContact.Text = dr["VCONTACT"].ToString();
                    pbxLogo.Image = Image.FromStream(stmBLOBData);
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

            DataRow dr = db.getCompanyNameAndAddress();
            string prevImgName = dr["VFILE_NAME"].ToString();            
            string basePath = Application.StartupPath;
            string prevImgPath = basePath +"\\"+ prevImgName;
            string contentType = "";
            string name    = txtCompanyName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string contact = txtContact.Text.Trim();
            string fileName = "";
            var time = DateTime.Now.TimeOfDay.ToString();
            fileName = Path.GetFileName(imageLocation);
            MemoryStream strm = new MemoryStream();
            pbxLogo.Image.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = strm.ToArray();
            if (File.Exists(prevImgPath))
            {
                File.Delete(prevImgPath);
            }

            switch (Path.GetExtension(fileName))
            {
                case ".jpg":
                    contentType = "image/jpg";
                    break;
                case ".JPG":
                    contentType = "image/jpg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".bmp":
                    contentType = "image/bmp";
                    break;
            }

            try
            {
                string s = @" update TBL_COMPANY SET
                                    VCOMPANY_NAME = '"+name+"', VCOMPANY_ADDRESS='"+address+"', VCONTACT = '"+contact+"', " +
                                   " VCONTENT_TYPE = '"+ contentType + "', VFILE_NAME = '"+ fileName + "', BLOGO = @Pic " +
                            " where NCOMPANYID = " +selectID+" ";
                conn = new SQLiteConnection(db.connectionString);
                cmd = new SQLiteCommand(s, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@Pic", pic);
                cmd.ExecuteNonQuery();
                File.Copy(imageLocation, Path.Combine(basePath + "\\", Path.GetFileName(imageLocation)), true);
                conn.Close();
                LoadData();
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
            pbxLogo.ImageLocation = "";
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


        private void btnUpload_Click(object sender, EventArgs e)
        {
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pbxLogo.ImageLocation = imageLocation;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            txtCompanyName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            byte[] byteBLOBData = (byte[])dataGridView1.Rows[e.RowIndex].Cells[4].Value;
            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            pbxLogo.Image = Image.FromStream(stmBLOBData);
        }
    }
}
