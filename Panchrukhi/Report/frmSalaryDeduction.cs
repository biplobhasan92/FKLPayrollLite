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
namespace Panchrukhi.Report
{
    public partial class frmSalaryDeduction : Form
    {
        public frmSalaryDeduction()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
        }

        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getSecID = 0;


        private void btnSaveAndUpdate_Click(object sender, EventArgs e)
        {

            bool b = DBConn.isDuplicateEntry(Convert.ToInt16((comboBox1.SelectedItem as dynamic).Value), dateTimePicker1.Text.Trim(),txtEmpID.Text.Trim());
            
            if (getSecID == 0)
            {
                if (b) { MessageBox.Show(" Duplicate Entry in Same month ! "); return; }
                string cmdText 
                    = " Insert into TBL_SALARY_DEDUCTION( " +
                            " SL,  " +
                            " CAT_ID, " +
                            " EMP_ID, " +
                            " YEAR_MONTH, " +
                            " DEDUCTED_SALARY " +
                        ")" +
                        " values ( " +
                            "(select CASE WHEN max(SL) >= 0 THEN max(SL) +1 ELSE 1 END FROM TBL_SALARY_DEDUCTION), " +
                            " " + (comboBox1.SelectedItem as dynamic).Value + ", " +
                            " '" + txtEmpID.Text.Trim() + "', "+
                            " '" + dateTimePicker1.Value.ToString("yyyy/MM") + "', "+
                            "  " + Convert.ToInt16(txtDeductAmount.Text.ToString().Trim()) + " ) ";

                if (txtEmpID.Text.Trim() != ""){
                    DBConn.ExecutionQuery(cmdText);
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
                string cmdText = " UPDATE TBL_SALARY_DEDUCTION " +
                                   " set CAT_ID = " + (comboBox1.SelectedItem as dynamic).Value + ", " +
                                   "     EMP_ID = '" + txtEmpID.Text.Trim() + "', " +
                                   "     YEAR_MONTH = '" + dateTimePicker1.Value.ToString("yyyy/MM") + "', " +
                                   "     DEDUCTED_SALARY =  " + Convert.ToInt16(txtDeductAmount.Text.ToString().Trim()) + "  " +
                                 " where SL =    " + getSecID;

                if (DBConn.ExecutionQuery(cmdText))
                {                    
                    ClearData();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Something is wrong in Update ??? !");
                }
            } 
        }

        private void frmSalaryDeduction_Load(object sender, EventArgs e)
        {
            LoadDesigCombo();
            LoadData();
        }



        private void LoadDesigCombo()
        {
            string CommandText = "SELECT * from TBL_DEDUC_CAT";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                comboBox1.Items.Add(new { Text = dr["DEDUC_CAT"].ToString(), Value = dr["SL"].ToString() });
            }
        }


        private void LoadData()
        {
            try
            {
                DBConn.SetConnection();
                DBConn.sql_conn.Open();
                sql_cmd = DBConn.sql_conn.CreateCommand();
                string CommandText = @"Select 
                                            td.*,
                                             (select dc.DEDUC_CAT  from TBL_DEDUC_CAT dc where dc.sl= td.CAT_ID) as DEDUC_CAT
                                        from 
                                             TBL_SALARY_DEDUCTION td";
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
            comboBox1.Text = "";
            txtEmpID.Text = "";
            txtDeductAmount.Text = "";
            getSecID = 0;
            dateTimePicker1.Text = "";
            btnSaveAndUpdate.Text = "Save";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].Index == dataGridView1.Rows.Count) return;
            btnSaveAndUpdate.Text = "Update";
            getSecID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtEmpID.Text  = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDeductAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            string txt = txtEmpID.Text;
            if (txt != "")
            {
                int result = Int32.Parse(txt.Substring(0, 1));
                if (result == 0)
                {
                    MessageBox.Show("Invalid Input", "0 is not allowed as first digit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmpID.Clear();
                }
            }
        }
    }
}
