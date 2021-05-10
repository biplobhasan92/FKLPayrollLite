using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Panchrukhi.DAO
{
    class DatabaseConnection
    {
        // frmReportPersonalInfo frmPrsn = new frmReportPersonalInfo();
        public SQLiteConnection  sql_conn;
        public SQLiteCommand     sql_cmd;
        public SQLiteDataAdapter DA;
        public DataSet DS;





        // set Connection 
        public void SetConnection()
        {
            sql_conn = new SQLiteConnection("Data Source=panchrukhi.db; Version=3; New=False;Compress=True;");
        }





        // Execution Query
        public bool ExecutionQuery(string txtQuery)
        {
            bool returnVal = false;
            try
            {
                SetConnection();
                sql_conn.Open();
                sql_cmd = sql_conn.CreateCommand();
                sql_cmd.CommandText = txtQuery;
                int success = sql_cmd.ExecuteNonQuery();
                if (success > 0)
                {
                    returnVal = true;
                }
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
            sql_conn.Close();
            return returnVal;
        }





        public bool ExecutionQuery(SortedList SLQuery)
        {
            bool s = false;
            string txtQuery = ""; //"begin ";
            try
            {
                SetConnection();
                sql_conn.Open();
                sql_cmd = sql_conn.CreateCommand();
                for (int i = 0; i < SLQuery.Count; i++)
                {
                    txtQuery += SLQuery.GetByIndex(i) + "; ";   
                }

                //txtQuery += " end";

                sql_cmd.CommandText = txtQuery;
                int success = sql_cmd.ExecuteNonQuery();
                if (success > 0)
                {
                    s = true;
                }
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                MessageBox.Show(ex.Message);
            }
            sql_conn.Close();
            return s;
        }







        // Load Data From SQlite Database For Any Table ...
        public void LoadDate(String tableName, String orderByColumnName)
        {
            string CommandText = "Select * from " + tableName + " order by " + orderByColumnName + " desc";
            ExecutionQuery(CommandText);
            DA = new SQLiteDataAdapter(CommandText, sql_conn);
            DataSet DS = new DataSet();
            DS.Reset();
            DA.Fill(DS);
            if (DS.Tables[0].Rows.Count > 0)
            {

            }
        }






        // Delete Function For All Form Int Val
        public bool DeleteTableRowInt(String tableName, String whereConditionColName, int whereConditionValue) {

            bool returnVal = false;
            try
            {
                DialogResult result = MessageBox.Show("Do You Want to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    string cmdText = "delete from " + tableName + " where  " + whereConditionColName + " = " + whereConditionValue;
                    if (ExecutionQuery(cmdText))
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = false;
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message + " - Problem in DeleteTableRowInt()");
            }
            return returnVal;
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






        // Delete Function For All Form String Val
        public bool DeleteTableRowStr(String tableName, String whereConditionColName, String whereConditionValue)
        {               //                   "TBLCATEGORY"     "NCATID"                      "1"
            bool returnVal = false;
            try {
                DialogResult result = MessageBox.Show("Do You Want to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    string cmdText = "delete from " + tableName + " where  " + whereConditionColName + " = " + whereConditionValue;
                    if (ExecutionQuery(cmdText))
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = false;
                    }
                }
            } catch (Exception exc) {
                MessageBox.Show(exc.Message + " - Problem DeleteTableRowStr()");
            }
            return returnVal;
        }









        // Get Weekends for make absent disable. 
        public bool getWeekends(string attendDate, string PersonID)
        {
            List<string> getHolidayName = new List<string>();
            string[] dayName = { "Friday", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" };
            string getGeneralHoliday = checkGeneralWeekend(PersonID); // Get value of General Holiday string from database.

            // if General Holiday index val not equals to '0' 
            // add holiday in getHolidayName list for that index from 'dayName' static array
            for (int i = 0; i < getGeneralHoliday.Length; i++)
            {
                if (getGeneralHoliday[i] != '0')
                {
                    getHolidayName.Add(dayName[i]);
                }
            }

            DateTime dt = Convert.ToDateTime(attendDate);
            for (int i = 0; i < getHolidayName.Count; i++)
            {
                if (dt.DayOfWeek.ToString().Equals(getHolidayName[i].ToString()))
                { return true; }
            }
            return false;
        }








        public string checkGeneralWeekend(string PersonID)
        {
            string returnVal = "";
            try
            {
                DataSet dsTemp = new DataSet();
                DataSet DS = new DataSet();

                string cmdText = "select VDAYSFLAG from TBL_WEEKEND where  VSPECIALID = '" + PersonID + "' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DA.Fill(dsTemp);

                if (dsTemp.Tables[0].Rows.Count > 0)
                    DS = dsTemp;
                else
                {
                    cmdText = "select VDAYSFLAG from TBL_WEEKEND where  VSPECIALID = 'GENERAL' ";
                    ExecutionQuery(cmdText);
                    DA = new SQLiteDataAdapter(cmdText, sql_conn);
                    DA.Fill(DS);
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
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



        // Holiday Validation Method. if return true then make reporting grid row green.
        public bool checkGeneralWeekend00(string attenDate)
        {
            bool returnVal = false;
            try
            {
                string cmdText = "select VDAYSFLAG from TBL_WEEKEND where  VSPECIALID = '00' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DataSet DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkDataIfItUsedOtherTable()");
            }
            return returnVal;
        }







        public string getLeaveClsl(string attendDate, string PersonID)
        {
            string returnVal = "";
            try
            {
                string cmdText = "select (select s.LEAVE_NAME from TBL_LEAVE_SETTINGS s where s.SL = le.LEAVE_NAME) as leave_name from TBL_LEAVE_ENTRY le" +
                    " where  EMP_ID = '" + PersonID + "' and LEAVE_DATE = '" + attendDate + "' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
                    {
                        returnVal = dr["leave_name"].ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem getLeaveClsl()");
            }
            return returnVal;
        }





        public Boolean isLeaveClsl(string attendDate, string PersonID)
        {
            bool returnVal = false;
            try
            {
                string cmdText = "select * from TBL_LEAVE_ENTRY le" +
                    " where  EMP_ID = '" + PersonID + "' and LEAVE_DATE = '" + attendDate + "' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem getLeaveClsl()");
            }
            return returnVal;
        }






        public Boolean isDuplicateEntry(int cat_id, string attendDate, string PersonID)
        {
            bool returnVal = false;
            try
            {
                string cmdText = @"select 
                                     sd.*
                                   from 
                                     TBL_SALARY_DEDUCTION sd
                                   where 
                                     sd.EMP_ID = '"+PersonID+"' and sd.YEAR_MONTH = '"+attendDate+"' and sd.CAT_ID = "+cat_id+" ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem isDuplicateEntry()");
            }
            return returnVal;
        }




        // Validate Data Before Delete. if data used in Other Table.
        public bool checkDataIfItUsedOtherTable(String tableName, String whereConditionColName, int whereConditionValue) {
            bool returnVal = false;
            try
            {
                string cmdText = "select " + whereConditionColName + " from " + tableName + " where  " + whereConditionColName + " = " + whereConditionValue;
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DataSet DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message + " - Problem checkDataIfItUsedOtherTable()");
            }
            return returnVal;
        }





        
        // Holiday Validation Method. if return true then make reporting grid row green.
        public bool checkDataIfItUsedOtherTableStr(String tableName, String whereConditionColName, string whereConditionValue)
        {
            bool returnVal = false;
            try
            {
                string cmdText = "select "+whereConditionColName+" from "+tableName+" where  "+whereConditionColName+" = '"+whereConditionValue+"' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DataSet DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkDataIfItUsedOtherTableStr()");
            }
            return returnVal;
        }






        public bool checkIfTableIsReturnNull(string table_name){

            bool returnVal = false;
            try
            {
                string cmdText = "select * from "+table_name;
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DataSet DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkIfTableIsReturnNull()");
            }
            return returnVal;
        }




        // Holiday Validation Method. if return true then make reporting grid row green.
        public bool checkGeneralHoliday(String tableName, String whereConditionColName, string whereConditionValue)
        {
            bool returnVal = false;
            try
            {
                string cmdText = "select " + whereConditionColName + " from " + tableName + " where  " + whereConditionColName + " = '"+ whereConditionValue+"' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DataSet DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    returnVal = true;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkDataIfItUsedOtherTable()");
            }
            return returnVal;
        }








        // User Check In LogIn Table  
        public int checkUserToLogIn(String userName, String Password)
        {
            int LoginType = 0;
            try
            {
                string cmdText = "select LoginType from TBLUSERS where Username = '"+userName+ "' AND Password = '"+Password+ "' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        LoginType = Convert.ToInt32(DS.Tables[0].Rows[i]["LoginType"]);
                    //returnVal = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " - Problem checkUserToLogIn()");
            }
            return LoginType;
        }




        // GET Company name 
        public DataRow getCompanyNameAndAddress() {

            try
            {
                string cmdText = "select * from TBL_COMPANY";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                    return DS.Tables[0].Rows[0];
                else
                {
                    MessageBox.Show("Company Name was not set properly. Please contact your system administrator.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Company Name was not set properly. Please contact your system administrator.");
            }
            return null;
        }



        public DataRow getEmpInfo(string empID)
        {

            try
            {
                string cmdText = @" SELECT 
                                       P.VNAME, 
                                       (SELECT VCLASSNAME FROM TBLCLASS TC WHERE TC.NCLASSID = P.NCLASSID) as CLASS,
                                       (SELECT VSECTION FROM TBLSECTION TS WHERE TS.NSECID = P.NSECID) as SECTION,
                                        (SELECT VSLOTNAME || '(' || VINTIME || '-' || VOUTTIME || ')'  FROM TBLATTENSLOT where(TBLATTENSLOT.NSLOTID = P.NSLOTID)) as SHIFT,
                                       (select VCATEGORY from TBLCATEGORY where P.NCATID = TBLCATEGORY.NCATID) as CATEGORY
                                from
                                     TBLPERSON P where p.PERSONID = '"+ empID + "' ";
                ExecutionQuery(cmdText);
                DA = new SQLiteDataAdapter(cmdText, sql_conn);
                DS = new DataSet();
                DS.Reset();
                DA.Fill(DS);
                if (DS.Tables[0].Rows.Count > 0)
                    return DS.Tables[0].Rows[0];
                else
                {
                    MessageBox.Show("Company Name was not set properly. Please contact your system administrator.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Company Name was not set properly. Please contact your system administrator.");
            }
            return null;
        }

    }
}
