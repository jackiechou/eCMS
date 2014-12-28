using Eagle.Common.Parse;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Host;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eagle.Model.Mail;

namespace Eagle.Repository.Mail
{
    public class MailTemplateRespository
    {
        public static bool IsIDExists(int ID)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = dbContext.Mail_Templates.FirstOrDefault(c => c.Mail_Template_Id.Equals(ID));
                return (query != null);
            }
        }

        public static MailTemplateViewModel GetDetails(int Mail_Template_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                
                var entity = (from x in dbContext.Mail_Templates
                            join y in dbContext.Mail_Types on x.Mail_Type_Id equals y.TypeId
                            where x.Mail_Template_Id == Mail_Template_Id
                            select new MailTemplateViewModel { 
                                    Mail_Template_Id = x.Mail_Template_Id,
                                    Mail_Template_Name = x.Mail_Template_Name,
                                    Mail_Template_Content = x.Mail_Template_Content,
                                    Mail_Template_Discontinued = x.Mail_Template_Discontinued,
                                    Mail_Type_Id = x.Mail_Type_Id,
                                    Mail_Type_Name = y.Name,
                            }).FirstOrDefault();

                entity.Mail_Template_Content = HttpUtility.HtmlDecode(entity.Mail_Template_Content);
                return entity;
            }
        }

        public static List<Mail_Templates> GetList()
        {
            using (EntityDataContext dbContext = new EntityDataContext()){                
                var query = from x in dbContext.Mail_Templates
                            select x;
                return query.ToList();
            }
        }

        public static string GetTemplateContent(int Mail_Template_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                string Mail_Template_Content = (from x in dbContext.Mail_Templates
                                                where x.Mail_Template_Id == Mail_Template_Id
                            select x.Mail_Template_Content).FirstOrDefault();
                return Mail_Template_Content;
            }
        }

        public static List<MailTemplateViewModel> GetListByTypeId(int? TypeId)
        {
            List<MailTemplateViewModel> lst = new List<MailTemplateViewModel>();
            
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Templates
                            join y in dbContext.Mail_Types on x.Mail_Type_Id equals y.TypeId                               
                            select new MailTemplateViewModel
                            {
                                Mail_Template_Id = x.Mail_Template_Id,
                                Mail_Template_Name = x.Mail_Template_Name,
                                Mail_Template_Content = x.Mail_Template_Content,
                                Mail_Template_Discontinued = x.Mail_Template_Discontinued,
                                Mail_Type_Id = x.Mail_Type_Id,
                                Mail_Type_Name = y.Name,
                            };

                if (TypeId != null && TypeId > 0)                   
                    query = query.Where(x => x.Mail_Type_Id == TypeId);
                lst = query.ToList();
            }
            return lst;
        }

        ////INSERT- UPDATE - DELETE 
        public static bool Insert(MailTemplateViewModel model, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        bool isDuplicate = IsIDExists(model.Mail_Template_Id);
                        if (isDuplicate == false)
                        {
                            Mail_Templates entity = new Mail_Templates();
                            entity.Mail_Type_Id = model.Mail_Type_Id;
                            entity.Mail_Template_Name = model.Mail_Template_Name;
                            entity.Mail_Template_Content = HttpContext.Current.Server.HtmlEncode(model.Mail_Template_Content);
                            entity.Mail_Template_Discontinued = model.Mail_Template_Discontinued;


                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                            int affectedRow = dbContext.SaveChanges();
                            if (affectedRow == 1)
                            {
                                entity.Mail_Template_Id = model.Mail_Template_Id;
                                Message = Eagle.Resource.LanguageResource.CreateSuccess;
                                result = true;
                            }
                            transcope.Complete();
                        } else
                        {
                            result = false;
                            Message = Eagle.Resource.LanguageResource.DuplicateError;
                        }
                    }catch (Exception ex)
                    {
                        ex.ToString();
                        result = false;
                        Message = Eagle.Resource.LanguageResource.SystemError;
                    }                    
                }
                return result;
            }
        }
        public static bool Update(MailTemplateViewModel model, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Mail_Templates entity = dbContext.Mail_Templates.Single(x => x.Mail_Template_Id == model.Mail_Template_Id);
                        entity.Mail_Type_Id = model.Mail_Type_Id;
                        entity.Mail_Template_Name = model.Mail_Template_Name;
                        entity.Mail_Template_Content = HttpContext.Current.Server.HtmlEncode(model.Mail_Template_Content);
                        entity.Mail_Template_Discontinued = model.Mail_Template_Discontinued;
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = dbContext.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                        transcope.Complete();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        result = false;
                        Message = Eagle.Resource.LanguageResource.SystemError;
                    }
                }
            }
            return result;
        }

        public static bool Delete(int Mail_Template_Id)
        {
            bool flag = false;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Mail_Templates entity = dbContext.Mail_Templates.Single(x => x.Mail_Template_Id == Mail_Template_Id);
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    int i = dbContext.SaveChanges();
                    if (i > 0)
                        flag = true;
                    transcope.Complete();
                }
            }
            return flag;
        }


        #region PARSE PARAMETER AND OUTPUT CONTENT =====================================================
        //Hashtable templateVars = new Hashtable();
        //templateVars.Add("DAY", DAY);  
        public static string ParseTemplate(Hashtable TemplateVariables, string TemplateContent)
        {
            ParseHTMLContents parser = new ParseHTMLContents(TemplateContent, TemplateVariables);
            string body_content = parser.Parse();
            return body_content;
        }

        public static string ParseTemplateByTemplateId(Hashtable TemplateVariables, int Mail_Template_Id)
        {
            string HTMLContents = GetTemplateContent(Mail_Template_Id);
            ParseHTMLContents parser = new ParseHTMLContents(HTMLContents, TemplateVariables);
            string body_content = parser.Parse();
            return body_content;
        }

        public static bool SendGMailByTemplate(Hashtable TemplateVariables, int Mail_Template_Id, string MailTo, string Cc, string Bcc)
        {       
            MailTemplateViewModel mail_template_entity = MailTemplateRespository.GetDetails(Mail_Template_Id);
            string Subject = mail_template_entity.Mail_Template_Name;
            string Body = ParseTemplate(TemplateVariables, mail_template_entity.Mail_Template_Content);

            string SMTPServer = HostSettings.SMTPServer;
            string SMTPAuthentication = "1"; //Anonymous(0) - Basic(1) - NTLM(2)
            string MailFrom = HostSettings.SMTPMEmail;
            string SMTPUsername = HostSettings.SMTPUsername;
            string SMTPPassword = HostSettings.SMTPPassword;
            string Mail_Signature = HostSettings.Mail_Signature;
            bool SMTPEnableSSL = HostSettings.SMTPEnableSSL;
            string ReplyTo = string.Empty;
            List<System.Net.Mail.Attachment> Attachments = null;
            string result = MailRepository.SendMail(MailFrom, MailTo, Cc, Bcc, ReplyTo, System.Net.Mail.MailPriority.Normal, Subject, Eagle.Common.Services.Mail.MailFormat.Html,
             System.Text.Encoding.UTF8, Body, Attachments, SMTPServer, SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
            return result == "";
        }


        public static bool SendMailWithTLSByTemplate(Hashtable TemplateVariables, int Mail_Template_Id, string MailTo, string Cc, string Bcc)
        {
            MailTemplateViewModel mail_template_entity = MailTemplateRespository.GetDetails(Mail_Template_Id);
            string Subject = mail_template_entity.Mail_Template_Name;
            string Body = ParseTemplate(TemplateVariables, mail_template_entity.Mail_Template_Content);

            string SMTPServer = HostSettings.SMTPServer;
            string SMTPAuthentication = "1"; //Anonymous(0) - Basic(1) - NTLM(2)
            string MailFrom = HostSettings.SMTPMEmail;
            string SMTPUsername = HostSettings.SMTPUsername;
            string SMTPPassword = HostSettings.SMTPPassword;
            string Mail_Signature = HostSettings.Mail_Signature;
            bool SMTPEnableSSL = HostSettings.SMTPEnableSSL;
            string ReplyTo = string.Empty;
            List<System.Net.Mail.Attachment> Attachments = null;
            string result = MailRepository.SendMailWithTLS(MailFrom, MailTo, Cc, Bcc, ReplyTo, System.Net.Mail.MailPriority.Normal, Subject, Eagle.Common.Services.Mail.MailFormat.Html,
             System.Text.Encoding.UTF8, Body, Attachments, SMTPServer, SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
            return result == "";
        }
        #endregion ==================================================================================================================================================
    }
}
