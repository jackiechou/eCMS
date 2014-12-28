using EntityModels;
using Repository.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using ViewModels.Mail;

namespace SendMailCalendar
{
    partial class SendMailService : ServiceBase
    {
        //Initialize the timer
        Timer timer = new Timer();
        private static string serviceName = "HRM_UNIT_Send Mail Calendar";
        //private static string serviceDescription = "BIS is service which is used to transfer data";
        private static string appPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        private static string executableServicePath = System.IO.Path.Combine(appPath, "SendMailCalendar.exe");
        private static string logFilePath = System.IO.Path.Combine(appPath, "SendMailLog" + DateTime.Now.ToString("MM-yyyy") + ".log");

        public SendMailService()
        {
            InitializeComponent();
            //System.Diagnostics.Debugger.Break();            

            this.ServiceName = serviceName;
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
            this.AutoLog = false; //if use custom service

            //add this line to text file during start of service
            //TraceService("Service starts ..." + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());

            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //15 phút chạy 1 lần
            //timer.Interval = 1000 * 60 * 15;
            //30 giây chạy 1 lần
            timer.Interval = 1000 * 30;
            
            timer.Enabled = true;

            //Write an informational entry to the event log.    
            //EventLogController.WriteEntryToEventViewLog(serviceName, "Writing to event log.");
        }

        //Làm khi mỗi lần timer lập lại
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                WriteLogFile(logFilePath, "Service work");
                // do any background work
                EntityDataContext context = new EntityDataContext();
                //Lấy những mail chưa gửi
                List<Mail_SendMailCalendar> mailCalendarList = (from mailcalendar in context.Mail_SendMailCalendar
                                                                where mailcalendar.SendTime.Value < DateTime.Now & mailcalendar.Actived == true & mailcalendar.isSent == false
                                                                select mailcalendar).ToList();
                
                foreach (var mail in mailCalendarList)
                {
                    #region //Tạo Hashtable để truyền vào mail template
                    Hashtable TemplateVariables = new Hashtable();
                    if (!string.IsNullOrEmpty(mail.Param))
                    {
                        
                        string[] arr = mail.Param.Split('|');

                        for (int i = 0; i < arr.Length; i = i + 2)
                        {
                            TemplateVariables.Add(arr[i], arr[i + 1]);
                        }

                    }
                    WriteLogFile(logFilePath, "Do line 89");
                   // MailTemplateViewModel mailTemplateModel = GetDetails(mail.MailTemplateID);
                    
                    WriteLogFile(logFilePath, "Do line 92");

                    #endregion
                    var ToEmp = context.HR_tblEmp.Where(p => p.EmpID == mail.ToEmpID).FirstOrDefault();
                    string CcEmail = "";
                    string BccEmail = "";

                    WriteLogFile(logFilePath, "Do line Get CC Email 99");

                    #region lấy Cc Email
                    if (!string.IsNullOrEmpty(mail.CcEmpID))
                    {
                        string[] CcEmpIDArray = mail.CcEmpID.Split(',');
                        List<int> CcEmpIDList = new List<int>();
                        foreach (var item in CcEmpIDArray)
                        {
                            try
                            {
                                int tmp = Convert.ToInt32(item);
                                CcEmpIDList.Add(tmp);
                            }
                            catch { }
                        }
                        var CcEmailList = context.HR_tblEmp.Where(p => CcEmpIDList.Contains(p.EmpID)).Select(p => p.Email).Distinct().ToList();
                        if (CcEmailList != null && CcEmailList.Count > 0)
                        {
                            foreach (var email in CcEmailList)
                            {
                                CcEmail += email + ",";
                            }
                        }
                    }

                    #endregion

                    WriteLogFile(logFilePath, "Do line Get BCC Email 124");

                    #region lấy Bcc Email
                    if (!string.IsNullOrEmpty(mail.BccEmpID))
                    {
                        if (!string.IsNullOrEmpty(CcEmail))
                        {
                            CcEmail = CcEmail.Substring(0, CcEmail.Length - 1);
                        }
                        if (!string.IsNullOrEmpty(BccEmail))
                        {
                            BccEmail = BccEmail.Substring(0, BccEmail.Length - 1);
                        }

                        string[] BccEmpIDArray = mail.BccEmpID.Split(',');
                        List<int> BccEmpIDList = new List<int>();
                        foreach (var item in BccEmpIDArray)
                        {
                            try
                            {
                                int tmp = Convert.ToInt32(item);
                                BccEmpIDList.Add(tmp);
                            }
                            catch { }
                        }
                        var BccEmailList = context.HR_tblEmp.Where(p => BccEmpIDList.Contains(p.EmpID)).Select(p => p.Email).Distinct().ToList();
                        if (BccEmailList != null && BccEmailList.Count > 0)
                        {
                            foreach (var email in BccEmailList)
                            {
                                BccEmail += email + ",";
                            }
                        }
                    }
                   

                    #endregion

                    WriteLogFile(logFilePath, "Do line 149");

                    if (ToEmp != null && !string.IsNullOrEmpty(ToEmp.Email))
                    {

                        if (!string.IsNullOrEmpty(CcEmail))
                        {
                            if (CcEmail.EndsWith(";") || CcEmail.EndsWith(","))
                            {
                                CcEmail = CcEmail.Substring(0, CcEmail.Length - 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(BccEmail))
                        {
                            if (BccEmail.EndsWith(",") || BccEmail.EndsWith(";"))
                            {
                                BccEmail = BccEmail.Substring(0, BccEmail.Length - 1);
                            }
                        }

                        bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, mail.MailTemplateID.Value, ToEmp.Email, CcEmail, BccEmail);

                        WriteLogFile(logFilePath, "Do line 155");
                        if (result == true)
                        {
                            mail.isSent = true;
                            context.Entry(mail).State = System.Data.EntityState.Modified;
                            context.SaveChanges();
                            WriteLogFile(logFilePath, "Send Mail Complete");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(logFilePath, "Service catch line 105 - " + ex.Message);
            }

        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            WriteLogFile(logFilePath, "Send mail success!");
        }

        public static void WriteLogFile(string filePath, string message)
        {
            if (System.IO.File.Exists(filePath))
            {
                if (!System.IO.File.Exists(filePath))
                    System.IO.File.Create(filePath);
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                fileStream.Flush();
                fileStream.Close();
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                string lastRecordText = "# " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " # " + Environment.NewLine + "#" + message + " #" + Environment.NewLine;
                sw.WriteLine(lastRecordText);
                sw.Close();
            }

        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            timer.Start();
            WriteLogFile(logFilePath, "Service Start");
        }

        protected override void OnStop()
        {
            System.Threading.Thread.Sleep(1000);
            timer.Stop();
            timer.Enabled = false;   
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            timer.Start();
            //EventLogController.LogEvent(serviceName, serviceName + "In OnContinue.", EventLogEntryType.Information);
        }

        private void TraceService(string content)
        {

        }

        #region Common
        public MailTemplateViewModel GetDetails(int? Mail_Template_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                try
                {

                    WriteLogFile(logFilePath, "Get Details: 1");
                    var entity = (from x in dbContext.Mail_Templates
                                  join y in dbContext.Mail_Types on x.Mail_Type_Id equals y.TypeId
                                  where x.Mail_Template_Id == Mail_Template_Id
                                  select new MailTemplateViewModel
                                  {
                                      Mail_Template_Id = x.Mail_Template_Id,
                                      Mail_Template_Name = x.Mail_Template_Name,
                                      Mail_Template_Content = x.Mail_Template_Content,
                                      Mail_Template_Discontinued = x.Mail_Template_Discontinued,
                                      Mail_Type_Id = x.Mail_Type_Id,
                                      Mail_Type_Name = y.Name,
                                  }).FirstOrDefault();

                    WriteLogFile(logFilePath, "Get Details: 2");
                    entity.Mail_Template_Content = HttpUtility.HtmlDecode(entity.Mail_Template_Content);
                    WriteLogFile(logFilePath, "Get Details: 3");
                    return entity;
                }
                catch (Exception ex)
                {
                    WriteLogFile(logFilePath, "Get Details: " + ex.Message);
                    return new MailTemplateViewModel();
                }
            }
        }
        #endregion
    }
}
