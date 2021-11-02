using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Host
{    
    public class Host
    {
        public MsSqlManager MSSql = null;
        public bool bConnection = false;

        public string Connect()
        {
            if (MSSql != null)
                return "NG";

            string strConnetion = string.Format("server=10.135.200.35;database=GR_Automation;user id=amm;password=amm@123"); //AMM

            MSSql = new MsSqlManager(strConnetion);

            if (MSSql.OpenTest() == false)
            {
                bConnection = false;
                return "NG";
            }
            else
                bConnection = true;

            return "OK";
        }
        public int Host_Get_PrintType(string strCust)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_PRINT_TYPE WHERE PRINT_CUST='{0}'", strCust);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return 0;

            string strType = dt.Rows[0]["PRINT_TYPE"].ToString();
            strType = strType.Trim();

            return Int32.Parse(strType);
        }

        public DataTable Host_Get_PrintAllType()
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_PRINT_TYPE");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public void Host_Set_PrintType(string strCust, string strType)
        {
            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            strCust = strCust.Trim();
            strType = strType.Trim();

            // 지우고 추가
            List<string> queryList = new List<string>();

            Host_Delete_PrintType(strCust);

            string query = string.Format(@"INSERT INTO TB_PRINT_TYPE (DATETIME,PRINT_CUST,PRINT_TYPE) VALUES ('{0}','{1}','{2}')", strSendtime, strCust, strType);
            queryList.Add(query);

            MSSql.SetData(queryList);
        }

        public int Host_Delete_PrintType(string strCust)
        {
            string query = string.Format("DELETE FROM TB_PRINT_TYPE WHERE PRINT_CUST='{0}'", strCust);

            int n = MSSql.SetData(query);

            return n;
        }

        public DataTable Host_Get_BCRFormat()
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_CUST_INFO");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public string Host_Get_Shot1Lot(string strCust, string strBcrName)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_CUST_INFO WHERE CUST='{0}' and NAME='{1}'", strCust, strBcrName);

            DataTable dt = MSSql.GetData(query);

            string str = dt.Rows[0]["MULTI_LOT"].ToString();
            str = str.Trim();

            if (str == "YES")
                str = "NO";
            else
                str = "YES";

            return str;
        }

        public string Host_Get_BcrType(string strCust, string strBcrName)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_CUST_INFO WHERE CUST='{0}' and NAME='{1}'", strCust, strBcrName);

            DataTable dt = MSSql.GetData(query);

            string str = dt.Rows[0]["BCR_TYPE"].ToString();
            str = str.Trim();

            return str;
        }

        public string Host_Get_Bcrcount(string strCust, string strBcrName)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_CUST_INFO WHERE CUST='{0}' and NAME='{1}'", strCust, strBcrName);

            DataTable dt = MSSql.GetData(query);

            string str = dt.Rows[0]["BCR_CNT"].ToString();
            str = str.Trim();

            return str;
        }

        public string Host_Get_GrMethod(string strCust, string strBcrName)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_CUST_INFO WHERE CUST='{0}' and NAME='{1}'", strCust, strBcrName);

            DataTable dt = MSSql.GetData(query);

            string str = dt.Rows[0]["GR_METHOD"].ToString();
            str = str.Trim();

            return str;
        }

        public void Host_Set_BCRFormat(string strCust, string strMultiLot, string strBank, string strBcrType, string strBcrCnt, string strBcrName, string strDeviceIndex, string strLotindex, string strLotDigit, string strWfrQty, string strSpr, string strGrMethod, string strUdigit, string strTTL_WfrQty, string strMtlType)
        {
            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            strCust = strCust.Trim();
            strMultiLot = strMultiLot.Trim();
            strBank = strBank.Trim();
            strBcrType = strBcrType.Trim();
            strBcrCnt = strBcrCnt.Trim();
            strBcrName = strBcrName.Trim();
            strDeviceIndex = strDeviceIndex.Trim();
            strLotindex = strLotindex.Trim();
            strLotDigit = strLotDigit.Trim();
            strWfrQty = strWfrQty.Trim();
            strSpr = strSpr.Trim();
            strGrMethod = strGrMethod.Trim();
            strUdigit = strUdigit.Trim();
            strTTL_WfrQty = strTTL_WfrQty.Trim();
            strMtlType = strMtlType.Trim();

            // 지우고 추가
            List<string> queryList = new List<string>();

            Host_Delete_BCRFormat(strCust, strBcrName);

            string query = string.Format(@"INSERT INTO TB_CUST_INFO (DATETIME,CUST,MULTI_LOT,BANK_NO,BCR_TYPE,BCR_CNT,NAME,DEVICE,LOTID,LOT_DIGIT,WFR_QTY,SPR,GR_METHOD,UDIGIT,TTL_WFR_QTY,MTL_TYPE) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", 
                strSendtime, strCust, strMultiLot, strBank, strBcrType, strBcrCnt, strBcrName, strDeviceIndex, strLotindex, strLotDigit, strWfrQty, strSpr, strGrMethod, strUdigit, strTTL_WfrQty, strMtlType);
            queryList.Add(query);

            MSSql.SetData(queryList);
        }

        public int Host_Delete_BCRFormat(string strCust, string strBcrName)
        {
            string query = string.Format("DELETE FROM TB_CUST_INFO WHERE CUST='{0}' and NAME='{1}'", strCust, strBcrName);

            int n = MSSql.SetData(query);

            return n;
        }        

        public DataTable Host_GetUserDB(string strID)
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_USER_INFO WHERE ID='{0}'", strID);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }
        public DataTable Host_GetAllUser()
        {
            string query = "";
            query = string.Format(@"SELECT * FROM TB_USER_INFO");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public void Host_SetUserDB(string strID, string strName, string strGrade)
        {
            strID = strID.Trim();
            strName = strName.Trim();
            strGrade = strGrade.Trim();

            // 지우고 추가
            List<string> queryList = new List<string>();
            //queryList.Add(SQL_DelUserDB(strID));

            Host_DelUserDB(strID);

            string query = string.Format(@"INSERT INTO TB_USER_INFO (ID,NAME,GRADE) VALUES ('{0}','{1}','{2}')", strID, strName, strGrade);
            queryList.Add(query);

            MSSql.SetData(queryList);
        }

        public void Host_SetUserMESinfo(string strID, string strMESid, string strMESpassword)
        {
            strID = strID.Trim();
            strMESid = strMESid.Trim();
            strMESpassword = strMESpassword.Trim();

            string query = string.Format(@"UPDATE TB_USER_INFO SET MES_ID='{0}',MES_PASSWORD='{1}' WHERE ID='{2}'", strMESid, strMESpassword, strID);            

            MSSql.SetData(query);
        }

        public int Host_DelUserDB(string strID)
        {
            string query = string.Format("DELETE FROM TB_USER_INFO WHERE ID='{0}'", strID);

            int n = MSSql.SetData(query);

            return n;
        }

        public string Host_Get_Ready(string strEqid)
        {
            string strReturn;

            string query = "";

            query = string.Format(@"SELECT * FROM TB_HOST_STATE WHERE EQID='{0}'",strEqid);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strState = dt.Rows[0]["STATE"].ToString();
            strState = strState.Trim();

            string strMode = dt.Rows[0]["MODE"].ToString();
            strMode = strMode.Trim();

            strReturn = string.Format("{0}|{1}", strState, strMode);

            return strReturn;
        }

        public string Host_Set_Ready(string strEqid, string strState, string strMode)
        {
            string query1 = "";
            List<string> queryList1 = new List<string>();

            string strJudge = Host_Delete_Ready(strEqid);

            //if(strJudge != "OK")
            //{
            //    return "NG";
            //}

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_HOST_STATE (DATETIME,EQID,STATE,MODE) VALUES ('{0}','{1}','{2}','{3}')",
                strSendtime, strEqid, strState, strMode);

            queryList1.Add(query1);

            int nJudge = MSSql.SetData(queryList1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Delete_Ready(string strEqid)
        {
            string query = "";            

            query = string.Format("DELETE FROM TB_HOST_STATE WHERE EQID='{0}'",strEqid);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public void Host_Set_Jobname(string strEqid, string strJob)
        {
            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            strJob = strJob.Trim();

            // 지우고 추가
            List<string> queryList = new List<string>();

            Host_Delete_JobName(strEqid);

            string query = string.Format(@"INSERT INTO TB_JOB_INFO (DATETIME,EQID,JOB_NAME) VALUES ('{0}','{1}','{2}')", strSendtime, strEqid, strJob);
            queryList.Add(query);

            MSSql.SetData(queryList);
        }

        public string Host_Delete_JobName(string strEqid)
        {
            string query = "";

            query = string.Format("DELETE FROM TB_JOB_INFO WHERE EQID='{0}'",strEqid);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Get_JobName(string strEqid)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_JOB_INFO WHERE EQID='{0}'",strEqid);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strToday = string.Format("{0}{1:00}{2:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string strDate = dt.Rows[0]["DATETIME"].ToString(); strDate = strDate.Trim();
            strDate = strDate.Substring(0, 8);

            if (strToday != strDate)
                return "";

            string strJobName = dt.Rows[0]["JOB_NAME"].ToString();
            strJobName = strJobName.Trim();

            return strJobName;
        }

        public string Host_Set_Workinfo(string strEqid, string strFilename, string strBill, string strInvoice, string strState)
        {
            if (strBill == "" && strInvoice == "")
                return "OK";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            strEqid = strEqid.Trim();
            strBill = strBill.Trim();
            strFilename = strFilename.Trim();
            strInvoice = strInvoice.Trim();
            strState = strState.Trim();

            if(strBill != "")
                Host_Delete_Workinfo(strEqid, strBill, 0);
            else
                Host_Delete_Workinfo(strEqid, strInvoice, 1);

            string query = string.Format(@"INSERT INTO TB_WORK_INFO (DATETIME,EQID,HAWB,INVOICE,STATE,JOB_NAME) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", 
                strSendtime, strEqid, strBill, strInvoice, strState, strFilename);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }
        public string Host_Delete_Workinfo(string strEqid, string strData, int nType)
        {
            string query = "";

            if(nType == 0)
                query = string.Format("DELETE FROM TB_WORK_INFO WHERE EQID='{0}' and HAWB='{1}'", strEqid, strData);
            else
                query = string.Format("DELETE FROM TB_WORK_INFO WHERE EQID='{0}' and INVOICE='{1}'", strEqid, strData);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Get_WorkState(string strEqid, string strData, int nType)
        {
            string query = "";

            if(nType == 0)
                query = string.Format(@"SELECT * FROM TB_WORK_INFO WHERE EQID='{0}' and HAWB='{1}'", strEqid, strData);
            else
                query = string.Format(@"SELECT * FROM TB_WORK_INFO WHERE EQID='{0}' and INVOICE='{1}'", strEqid, strData);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strState = dt.Rows[0]["STATE"].ToString();
            strState = strState.Trim();

            return strState;
        }
        public string Host_Get_JobfileName(string strEqid, string strBill)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_WORK_INFO WHERE EQID='{0}' and HAWB='{1}'", strEqid, strBill);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strName = dt.Rows[0]["JOB_NAME"].ToString();
            strName = strName.Trim();

            return strName;
        }
        public DataTable Host_Get_Workinfo(string strEqid)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_WORK_INFO WHERE EQID='{0}'", strEqid);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public DataTable Host_Get_Workinfo_All()
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_WORK_INFO");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public string Host_Get_Bcr_Read_Result(string strEqid)
        {
            string strReturn;

            string query = "";

            query = string.Format(@"SELECT * FROM TB_BCR_INFO WHERE EQID='{0}'",strEqid);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strState = dt.Rows[0]["STATE"].ToString(); //0: Not working, 1: Start, 2: NG, 3: Complete, 4: Manual
            strState = strState.Trim();
            
            if (strState == "NOT_WORKING")
                return "";

            else if(strState == "NG" || strState == "2")
            {
                string strResult = dt.Rows[0]["RESULT"].ToString();
                strResult = strResult.Trim();

                if (strResult == "3")
                    return "FAIL";
                else if (strResult == "2")
                    return "NG|2";
                else if (strResult == "1")
                    return "NG|1";
                else
                    return "FAIL";
            }
            else if(strState == "COMPLETE" || strState == "3")
            {
                string strResult = dt.Rows[0]["RESULT"].ToString();
                strResult = strResult.Trim();

                string strwfrqty = dt.Rows[0]["WFR_QTY"].ToString();
                strwfrqty = strwfrqty.Trim();               

                string strlotinfo = dt.Rows[0]["LOT_INFO"].ToString();
                strlotinfo = strlotinfo.Trim();

                string strDeviceinfo = dt.Rows[0]["DEVICE_INFO"].ToString();
                strDeviceinfo = strDeviceinfo.Trim();

                strReturn = string.Format("{0}|{1}|{2}|{3}", strResult, strwfrqty, strlotinfo, strDeviceinfo);

                return strReturn;
            }

            return strState;
        }

        public string Host_Set_Bcr_Read_Start(string strEqid, int nMode)
        {
            string query1 = "";
            List<string> queryList1 = new List<string>();

            //queryList1.Add(Host_Delete_Bcrinfo());

            Host_Delete_Bcrinfo(strEqid);

            string strState = "0";
            if (nMode == 0) //Auto scan
                strState = "1";
            else
                strState = "4";


            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        
            query1 = string.Format(@"INSERT INTO TB_BCR_INFO (DATETIME,EQID,STATE,RESULT,WFR_QTY,LOT_INFO,DEVICE_INFO) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                strSendtime, strEqid, strState, "","","","");

            queryList1.Add(query1);

            int nJudge = MSSql.SetData(queryList1); 

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Delete_Bcrinfo(string strEqid)
        {
            string query = "";

            query = string.Format("DELETE FROM TB_BCR_INFO WHERE EQID='{0}'",strEqid);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Set_Bcr_Data(string strEqid, string[] strData)
        {
            if (strData.Length < 2)
                return "NG";

            string query1 = "";
            List<string> queryList1 = new List<string>();

            string strJudge = Host_Delete_Bcrinfo(strEqid);

            //if (strJudge != "OK")
            //    return "NG";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_BCR_INFO (DATETIME,EQID,STATE,RESULT,WFR_QTY,LOT_INFO,DEVICE_INFO) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                strSendtime, strEqid, strData[0], strData[1], strData[2], strData[3], strData[4]);

            queryList1.Add(query1);

            int nJudge = MSSql.SetData(queryList1); 

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Delete_BcrReadinfo(string strEqid, string strLot, int nType)
        {
            string query = "";

            if(nType == 0)
                query = string.Format("DELETE FROM TB_BCR_READINFO WHERE EQID='{0}' and LOT='{1}'", strEqid, strLot);
            else
                query = string.Format("DELETE FROM TB_BCR_READINFO WHERE EQID='{0}' and DEVICE='{1}'", strEqid, strLot);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Delete_BcrReadinfoAll(string strEqid)
        {
            try
            {
                string query = "";

                query = string.Format("DELETE FROM TB_BCR_READINFO WHERE EQID='{0}'", strEqid);

                int nJudge = MSSql.SetData(query);

                if (nJudge == 0)
                    return "NG";
                else
                    return "OK";
            }
            catch
            {
                return "NG";
            }
        }

        public string Host_Set_BcrReadInfo(string strEqid, string strDevice, string strLot, string strWaferID)
        {
            string query1 = "";

            string strJudge = Host_Get_BcrReadInfo(strEqid,strLot, strWaferID);

            if (strJudge == "True")
                return strJudge;

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_BCR_READINFO (DATETIME,EQID,DEVICE,LOT,WAFER_ID) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                strSendtime, strEqid, strDevice, strLot, strWaferID);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }
        public string Host_Get_BcrReadInfo(string strEqid, string strLot, string strWaferID)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_BCR_READINFO WHERE EQID='{0}'",strEqid);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            bool bDupicate = false;
            for(int n = 0; n< dt.Rows.Count; n++)
            {
                string strID = dt.Rows[n]["WAFER_ID"].ToString();
                string strGetLOT = dt.Rows[n]["LOT"].ToString();
                strID = strID.Trim();
                strGetLOT = strGetLOT.Trim();

                if (strID == strWaferID && strGetLOT == strLot)
                {
                    bDupicate = true;
                    n = dt.Rows.Count;
                }
            }

            return bDupicate.ToString();
        }

        public int Host_Get_BcrRead_Wfrcount(string strEqid, string strLot)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_BCR_READINFO WHERE EQID='{0}' and LOT='{1}'", strEqid, strLot);

            DataTable dt = MSSql.GetData(query);

            return dt.Rows.Count;
        }


        public string Host_Get_Print_State(string strEqid)
        {
            string strReturn;

            string query = "";

            query = string.Format(@"SELECT * FROM TB_PRINT_INFO WHERE EQID='{0}'",strEqid);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "";

            string strState = dt.Rows[0]["STATE"].ToString(); //0: No info, 1: Ready, 2: Working, 3: Complete, 4: 재출력
            strState = strState.Trim();

            if (strState == "0")
                return "";

            string strlotinfo = dt.Rows[0]["LOT_INFO"].ToString();
            strlotinfo = strlotinfo.Trim();

            if(strState == "1" || strState == "2" || strState == "4")
            {
                strReturn = string.Format("{0}", strState);
            }
            else
                strReturn = string.Format("{0}|{1}", strState, strlotinfo);

            return strReturn;
        }

        public string Host_Delete_PrintInfo(string strEqid)
        {
            string query = "";

            query = string.Format("DELETE FROM TB_PRINT_INFO WHERE EQID='{0}'",strEqid);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Set_Print_Data(string strEqid, string[] strData)
        {
            if (strData.Length < 2)
                return "NG";

            string query1 = "";
            List<string> queryList1 = new List<string>();

            string strJudge = Host_Delete_PrintInfo(strEqid);

            //if (strJudge == "NG")
            //    return "NG";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_PRINT_INFO (DATETIME,EQID,STATE,LOT_INFO) VALUES ('{0}','{1}','{2}','{3}')",
                strSendtime, strEqid, strData[0], strData[1]);

            queryList1.Add(query1);

            int nJudge = MSSql.SetData(queryList1); 

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Set_Print_Retry(string strEqid)
        {
            string query1 = "";
            List<string> queryList1 = new List<string>();

            string strJudge = Host_Delete_PrintInfo(strEqid);

            //if (strJudge == "NG")
            //    return "NG";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_PRINT_INFO (DATETIME,EQID,STATE,LOT_INFO) VALUES ('{0}','{1}','{2}','{3}')",
                strSendtime, strEqid, "4", "");

            queryList1.Add(query1);

            int nJudge = MSSql.SetData(queryList1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Set_Print_Detach(string strEqid, string strlot)
        {
            string query = "";

            query = string.Format("DELETE FROM TB_PRINT_INFO WHERE EQID='{0}' and LOT_INFO='{1}'", strEqid, strlot);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            string[] printinfo = { "", "" };

            printinfo[0] = "1"; printinfo[1] = ""; //Ready

            string strJudge = Host_Set_Print_Data(strEqid, printinfo);

            return strJudge;
        }

        public string Host_Set_Unprinted_Device(string strDevice, string strCust)
        {
            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_UNPRINTED_DEVICE (DATETIME,DEVICE,CUST_CODE) VALUES ('{0}','{1}','{2}')",
                strSendtime, strDevice, strCust);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public int Host_Check_Unprinted_Device(string strDevice)
        {
            string query = "";

            query = string.Format("IF EXISTS (SELECT DEVICE FROM TB_UNPRINTED_DEVICE WHERE DEVICE='{0}') BEGIN SELECT 1 CNT END ELSE BEGIN SELECT 0 CNT END", strDevice);
            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count == 0)
            {
                return 0;
            }

            if (dt.Rows[0]["CNT"].ToString() == "1")
                return 1;
            else
                return 0;
        }

        public DataTable Host_Get_Unprinted_Device()
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_UNPRINTED_DEVICE");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public string Host_Delete_Unprinted_Device(string strDevice)
        {
            string query = "";

            query = string.Format("DELETE FROM TB_UNPRINTED_DEVICE WHERE DEVICE='{0}'", strDevice);

            int nJudge = MSSql.SetData(query);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Hist_Job(string[] strinfo)
        {
            if (strinfo.Length != 10)
                return "NG";

            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_HIST_JOB (DATETIME,LOCATION,WORK_TYPE,HAWB,DEVICE,LOT,DIE_QTY,DIE_TTL,WFR_QTY,WFR_TTL,OP_NAME) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                strSendtime, strinfo[0], strinfo[1], strinfo[2], strinfo[3], strinfo[4], strinfo[5], strinfo[6], strinfo[7], strinfo[8], strinfo[9]);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Hist_Unprint(string[] strinfo)
        {
            if (strinfo.Length != 5)
                return "NG";

            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_HIST_UNPRINT (DATETIME,LOCATION,WORK_TYPE,DEVICE,CUST_CODE,OP_NAME) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                strSendtime, strinfo[0], strinfo[1], strinfo[2], strinfo[3], strinfo[4]);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public DataTable Host_Get_Histinfo_Job(string strEqid, double dStart, double dEnd)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_HIST_JOB WHERE LOCATION='{0}' and DATETIME>'{1}' and DATETIME<='{2}' ", strEqid, dStart, dEnd);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public DataTable Host_Get_Histinfo_Job_Bill(string strBill)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_HIST_JOB WHERE HAWB='{0}'", strBill);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public DataTable Host_Get_Histinfo_Job_Device(string strDevice)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_HIST_JOB WHERE DEVICE='{0}'", strDevice);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public DataTable Host_Get_Histinfo_Unprint(string strEqid, double dStart, double dEnd)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_HIST_UNPRINT WHERE LOCATION='{0}' and DATETIME>'{1}' and DATETIME<='{2}' ", strEqid, dStart, dEnd);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public async Task<string> Fnc_UserInformation(string strUserSerial) //사번
        {
            string strurl = "";

            strurl = string.Format("http://10.101.14.181:8080/SmartConsoleWebService/user_info/get_user/{0},1", strUserSerial);

            string res_ = await Fnc_RunAsync(strurl);

            Fnc_WebServiceLog(strurl, res_);

            return res_;
        }

        public async Task<string> Fnc_eMES_Login(string strMesID, string strMesPw, string strUserSerial) //
        {
            string strurl = "";

            strurl = string.Format("http://10.101.14.181:8080/SmartConsoleWebService/user_info/doLoginEncrypt?ID={0}&PW={1}=&Badge={2}", 
                strMesID, Base64Encode(strMesPw), strUserSerial);

            string res_ = await Fnc_RunAsync(strurl);

            Fnc_WebServiceLog(strurl, res_);

            return res_;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public async Task<string> Fnc_GetLotInformation(string site) //가전산 잡은 리스트 다운로드
        {
            string strurl = "";

            //strurl = string.Format("http://10.101.14.181:8080/SmartConsoleWebService/lot_info/getAutoGRLotListOnReadyStatus_eMES/K4");
            //strurl = string.Format("http://10.101.14.130:8180/eMES_Webservice/lot_info_list/getAutoGRLotListOnReadyStatus_eMES/K4");
            strurl = string.Format("http://10.101.14.130:8180/eMES_Webservice/lot_info_list/getAutoGRLotListOnReadyStatus_eMES/{0}", site); // 20211102 iyjeong
            string res_ = await Fnc_RunAsync(strurl);

            Fnc_WebServiceLog(strurl, res_);

            return res_;
        }

        public async Task<string> Fnc_AutoGR(string stGRinfo) //{0};{1};{2}, st.Amkorid, st.Die_Qty, st.Rcv_WQty
        {
            string strurl = "";

            //Fnc_WebServiceLog("Start", "");
           
            //strurl = string.Format("http://10.101.2.27:9080/eMES/diebank/lotEntryMultiLotCreateAll.do?serviceRequestor=AutoGR&GR_INFO={0}", stGRinfo);
            strurl = string.Format("http://aak1ws01/eMES/diebank/lotEntryMultiLotCreateAll.do?serviceRequestor=AutoGR&GR_INFO={0}", stGRinfo);

            Fnc_WebServiceLog(strurl, "Start");

            var res_ = await Fnc_RunAsync(strurl);

            Fnc_WebServiceLog(strurl, res_);

            return res_;
        }

        public async Task<string> Fnc_GetBillInformation(string site, string strBill)
        {
            string strurl = "";

            //strurl = string.Format("http://10.101.14.181:8080/SmartConsoleWebService/lot_list/AutoGRLotList/K4,{0}", strBill);
            strurl = string.Format("http://10.101.14.181:8080/SmartConsoleWebService/lot_list/AutoGRLotList/{0},{1}", site, strBill);   // 20211102 iyjeong

            var res_ = await Fnc_RunAsync(strurl);

            Fnc_WebServiceLog(strurl, res_);

            return res_;
        }


        public void Fnc_WebServiceLog(string strMessage, string strResult)
        {
            string strPath = "C:\\LOG\\";
            string strToday = string.Format("{0}{1:00}{2:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string strHead = string.Format(",{0:00}:{1:00}:{2:00}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            strPath = strPath + strToday + "WebService.txt";
            strHead = strToday + strHead;

            string strSave;
            strSave = strHead + ',' + strMessage + ','+ '\n' + strResult;
            Fnc_WriteFile(strPath, strSave);
        }

        public void Fnc_WriteFile(string strFileName, string strLine)
        {
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(strFileName, true))
            {
                file.WriteLine(strLine);
            }
        }
        public async Task<string> Fnc_RunAsync(string strKey)
        {
            string str = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/HY"));
                
                HttpResponseMessage response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    str = contents;
                }
            }

            return str;
        }

        public string Host_Get_Lot_State(string strEqid, string strLot, string strDevice)
        {
            string strJobName = Host_Get_JobName(strEqid);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Environment.CurrentDirectory);
            string strExcutionPath = di.ToString();

            string strFileName = strExcutionPath + "\\Work\\" + strJobName + "\\";
            string strReadfile = strFileName + "\\" + strDevice + "\\" + strDevice + ".txt";
            string strStatus = Fnc_Get_LotStatus(strReadfile, strLot);

            if (strStatus != "")
                return strStatus;

            return strReadfile;
        }

        public string Fnc_Get_LotStatus(string strFileName, string strLot)
        {
            string[] info = Fnc_ReadFile(strFileName);

            if (info == null)
                return "";

            for (int m = 0; m < info.Length; m++)
            {
                string[] strSplit_data = info[m].Split('\t');

                if (strLot == strSplit_data[2])
                {
                    return strSplit_data[15];
                }
            }

            return "";
        }
        public string Host_Set_TempConfig(string strLoc, string strCustname, string strCustcode, string strSensorcount, string strShelfname)
        {
            string query1 = "";

            if(Host_Check_TempConfig(strShelfname) != "OK")
            {
                return "DUPLICATE";
            }

            query1 = string.Format(@"INSERT INTO TB_TEMPCONFIG (LOCATION,CUST_NAME,CUST_CODE,SENSOR_QTY,SHELF_NAME) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                strLoc, strCustname, strCustcode, strSensorcount, strShelfname);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Check_TempConfig(string strShelfname)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_TEMPCONFIG WHERE SHELF_NAME='{0}' ", strShelfname);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "OK";

            return "NG";
        }

        public DataTable Host_Get_TempConfig()
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_TEMPCONFIG");

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public DataTable Host_Get_TempConfig(string strShelfname)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_TEMPCONFIG WHERE SHELF_NAME='{0}'", strShelfname);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public string Host_Set_TempSpec(string strShelfname, string strCh, string strTempLSL, string strTempUSL, string strHumidLSL, string strHumidUSL)
        {
            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_TEMPSPEC (DATETIME,SHELF_NAME,CHANNEL,TEMP_LSL,TEMP_USL,HUMID_LSL,HUMID_USL) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                strSendtime, strShelfname, strCh, strTempLSL, strTempUSL, strHumidLSL, strHumidUSL);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public int Host_Delete_TempSpec(string strShelfname)
        {
            string query = string.Format("DELETE FROM TB_TEMPSPEC WHERE SHELF_NAME='{0}'", strShelfname);

            int n = MSSql.SetData(query);

            return n;
        }

        public string Host_Get_TempSpec(string strShelf, string strCh)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_TEMPSPEC WHERE SHELF_NAME='{0}' and CHANNEL='{1}'", strShelf, strCh);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "NO_INFO";

            string strDatetime = "", strGetShelf = "", strGetch = "", strTemp_LSL = "", strTemp_USL = "", strHumid_LSL = "", strHumid_USL = "";
            bool bcheck = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strDatetime = dt.Rows[i]["DATETIME"].ToString();strDatetime = strDatetime.Trim();
                strGetShelf = dt.Rows[i]["SHELF_NAME"].ToString();strGetShelf = strGetShelf.Trim();
                strGetch = dt.Rows[i]["CHANNEL"].ToString(); strGetch = strGetch.Trim();
                strTemp_LSL = dt.Rows[i]["TEMP_LSL"].ToString();strTemp_LSL = strTemp_LSL.Trim();
                strTemp_USL = dt.Rows[i]["TEMP_USL"].ToString();strTemp_USL = strTemp_USL.Trim();
                strHumid_LSL = dt.Rows[i]["HUMID_LSL"].ToString();strHumid_LSL = strHumid_LSL.Trim();
                strHumid_USL = dt.Rows[i]["HUMID_USL"].ToString();strHumid_USL = strHumid_USL.Trim();

                if (strShelf == strGetShelf && strCh == strGetch)
                {
                    i = dt.Rows.Count;
                    bcheck = true;
                }
            }

            if (!bcheck)
                return "NO_INFO";

            string strDate = string.Format("{0};{1};{2};{3}", strTemp_LSL, strTemp_USL, strHumid_LSL, strHumid_USL);

            return strDate;
        }

        public string Host_Set_TempCheck(string strShift, string strSID, string strName, string strResult)
        {
            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_TEMPCHECK (DATETIME,SHIFT,SID,NAME,RESULT) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                strSendtime, strShift, strSID, strName, strResult);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string Host_Set_ForcedCheck(string strSendtime, string strShift, string strSID, string strName, string strResult)
        {
            string query1 = "";
            
            query1 = string.Format(@"INSERT INTO TB_TEMPCHECK (DATETIME,SHIFT,SID,NAME,RESULT) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                strSendtime, strShift, strSID, strName, strResult);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public DataTable Host_Get_TempCheck(double dStart, double dEnd)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM TB_TEMPCHECK WHERE DATETIME>'{0}' and DATETIME<='{1}' ", dStart, dEnd);

            DataTable dt = MSSql.GetData(query);

            return dt;
        }

        public string Host_Get_TempCheck_Name(string strShift)
        {
            string query = "";

            query = string.Format(@"SELECT * FROM dbo.TB_TEMPCHECK where DATETIME=(select max(DATETIME) from dbo.TB_TEMPCHECK where SHIFT = '{0}')", strShift);

            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count < 1)
                return "NO_INFO";

            string strDatetime = "", strGetShift = "", strName = "", strResult = "";
            for (int i = 0; i<dt.Rows.Count; i++)
            {
                strDatetime = dt.Rows[i]["DATETIME"].ToString();
                strGetShift = dt.Rows[i]["SHIFT"].ToString();
                strName = dt.Rows[i]["NAME"].ToString();
                strResult = dt.Rows[i]["RESULT"].ToString();
                strDatetime = strDatetime.Trim();
                strGetShift = strGetShift.Trim();
                strName = strName.Trim();
                strResult = strResult.Trim();

                if(strShift == strGetShift)
                {
                    i = dt.Rows.Count;
                }
            }

            string strDate = string.Format("{0}-{1}", strDatetime.Substring(4, 2), strDatetime.Substring(6, 2));
            string strTime = strDatetime.Substring(8, 6);
            strTime = strTime.Substring(0, 2) + ":" + strTime.Substring(2, 2) + ":" + strTime.Substring(4, 2);

            strName = string.Format("{0} {1}\n({2},{3})", strName, strResult, strDate, strTime);

            if (strName.Contains("NO_INFO"))
                strName = "NO_INFO";

            return strName;
        }
        public string CheckDeviceRename(string device)
        {
            string query = "";

            query = string.Format("IF EXISTS (SELECT DEVICE_ORG FROM TB_DEVICE_RENAME WHERE DEVICE_ORG='{0}') BEGIN SELECT 99 CNT END ELSE BEGIN SELECT 55 CNT END", device);
            DataTable dt = MSSql.GetData(query);

            if (dt.Rows.Count == 0)
            {
                return "ERROR";
            }

            if (dt.Rows[0]["CNT"].ToString() == "99")
                return "EXIST";
            else
                return "NO_DATA";
        }
        public string Get_Device_Rename(string device)
        {
            string query = "";

            query = string.Format(@"SELECT DEVICE_RENAME FROM TB_DEVICE_RENAME WHERE DEVICE_ORG='{0}'", device);

            DataTable dt = MSSql.GetData(query);

            int nCount = dt.Rows.Count;

            if (nCount == 0)
            {
                return "NO_DATA";
            }

            string strRename = dt.Rows[0]["DEVICE_RENAME"].ToString();

            return strRename;
        }
        public string Set_Device_Rename(string device_org, string device_rename, string supplier)
        {
            string query1 = "";

            string strSendtime = string.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            query1 = string.Format(@"INSERT INTO TB_DEVICE_RENAME (DATETIME,DEVICE_ORG,DEVICE_RENAME,SUPPLIER) VALUES ('{0}','{1}','{2}','{3}')",
                    strSendtime, device_org, device_rename, supplier);

            int nJudge = MSSql.SetData(query1);

            if (nJudge == 0)
                return "NG";

            return "OK";
        }

        public string[] Fnc_ReadFile(string strPath)
        {
            if (!System.IO.File.Exists(strPath))
            {
                return null;
            }
            else
            {
                string[] lines = System.IO.File.ReadAllLines(strPath);
                int nLength = lines.Length;

                if (nLength != 0)
                    return lines;
                else
                    return null;
            }
        }
    }
}
