#region "References"

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using Eagle.Common.Data;

#endregion

namespace Eagle.Repository.Services.Logs
{
    public class ErrorLogRepository
    {
        public void ClearLog(Guid ApplicationCode)
        {          
            try
            {
                string sp = "[Cms].[Logs_ClearByApplicationCode]";
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationCode", ApplicationCode));

                SQLHandler sqlHandler = new SQLHandler();
                sqlHandler.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteLogByLogID(int LogId)
        {           
            int affectedRows = 0;
            try
            {
                string sp = "[Cms].[Logs_Delete]";
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@LogId", LogId));

                SQLHandler sqlHandler = new SQLHandler();
                affectedRows = sqlHandler.ExecuteNonQuery(sp, ParamCollInput, "o_return");                
            }
            catch (Exception)
            {
                throw;
            }
            return affectedRows;
        }
        public int InsertLog(Guid ApplicationCode, string LogLevel, string Thread, string Message, string Exception, string Logger)
        {
            int affectedRows = 0;
            try
            {
                string sp = "[Cms].[Logs_Insert]";
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationCode", ApplicationCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LogLevel", LogLevel));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Thread", Thread));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Message", Message));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Exception", Exception));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Logger", Logger));

                SQLHandler sqlHandler = new SQLHandler();
                affectedRows = sqlHandler.ExecuteNonQuery(sp, ParamCollInput, "@o_return");
            }
            catch (Exception)
            {
                throw;
            }
            return affectedRows;
        }

     
    }
    
}
