using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panchrukhi.DAO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Panchrukhi.Report;
using Microsoft.Reporting.WinForms;

namespace Panchrukhi
{
    public partial class frmReportPersonalInfo : Form
    {

        DatabaseConnection DBConn = new DatabaseConnection();
        private DataSet   DS;
        private DataTable DT;
        private SQLiteDataAdapter DA;

        int presents, absents,holidays;




        /*
         * #########################################################################################################################################################
         * ################################### --  Person Tab All Operation --   ###################################################################################
         * #########################################################################################################################################################
         */


        public frmReportPersonalInfo()
        {
            InitializeComponent();
            dtpFstDate.Format = dtpLstDate.Format = DateTimePickerFormat.Custom;
            dtpFstDate.CustomFormat = dtpFstDate.CustomFormat = "dd/MM/yyyy";
        }


        // private method for Person tab ..
        void LoadPersonGenderCombo()
        {
            
        }


        void ClearPrsnForm()
        {
            txtMultID.Text = cmbCategory.Text = cmbClass.Text = cmbDesig.Text = cmbGender.Text = cmbSection.Text = "";
        }

        private void btnPrsnReset_Click(object sender, EventArgs e)
        {
            ClearPrsnForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReportPersonalInfo_Load(object sender, EventArgs e)
        {
            //LoadData();
            LoadCatCombo();
            LoadDesigCombo();
            LoadClassCombo();
            LoadSectionCombo();
            //this.Owner.Enabled = false;
            txtMultID.Enabled  = false;
        }


        // Load Data From SQlite Database
        private void LoadData(bool isDataForPrint)
        {
            string CommandText = "";
            string AndConditions = "";
            string AddColumn = "";
            
            // ADD COLUMN IN DATA GRID VIEW TO SEE MORE INFO ABOUT EMPLOYEES OR STUDENTS
            if (isDataForPrint)
            {
                AddColumn += ", (select VCATEGORY from TBLCATEGORY where P.NCATID = TBLCATEGORY.NCATID) as CATEGORY" + 
                             ", P.VNAME NAME" +
                             ", (select VDESIGNATIONNAME from TBLDESIGNATION where TBLDESIGNATION.NDESIGID = P.NDESIGID ) as DESIGNATION" +
                             ", (SELECT VSLOTNAME || \" (\" || VINTIME ||\" - \"||  VOUTTIME || \")\"  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = P.NSLOTID))   as SHIFT" +
                             ", P.VGENDER GENDER" +
                             ", (SELECT VCLASSNAME FROM TBLCLASS TC WHERE TC.NCLASSID = P.NCLASSID) as CLASS " +
                             ", (SELECT VSECTION   FROM TBLSECTION TS WHERE TS.NSECID = P.NSECID) as SECTION" +
                             ", P.VEMERGENCY_CONTRACT as EMERGENCY";
            }
            else {
                if (ckbxCategory.Checked) { AddColumn += ", (select VCATEGORY from TBLCATEGORY where P.NCATID = TBLCATEGORY.NCATID) as CATEGORY"; }
                if (ckbxName.Checked) { AddColumn += ", P.VNAME NAME"; }
                if (ckbxDesig.Checked) { AddColumn += ", (select VDESIGNATIONNAME from TBLDESIGNATION where TBLDESIGNATION.NDESIGID = P.NDESIGID ) as DESIGNATION"; }
                if (ckbxSlot.Checked) { AddColumn += ", (SELECT VSLOTNAME || \" (\" || VINTIME ||\" - \"||  VOUTTIME || \")\"  FROM TBLATTENSLOT where (TBLATTENSLOT.NSLOTID = P.NSLOTID))   as SHIFT"; }
                if (ckbxGNDR.Checked) { AddColumn += ", P.VGENDER GENDER"; }
                if (ckbxClass.Checked) { AddColumn += ", (SELECT VCLASSNAME FROM TBLCLASS TC WHERE TC.NCLASSID = P.NCLASSID) as CLASS "; }
                if (ckbxSection.Checked) { AddColumn += ", (SELECT VSECTION   FROM TBLSECTION TS WHERE TS.NSECID = P.NSECID) as SECTION"; }
                if (cbxEmergencyNum.Checked) { AddColumn += ", P.VEMERGENCY_CONTRACT as EMERGENCY"; }
            }


            // ADD WHERE CONDITION FOR SEARCH, USING COMBOBOX AND DATE RANGE .....
            if (dtpFstDate.Text.Length > 0 && dtpLstDate.Text.Length > 0)
            {
                string fstDate = dtpFstDate.Value.ToString("yyyyMMdd");
                string lstDate = dtpLstDate.Value.ToString("yyyyMMdd");
                AndConditions += " substr(TA.DATTENDATE,7)||substr(TA.DATTENDATE,4,2)||substr(TA.DATTENDATE,1,2) between '" + fstDate + "' AND '" + lstDate + "' ";
                // substr(TA.DATTENDATE,7)||substr(TA.DATTENDATE,4,2)||substr(TA.DATTENDATE,1,2)
            }

            if (cmbCategory.Text.Length > 0)
            {
                AndConditions += " AND P.NCATID = " + (cmbCategory.SelectedItem as dynamic).Value + " ";
            }

            if (cmbDesig.Text.Length > 0)
            {
                AndConditions += " AND P.NDESIGID = " + (cmbDesig.SelectedItem as dynamic).Value + " ";
            }

            if (cmbGender.Text.Length > 0)
            {
                AndConditions += " AND P.VGENDER = '" + cmbGender.Text + "' ";
            }

            if (cmbClass.Text.Length > 0)
            {
                AndConditions += " AND P.NCLASSID = '" + (cmbClass.SelectedItem as dynamic).Value + "' ";
            }

            if (cmbSection.Text.Length > 0)
            {
                AndConditions += " AND P.NSECID = '" + (cmbSection.SelectedItem as dynamic).Value + "' ";
            }

            if (ckbxSearchID.Checked)
            {
                try
                {
                    string TmpStr = "";
                    foreach (string id in txtMultID.Text.Split(',')) TmpStr += "'" + id.ToString() + "',";// TmpStr += "'" + Convert.ToInt32(id).ToString() + "',";
                    TmpStr = TmpStr.Remove(TmpStr.LastIndexOf(','), 1);
                    AndConditions += " AND P.PERSONID IN (" + TmpStr + ") ";
                }
                catch (Exception e)
                {
                    MessageBox.Show("Invalid Input try like: Example: 100001, 100002, 100005 Or Example: 100001", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            // THIS IS THE QUERY TO UPDATE FOR OUT TIME
            CommandText = " Select distinct P.PERSONID ID, TA.DATTENDATE DATE, IFNULL(IT.INTIME,'') INTIME, IFNULL(OT.OUTTIME,'') OUTTIME, 'STATUS' STATUS " + AddColumn+ " " +
                          "   FROM "+ 
                          " TBLPERSON P "+
                          "   JOIN "+
                          " TBLATTENDANCE_PROCESS_DATA TA "+
                          " LEFT OUTER JOIN"+
                          " (SELECT VEMPID, VINOUTTIME INTIME, DATTENDATE from TBLATTENDANCE_PROCESS_DATA WHERE NATTENTYPE = 1) IT "+
                          " ON "+
                          " (IT.VEMPID = P.PERSONID AND IT.DATTENDATE = TA.DATTENDATE)"+
                          " LEFT OUTER JOIN "+
                          " (SELECT VEMPID, VINOUTTIME OUTTIME, DATTENDATE from TBLATTENDANCE_PROCESS_DATA WHERE NATTENTYPE = 5) OT"+
                          " ON"+
                          " (OT.VEMPID = P.PERSONID AND OT.DATTENDATE = TA.DATTENDATE)";
            
            CommandText += " WHERE "+AndConditions+ " AND P.NSTATUS == 1  ORDER BY TA.DATTENDATE;";
            
            DS = new DataSet();
            DT = new DataTable();
            DBConn.ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DS.Reset();
            DA.Fill(DS);
            DT = DS.Tables[0];
            if (isDataForPrint) return;
            dataGridView.DataSource = DT;            
            ConditionalRowBackgroundColor();
        }



        public void ConditionalRowBackgroundColor()
        {            
            //List<string> getIdList = new List<string>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                // dataGridView.Rows[i].Cells[5].Value = (i+1);
                bool absentCounted = false;                
                string getID = dataGridView.Rows[i].Cells[0].Value.ToString();
                string strIntime   = dataGridView.Rows[i].Cells[2].Value.ToString(); //Intime
                string strOuttime  = dataGridView.Rows[i].Cells[3].Value.ToString(); //OutTime

                bool getHolidayDays= DBConn.checkDataIfItUsedOtherTableStr("TBL_HOLIDAY", "DDATE", dataGridView.Rows[i].Cells[1].Value.ToString());
                bool getWeekend = DBConn.getWeekends(dataGridView.Rows[i].Cells[1].Value.ToString(), getID);
                string getLvClSl  = DBConn.getLeaveClsl(dataGridView.Rows[i].Cells[1].Value.ToString(), getID);

                if (strIntime.Equals("") || strOuttime.Equals(""))
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    dataGridView.Rows[i].Cells[4].Value="Absent";
                    absents++; absentCounted = true;
                }
                else
                {
                    dataGridView.Rows[i].Cells[4].Value = "Present";
                    presents++;
                }

                if (getHolidayDays || getWeekend || !string.IsNullOrEmpty(getLvClSl))
                {
                    if (getHolidayDays)
                    {
                        dataGridView.Rows[i].Cells[4].Value = "Holiday";
                    }
                    else if (getWeekend && getLvClSl == "")
                    {
                        dataGridView.Rows[i].Cells[4].Value = "Weekend";
                    }
                    else if(getLvClSl!="")
                    {
                        dataGridView.Rows[i].Cells[4].Value = getLvClSl;
                    }                    
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.DarkGreen;
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.WhiteSmoke;
                    if (absentCounted) absents--;
                    holidays++;
                }
            }
        }








        // To Load Category Combo in Person Form. calling from Load Form.
        private void LoadCatCombo()
        {
            string CommandText = "SELECT * from TBLCATEGORY";
            DS = new DataSet();
            DT = new DataTable();
            DBConn.ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DA.Fill(DS);
            DT = DS.Tables[0];
            cmbCategory.DisplayMember = "Text";
            cmbCategory.ValueMember   = "Value";

            foreach (DataRow dr in DT.Rows)
            {
                cmbCategory.Items.Add(new { Text = dr["VCATEGORY"].ToString(), Value = dr["NCATID"].ToString() });
            }
        }





        // To Load Designation Combo in Person Form. calling from Load Form.
        private void LoadDesigCombo()
        {
            string CommandText = "SELECT * from TBLDESIGNATION";
            DS = new DataSet();
            DT = new DataTable();
            DBConn.ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DA.Fill(DS);
            DT = DS.Tables[0];
            cmbDesig.DisplayMember = "Text";
            cmbDesig.ValueMember   = "Value";
            foreach (DataRow dr in DT.Rows)
            {
                cmbDesig.Items.Add(new { Text = dr["VDESIGNATIONNAME"].ToString(), Value = dr["NDESIGID"].ToString() });
            }
        }





        // To Load Class Combo in CLASS Form. calling from Load Form.
        private void LoadClassCombo()
        {
            string CommandText = "SELECT * from TBLCLASS";
            DS = new DataSet();
            DT = new DataTable();
            DBConn.ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DA.Fill(DS);
            DT = DS.Tables[0];
            cmbClass.DisplayMember = "Text";
            cmbClass.ValueMember = "Value";

            foreach (DataRow dr in DT.Rows)
            {
                cmbClass.Items.Add(new { Text = dr["VCLASSNAME"].ToString(), Value = dr["NCLASSID"].ToString() });//NCATID
            }
        }






        // To Load Section Combo in Slot Form. calling from Load Form.
        private void LoadSectionCombo()
        {
            string CommandText = "SELECT * from TBLSECTION";
            DS = new DataSet();
            DT = new DataTable();
            DBConn.ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, DBConn.sql_conn);
            DA.Fill(DS);
            DT = DS.Tables[0];
            cmbSection.DisplayMember = "Text";
            cmbSection.ValueMember   = "Value";

            foreach (DataRow dr in DT.Rows)
            {
                cmbSection.Items.Add(new { Text = dr["VSECTION"].ToString(), Value = dr["NSECID"].ToString() });
            }
        }






        private void frmReportPersonalInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
             // this.Owner.Enabled = true;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            btnExcelExporter.Enabled = true;
            // btnPrint.Enabled = true;
            presents = absents = holidays = 0;
            LoadData(false);
        }






        string ConvertDays(int BinVal)
        {
            string Days = "FSSMTWT";
            string binText = Convert.ToString(BinVal, 2).PadLeft(7, '0');
            string tmpStr = "";
            for (int i = 0; i < binText.Length; i++)
            {
                if (binText[i] != '0') tmpStr += Days[i];
                else tmpStr += '0';
            }
            return tmpStr;
        }







        public string checkGeneralWeekend(string PersonID)
        {
            string returnVal = "";
            try
            {
                DataSet dsTemp = new DataSet();
                DataSet DS = new DataSet();

                string cmdText = "select VDAYSFLAG from TBL_WEEKEND where  VSPECIALID = '" + PersonID + "' ";
                DBConn.ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, DBConn.sql_conn);
                DA.Fill(dsTemp);

                if (dsTemp.Tables[0].Rows.Count > 0)
                    DS = dsTemp;
                else
                {
                    cmdText = "select VDAYSFLAG from TBL_WEEKEND where  VSPECIALID = 'GENERAL' ";
                    DBConn.ExecutionQuery(cmdText);
                    DA = new SQLiteDataAdapter(cmdText, DBConn.sql_conn);
                    DA.Fill(DS);
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach ( DataRow dr in DS.Tables[0].Rows)
                    {
                        returnVal = ConvertDays(Convert.ToInt32(dr["VDAYSFLAG"]));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkDataIfItUsedOtherTable()");
            }
            return returnVal;
        }








        private void ckbxSearchID_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbxSearchID.Checked)
            {
                txtMultID.Enabled = true;
                gbxSearchByDetails.Enabled = false;
                ClearPrsnForm();
            }
            else {
                gbxSearchByDetails.Enabled = true;
                txtMultID.Enabled = false;
            }
        }

        private void btnExcelExporter_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
                ExtractDataToCSV(dataGridView);
            else
                MessageBox.Show("", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void btn_pdf_export_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView.Columns.Count);
                            pdfTable.DefaultCell.Padding = 2;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 8f, 15f, 15f, 8f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }


        
        private void btn_datagrid_Click(object sender, EventArgs e)
        {
            /*
            
                if (dataGridView.Rows.Count > 0)
                {
                    DataRow dr = DBConn.getCompanyNameAndAddress();
                    if (dr == null) return;
                    LoadData(true);
                    Report.rptAttendanceData rptObj = new Report.rptAttendanceData();
                    rptObj.SetDataSource(DS.Tables[0]);
                    rptObj.SetParameterValue(0, presents);
                    rptObj.SetParameterValue(1, absents);
                    rptObj.SetParameterValue(4, holidays);
                    rptObj.SetParameterValue(2, dr["VCOMPANY_NAME"]);
                    rptObj.SetParameterValue(3, dr["VCOMPANY_ADDRESS"]);
                    frmCrystalReportViewer crpt = new frmCrystalReportViewer();
                    crpt.crptViewer.ReportSource = rptObj;
                    crpt.Show();
                }
                else
                    MessageBox.Show("", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            */

            DS = new DataSet();
            DT = new DataTable();
            string str = "";
            DT.Columns.Add("Emp ID", typeof(string));
            DT.Columns.Add("Atten Date", typeof(string));
            DT.Columns.Add("Time In", typeof(string));
            DT.Columns.Add("Time Out", typeof(string));
            DT.Columns.Add("Status", typeof(string));
            DT.Columns.Add("Name", typeof(string));
            DT.Columns.Add("Class", typeof(string));
            DT.Columns.Add("Section", typeof(string));
            DT.Columns.Add("Shift", typeof(string));
            DT.Columns.Add("Category", typeof(string));

            if (dataGridView.Rows.Count > 0)
            {
                DataRow dr = DBConn.getCompanyNameAndAddress();         
                foreach (DataGridViewRow dgv in dataGridView.Rows)
                {
                    DataRow drr = DBConn.getEmpInfo(dgv.Cells[0].Value.ToString());
                    DT.Rows.Add(
                        dgv.Cells[0].Value,
                        dgv.Cells[1].Value,
                        dgv.Cells[2].Value,
                        dgv.Cells[3].Value,
                        dgv.Cells[4].Value,
                        drr["VNAME"],
                        drr["CLASS"],
                        drr["SECTION"],
                        drr["SHIFT"],
                        drr["CATEGORY"]
                    );
                }
                DS.Tables.Add(DT);
                DS.WriteXmlSchema("attenReport.xml");  //  rptAttendanceData.xml
                frmCrystalReportViewer frm = new frmCrystalReportViewer();
                rptAttendanceSnd cr = new rptAttendanceSnd();
                cr.SetDataSource(DS);
                cr.SetParameterValue(0, presents);
                cr.SetParameterValue(1, absents);
                cr.SetParameterValue(4, holidays);
                cr.SetParameterValue(2, dr["VCOMPANY_NAME"]);
                cr.SetParameterValue(3, dr["VCOMPANY_ADDRESS"]);
                frm.crptViewer.ReportSource = cr;
                frm.crptViewer.Refresh();
                frm.Show();
            }            
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
            sfd.FileName = "SaveAttendanceData";
            sfd.Filter = "CSV files (*.csv)|*.csv";
            //sfd.Filter = "PDF files (*.pdf)|*.pdf";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridView.Rows.Count > 0)
            {
                DataRow dr = DBConn.getCompanyNameAndAddress();
                if (dr == null) return;
                LoadData(true);
                rptAttendanceData rptObj = new rptAttendanceData();
                rptObj.SetDataSource(DS.Tables[0]);
                rptObj.SetParameterValue(0, presents);
                rptObj.SetParameterValue(1, absents);
                rptObj.SetParameterValue(4, holidays);
                rptObj.SetParameterValue(2, dr["VCOMPANY_NAME"]);
                rptObj.SetParameterValue(3, dr["VCOMPANY_ADDRESS"]);
                frmCrystalReportViewer crpt = new frmCrystalReportViewer();
                crpt.crptViewer.ReportSource = rptObj;
                crpt.Show();
            }
            else
                MessageBox.Show("", "Data Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return; */
        }
    }
}
