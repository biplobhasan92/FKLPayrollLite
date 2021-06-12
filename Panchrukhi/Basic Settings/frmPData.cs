using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panchrukhi.DAO;
using System.Data.SQLite;
using Panchrukhi.Report;

namespace Panchrukhi
{
    public partial class frmPData : Form
    {
        public frmPData()
        {
            InitializeComponent();
            txtPDOJ.Format = txtPDOB.Format = DateTimePickerFormat.Custom;
            txtPDOJ.CustomFormat = txtPDOB.CustomFormat = "dd/MM/yyyy";
            
        }

        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet    DS     = new DataSet();
        private DataTable  DT     = new DataTable();
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        int getEMPID        = 0;



        // Slot Form Close Button
        private void BtnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        


        private void BtnSaveAndUpdate_Click(object sender, EventArgs e)
        {
            
            // Regular
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            
            // First time if any combo box table is null .
            if (!DBConn.checkIfTableIsReturnNull("TBLCATEGORY"))    { MessageBox.Show("CATEGORY NOT FOUND", "Add some Category and first add N/A ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!DBConn.checkIfTableIsReturnNull("TBLCLASS"))       { MessageBox.Show("CLASS NOT FOUND", "Add some CLASS and first add N/A ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!DBConn.checkIfTableIsReturnNull("TBLDESIGNATION")) { MessageBox.Show("DESIGNATION NOT FOUND", "Add some DESIGNATION and first add N/A ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!DBConn.checkIfTableIsReturnNull("TBLSECTION"))     { MessageBox.Show("SECTION NOT FOUND", "Add some SECTION and first add N/A ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!DBConn.checkIfTableIsReturnNull("TBLATTENSLOT"))   { MessageBox.Show("SHIFT NOT FOUND", "Add SHIFT and first add N/A ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (string.IsNullOrEmpty(txtPID.Text))     { errorProviderPData.SetError(txtPID, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear();  }
            if (string.IsNullOrEmpty(txtPName.Text))   { errorProviderPData.SetError(txtPName, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear();}
            if (!string.IsNullOrEmpty(txtPEmail.Text)) { if (!regex.IsMatch(txtPEmail.Text.Trim())) { errorProviderPData.SetError(txtPEmail, errorProviderPData.Icon.ToString()); return;}}

            if (string.IsNullOrEmpty(txtPBasic.Text.Trim())) { errorProviderPData.SetError(txtPBasic, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear(); }
            if (string.IsNullOrEmpty(txtPHouseRent.Text.Trim())) { errorProviderPData.SetError(txtPHouseRent, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear(); }
            if (string.IsNullOrEmpty(txtPMedical.Text.Trim())) { errorProviderPData.SetError(txtPMedical, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear(); }
            if (string.IsNullOrEmpty(txtPTransport.Text.Trim())) { errorProviderPData.SetError(txtPTransport, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear(); }
            if (string.IsNullOrEmpty(txtPSalary.Text.Trim())) { errorProviderPData.SetError(txtPSalary, errorProviderPData.Icon.ToString()); return; } else { errorProviderPData.Clear(); }

            if (getEMPID == 0) // NEMPID
            {
                if (txtPID.Text.Trim() != "")
                {
                    if (DBConn.checkDataIfItUsedOtherTableStr("TBLPERSON", "PERSONID", txtPID.Text.Trim())) { MessageBox.Show("", "Duplicate ID Not Allowed !", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    try
                    {                           
                        string cmdText =
                            "INSERT INTO TBLPERSON " +
                            "  (          " +
                            "    NEMPID,  " +
                            "    PERSONID," +
                            "    VNAME,   " +
                            "    VGENDER, " +
                            "    VADDRESS," +
                            "    DDOB,    " +
                            "    VMOBLE,  " +
                            "    VEMAIL,  " +
                            "    NSTATUS, " +
                            "    DDOJ,    " +
                            "    NCATID,  " +
                            "    NDESIGID," +
                            "    NBASIC,  " +
                            "    NHRENT,  " +
                            "    NTRANSPORT, " +
                            "    NMEDICAL," +
                            "    NSALARY, " +
                            "    NSLOTID, " +
                            "    NCLASSID," +
                            "    NSECID,  " +
                            "    VFATHER_NAME,  " +
                            "    VMOTHER_NAME,  " +
                            "    VEMERGENCY_CONTRACT   " +
                            "  )          " +
                            "  values     " +
                            "  (          " +
                                "(select CASE WHEN max(NEMPID) >= 0 THEN max(NEMPID) + 1 ELSE 1 END FROM TBLPERSON), " +
                                "  '" + txtPID.Text + "',     " +
                                "  '" + txtPName.Text.Trim() + "',   " +
                                "  IFNULL('" + cmbGender.SelectedItem.ToString() + "', 'MALE'), " +
                                "  '" + txtPAddress.Text.Trim() + "'," +
                                "  '" + txtPDOB.Text + "',    " +
                                "  '" + txtPMobile.Text.Trim() + "', " +
                                "  '" + txtPEmail.Text.Trim() + "',  " +
                                "  IFNULL(" + cmbStatus.SelectedIndex + ", 1)," +
                                "  '" + txtPDOJ.Text + "',    " +
                                " IFNULL(" + (cmbCat.SelectedItem as dynamic).Value + ", 1), " +
                                " IFNULL(" + (cmbDesig.SelectedItem as dynamic).Value + ", 1), " +
                                " '" + txtPBasic.Text.Trim() + "',  " +
                                " '" + txtPHouseRent.Text.Trim() + "',  " +
                                " '" + txtPTransport.Text.Trim() + "',  " +
                                " '" + txtPMedical.Text.Trim() + "',  " +
                                " '" + txtPSalary.Text.Trim() + "',  " +
                                " IFNULL(" + (cmbSlot.SelectedItem as dynamic).Value + ", 1), " +
                                " IFNULL(" + (cmbClass.SelectedItem as dynamic).Value + ", 1), " +
                                " IFNULL(" + (cmbSection.SelectedItem as dynamic).Value + ", 1), " +
                                "  '" + txtFather.Text + "',  " +
                                "  '" + txtMother.Text + "',  " +
                                "  '" + txtEmergencyContact.Text + "'  " +
                        " ) ";
                        DBConn.ExecutionQuery(cmdText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception to Save", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearData();
                    LoadData();
                    LoadDefaultComboValue();
                }
                else
                {
                    if(txtPID.Text == "")
                        MessageBox.Show("Enter ID First!");
                    if((cmbSlot.SelectedItem as dynamic).Value == 1)
                        MessageBox.Show("Select Shift First!");
                    else
                        MessageBox.Show("Something is wrong ??? !");
                }
            }
            else
            {
                bool isExecute = false;
                try
                {
                    string cmdText =
                    "UPDATE " +
                            " TBLPERSON " +
                    " set " +
                            " PERSONID = '" + txtPID.Text.Trim() + "',     " +
                            " VNAME    = '" + txtPName.Text.Trim() + "',   " +
                            " VGENDER  = IFNULL('" + cmbGender.SelectedItem + "', 'MALE'), " +
                            " VADDRESS = '" + txtPAddress.Text.Trim() + "', " +
                            " DDOB     = '" + txtPDOB.Text + "',     " +
                            " VMOBLE   = '" + txtPMobile.Text.Trim() + "',  " +
                            " VEMAIL   = '" + txtPEmail.Text.Trim() + "',   " +
                            " NSTATUS  = IFNULL(" + cmbStatus.SelectedIndex + ", 1), " +
                            " DDOJ     = '" + txtPDOJ.Text.Trim() + "',   " +
                            " NCATID   = IFNULL(" + (cmbCat.SelectedItem as dynamic).Value + ", 1),  " +
                            " NDESIGID = IFNULL(" + (cmbDesig.SelectedItem as dynamic).Value + ", 1)," +
                            " NBASIC   = " + txtPBasic.Text.Trim() + ", " +
                            " NHRENT  = " + txtPHouseRent.Text.Trim() + ", "+
                            " NTRANSPORT = " + txtPTransport.Text.Trim()+ ", " +
                            " NMEDICAL = " + txtPMedical.Text.Trim() + ", " +
                            " NSALARY  = " + txtPSalary.Text.Trim() + ",  " +
                            " NSLOTID  = IFNULL(" + (cmbSlot.SelectedItem as dynamic).Value + ",  1),  " +
                            " NCLASSID = IFNULL(" + (cmbClass.SelectedItem as dynamic).Value + ", 1),  " +
                            " NSECID   = IFNULL(" + (cmbSection.SelectedItem as dynamic).Value + ",1),  " +
                            " VFATHER_NAME        = '" + txtFather.Text.Trim() + "',   " +
                            " VMOTHER_NAME        = '" + txtMother.Text.Trim() + "',   " +
                            " VEMERGENCY_CONTRACT = '" + txtEmergencyContact.Text.Trim() + "'   " +
                    " where  " +
                            " NEMPID =" + getEMPID;// NEMPID

                    isExecute = DBConn.ExecutionQuery(cmdText);
                }
                catch (Exception ex){
                    MessageBox.Show("Exception to Update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (isExecute)
                {
                    LoadData();
                    ClearData();
                    LoadDefaultComboValue();
                }
                else
                {
                    MessageBox.Show("Something is wrong in Update ??? !");
                }
            }
        }

        
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            LoadData();
        }





        // Form Clear Method
        void ClearData()
        {
            txtFather.Text= txtMother.Text = txtEmergencyContact.Text  = txtPID.Text = txtPName.Text = cmbGender.Text = txtPAddress.Text = txtPDOB.Text = txtPMobile.Text = 
            txtPEmail.Text = cmbStatus.Text = txtPDOJ.Text = cmbCat.Text = cmbDesig.Text = txtPSalary.Text = cmbSection.Text = cmbClass.Text = cmbSlot.Text = "";
            getEMPID = 0; btnSaveAndUpdate.Text = "Save";
            txtPBasic.Text = txtPTransport.Text = txtPMedical.Text = txtPHouseRent.Text = txtPSalary.Text = "0";
            LoadDefaultComboValue();
        }


        // Load Data From SQlite Database
        private void LoadData()
        {
            string CommandText = "";
            try{
              CommandText =
                  "  SELECT *, " +
                  "  (select VCATEGORY from TBLCATEGORY where TBLPERSON.NCATID = TBLCATEGORY.NCATID) as CATEGORY,   " +
                  "  (SELECT VSLOTNAME FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = TBLPERSON.NSLOTID))   as SLOT, " +
                  "  (SELECT \"(\" || VINTIME ||\" - \"||  VOUTTIME || \")\"  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = TBLPERSON.NSLOTID))   as IN_OUT_TIME, " +
                  "  (select VDESIGNATIONNAME from TBLDESIGNATION where TBLDESIGNATION.NDESIGID = TBLPERSON.NDESIGID ) as DESIGNATION, " +
                  "  (SELECT VCLASSNAME FROM TBLCLASS   TC WHERE TC.NCLASSID = TBLPERSON.NCLASSID) as CLASS,  " +
                  "  (SELECT VSECTION   FROM TBLSECTION TS WHERE TS.NSECID   = TBLPERSON.NSECID) as SECTION, " +
                  "  CASE TBLPERSON.NSTATUS WHEN 1 THEN 'ACTIVE' ELSE 'INACTIVE' END STATUS" +
                " FROM TBLPERSON";
                DBConn.ExecutionQuery(CommandText);
                DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                dataGridView.Rows.Clear();
                foreach (DataRow dr in DT.Rows)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colVPERSONID.Index].Value = dr["PERSONID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colVNAME.Index].Value   = dr["VNAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCATNAME.Index].Value = dr["CATEGORY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colCLASS.Index].Value   = dr["CLASS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSLOT.Index].Value    = dr["SLOT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colTIME.Index].Value    = dr["IN_OUT_TIME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDESIGNATION.Index].Value = dr["DESIGNATION"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSTATUS.Index].Value  = dr["STATUS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colADDRESS.Index].Value = dr["VADDRESS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMOBILE.Index].Value  = dr["VMOBLE"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colFATHER.Index].Value  = dr["VFATHER_NAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colMOTHER.Index].Value  = dr["VMOTHER_NAME"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colEMERGENCY.Index].Value= dr["VEMERGENCY_CONTRACT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDOB.Index].Value = dr["DDOB"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colSECTION.Index].Value = dr["SECTION"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colDOJ.Index].Value = dr["DDOJ"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNCATID.Index].Value  = dr["NCATID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNCLASSID.Index].Value= dr["NCLASSID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNSECID.Index].Value  = dr["NSECID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNEMPID.Index].Value  = dr["NEMPID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNDESIGID.Index].Value= dr["NDESIGID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colVEMAIL.Index].Value  = dr["VEMAIL"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNSTATUS.Index].Value = dr["NSTATUS"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNSALARY.Index].Value = dr["NSALARY"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNSLOT.Index].Value   = dr["NSLOTID"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colVGENDER.Index].Value = dr["VGENDER"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNBASIC.Index].Value  = dr["NBASIC"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNHRENT.Index].Value  = dr["NHRENT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNTRANSPORT.Index].Value = dr["NTRANSPORT"].ToString();
                    dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[colNMEDICAL.Index].Value= dr["NMEDICAL"].ToString();
                }
                btnExcelExporter.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception to Update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void FrmData_Load(object sender, EventArgs e)
        {
            LoadSlotCombo();
            LoadClassCombo();
            LoadSectionCombo();
            LoadDesigCombo();
            LoadCatCombo();
            LoadData();
           // this.Owner.Enabled = false;
            LoadDefaultComboValue();
        }


        // To Load Category Combo in Person Form. calling from Load Form.
        private void LoadCatCombo() {
            string CommandText = "SELECT * from TBLCATEGORY";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cmbCat.DisplayMember = "Text";
            cmbCat.ValueMember = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cmbCat.Items.Add(new { Text = dr["VCATEGORY"].ToString(), Value = dr["NCATID"].ToString() });//NCATID
            }
        }



        private void LoadDefaultComboValue() {
            cmbGender.SelectedIndex  = cmbCat.MaxLength;
            cmbCat.SelectedIndex     = cmbCat.MaxLength;
            cmbClass.SelectedIndex   = cmbClass.MaxLength;
            cmbDesig.SelectedIndex   = cmbDesig.MaxLength;
            cmbSection.SelectedIndex = cmbSection.MaxLength;
            cmbSlot.SelectedIndex    = cmbSlot.MaxLength;
        }



        // To Load Designation Combo in Person Form. calling from Load Form.
        private void LoadDesigCombo()
        {
            string CommandText = "SELECT * from TBLDESIGNATION";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cmbDesig.DisplayMember = "Text";
            cmbDesig.ValueMember = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cmbDesig.Items.Add(new { Text = dr["VDESIGNATIONNAME"].ToString(), Value = dr["NDESIGID"].ToString() });
            }
        }




        private void DataGridData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnSaveAndUpdate.Text = "Update";
            getEMPID        = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtPID.Text     = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text   = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbGender.Text  = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPAddress.Text= dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtPDOB.Text    = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
            txtPMobile.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPEmail.Text  = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbStatus.Text  = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtPDOJ.Text    = dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            cmbCat.Text     = dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
            cmbDesig.Text   = dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtPBasic.Text  = dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtPHouseRent.Text = dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtPTransport.Text = dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtPMedical.Text= dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtPSalary.Text = dataGridView.Rows[e.RowIndex].Cells[16].Value.ToString();
            cmbClass.Text   = dataGridView.Rows[e.RowIndex].Cells[17].Value.ToString();
            cmbSection.Text = dataGridView.Rows[e.RowIndex].Cells[18].Value.ToString();
            cmbSlot.Text    = dataGridView.Rows[e.RowIndex].Cells[19].Value.ToString();
            txtFather.Text  = dataGridView.Rows[e.RowIndex].Cells[20].Value.ToString().Trim();
            txtMother.Text  = dataGridView.Rows[e.RowIndex].Cells[21].Value.ToString().Trim();
            txtEmergencyContact.Text = dataGridView.Rows[e.RowIndex].Cells[22].Value.ToString().Trim();
        }


        private void DataGridData_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0 || dataGridView.SelectedRows[0].Index == dataGridView.Rows.Count) return;            
            btnSaveAndUpdate.Text = "Update";
            getEMPID         = Convert.ToInt32(dataGridView[0, dataGridView.SelectedRows[0].Index].Value.ToString());
            txtPID.Text      = dataGridView[1, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPName.Text    = dataGridView[2, dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbGender.Text   = dataGridView[3, dataGridView.SelectedRows[0].Index].Value.ToString();            
            txtPAddress.Text = dataGridView[4, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPDOB.Text     = dataGridView[5, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPMobile.Text  = dataGridView[6, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPEmail.Text   = dataGridView[7, dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbStatus.Text   = dataGridView[8, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPDOJ.Text     = dataGridView[9, dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbCat.Text      = dataGridView[10,dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbDesig.Text    = dataGridView[11,dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPBasic.Text   = dataGridView[12, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPHouseRent.Text = dataGridView[13, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPTransport.Text = dataGridView[14, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPMedical.Text = dataGridView[15, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtPSalary.Text  = dataGridView[16,dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbClass.Text    = dataGridView[17,dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbSection.Text  = dataGridView[18,dataGridView.SelectedRows[0].Index].Value.ToString();
            cmbSlot.Text     = dataGridView[19,dataGridView.SelectedRows[0].Index].Value.ToString();
            txtFather.Text   = dataGridView[20, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtMother.Text   = dataGridView[21, dataGridView.SelectedRows[0].Index].Value.ToString();
            txtEmergencyContact.Text = dataGridView[22, dataGridView.SelectedRows[0].Index].Value.ToString();
        }





        private void BtnDelete_Click(object sender, EventArgs e)
        {
            bool r = DBConn.DeleteTableRowInt("TBLPERSON", "NEMPID", getEMPID);
            if (r)
            {
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("Unable to delete !");
            }
        }





        // To Load Class Combo in CLASS Form. calling from Load Form.
        private void LoadClassCombo()
        {
            string CommandText = "SELECT * from TBLCLASS";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cmbClass.DisplayMember = "Text";
            cmbClass.ValueMember = "Value";

            foreach (DataRow dr in DT.Rows)
            {
                cmbClass.Items.Add(new { Text = dr["VCLASSNAME"].ToString(), Value = dr["NCLASSID"].ToString() });//NCATID
            }
        }



        // To Load Slot Combo in Slot Form. calling from LoadForm().
        private void LoadSlotCombo()
        {
            string CommandText = "SELECT * from TBLATTENSLOT";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cmbSlot.DisplayMember = "Text";
            cmbSlot.ValueMember = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cmbSlot.Items.Add(new { Text = dr["VSLOTNAME"].ToString(), Value = dr["NSLOTID"].ToString() });//NCATID
            }
        }




        // To Load Section Combo in Slot Form. calling from Load Form.
        private void LoadSectionCombo()
        {
            string CommandText = "SELECT * from TBLSECTION";
            DataSet dsSLOT = new DataSet();
            DBConn.ExecutionQuery(CommandText);
            DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DB.Fill(dsSLOT);
            DT = dsSLOT.Tables[0];
            cmbSection.DisplayMember = "Text";
            cmbSection.ValueMember = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cmbSection.Items.Add(new { Text = dr["VSECTION"].ToString(), Value = dr["NSECID"].ToString() }); //NCATID
            }
        }

        private void frmPData_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Owner.Enabled = true;
        }
        string pat = Application.StartupPath;

        private void btnSaveAndUpdate_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                DataRow dr = DBConn.getCompanyNameAndAddress();
                if (dr == null) return;
                crptPersonalInfo rptObj = new crptPersonalInfo();
                rptObj.SetDataSource(DS.Tables[0]);
                rptObj.SetParameterValue(0, dr["VCOMPANY_NAME"]);
                rptObj.SetParameterValue(1, dr["VCOMPANY_ADDRESS"]);
                rptObj.SetParameterValue(2, pat + "\\" + dr["VFILE_NAME"]);
                frmCrystalReportViewer crpt = new frmCrystalReportViewer();
                crpt.crptViewer.ReportSource = null;
                crpt.crptViewer.ReportSource = rptObj;
                crpt.Show();
            }
            else
            {
                MessageBox.Show("", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void txtPSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateNumber(e);
        }








        public void validateNumber(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
                char.IsSymbol(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }






        private void btnExcelExporter_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
                ExtractDataToCSV(dataGridView);
            else
                MessageBox.Show("", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
        }








        private void ExtractDataToCSV(DataGridView dgv)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {

                columnsHeader += dgv.Columns[i].HeaderText + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.
                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "SaveAllPerson";
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
            }
            // Confirm to the user it has been completed.
        }






        private void txtPID_KeyPress(object sender, KeyPressEventArgs e)
        {            
            string txt = txtPID.Text;
            if (txtPID.Text!="")
            {
                int result = Int32.Parse(txt.Substring(0, 1));
                if (result == 0)
                {
                    MessageBox.Show("Invalid Input", "0 is not allowed as first digit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPID.Clear();
                }
            }            
        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            string CommandText = "";
            string getSearchItem  = "";
            string getSearchValue = txtSearch.Text;

            if (cbxSearch.Text=="ID"){ getSearchItem = "PERSONID"; }
            if (cbxSearch.Text == "Name") { getSearchItem = "VNAME"; }

            if (cbxSearch.Text !="")
            {
                try
                {
                    CommandText =
                    "  SELECT *, " +
                    "  (select VCATEGORY from TBLCATEGORY where TBLPERSON.NCATID = TBLCATEGORY.NCATID) as CATEGORY,   " +
                    "  (SELECT VSLOTNAME FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = TBLPERSON.NSLOTID))   as SLOT, " +
                    "  (SELECT \"(\" || VINTIME ||\" - \"||  VOUTTIME || \")\"  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = TBLPERSON.NSLOTID))   as IN_OUT_TIME, " +
                    "  (select VDESIGNATIONNAME from TBLDESIGNATION where TBLDESIGNATION.NDESIGID = TBLPERSON.NDESIGID ) as DESIGNATION, " +
                    "  (SELECT VCLASSNAME FROM TBLCLASS   TC WHERE TC.NCLASSID = TBLPERSON.NCLASSID) as CLASS,  " +
                    "  (SELECT VSECTION   FROM TBLSECTION TS WHERE TS.NSECID   = TBLPERSON.NSECID) as SECTION, " +
                    "  CASE TBLPERSON.NSTATUS WHEN 1 THEN 'ACTIVE' ELSE 'INACTIVE' END STATUS" +
                  " FROM TBLPERSON WHERE " + getSearchItem + " LIKE '%" + txtSearch.Text + "%'; ";
                    DBConn.ExecutionQuery(CommandText);
                    DB = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
                    DS.Reset();
                    DB.Fill(DS);
                    DT = DS.Tables[0];
                    dataGridView.DataSource = DT;
                    btnExcelExporter.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception to Filter", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Unable to filter data", "Select ID or Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
            }
        }

        private void txtPBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateNumber(e);
        }

        private void txtPHouseRent_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPTransport_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateNumber(e);
        }

        private void txtPMedical_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateNumber(e);
        }

        private void txtPHouseRent_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateNumber(e);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Exception : "+ex.Message);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Exception : " + ex.Message);
            }
        }
    }
}