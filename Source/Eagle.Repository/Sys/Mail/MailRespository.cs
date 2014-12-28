using Eagle.Common.Services.Mail;
using Eagle.Common.Utilities;
using Eagle.Repository.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;


namespace Eagle.Repository.Mail
{
    public class MailRepository
    {
         
        public static string ConvertToText(string sHTML)
        {
            string sContent = sHTML;
            sContent = sContent.Replace("&nbsp;", " ");            
            sContent = sContent.Replace("<br />", Environment.NewLine);
            sContent = sContent.Replace("<br>", Environment.NewLine);
            sContent = HtmlUtils.FormatText(sContent, true);           
            sContent = HtmlUtils.StripTags(sContent, true);
            return sContent;
        }

        public static bool IsValidEmailAddress(string Email, int PortalId)
        {
            string pattern = null;
            //if (PortalId != null)
            //{
            //    pattern = Convert.ToString(UserController.GetUserSettings(PortalId)["Security_EmailValidation"]);
            //}
            string glbEmailRegEx = "\\b[a-zA-Z0-9._%\\-+']+@[a-zA-Z0-9.\\-]+\\.[a-zA-Z]{2,4}\\b";
            pattern = string.IsNullOrEmpty(pattern) ? glbEmailRegEx : pattern;
            return System.Text.RegularExpressions.Regex.Match(Email, pattern).Success;
        }

        ////public static string SendMail(UserInfo user, MessageType msgType, PortalSettings settings)
        ////{
        ////    int toUser = user.UserID;
        ////    string locale = user.Profile.PreferredLocale;
        ////    string subject = "";
        ////    string body = "";
        ////    ArrayList custom = null;
        ////    switch (msgType)
        ////    {
        ////        case MessageType.UserRegistrationAdmin:
        ////            subject = "EMAIL_USER_REGISTRATION_ADMINISTRATOR_SUBJECT";
        ////            body = "EMAIL_USER_REGISTRATION_ADMINISTRATOR_BODY";
        ////            toUser = settings.AdministratorId;
        ////            UserInfo admin = UserController.GetUserById(settings.PortalId, settings.AdministratorId);
        ////            locale = admin.Profile.PreferredLocale;
        ////            break;
        ////        case MessageType.UserRegistrationPrivate:
        ////            subject = "EMAIL_USER_REGISTRATION_PRIVATE_SUBJECT";
        ////            body = "EMAIL_USER_REGISTRATION_PRIVATE_BODY";
        ////            break;
        ////        case MessageType.UserRegistrationPublic:
        ////            subject = "EMAIL_USER_REGISTRATION_PUBLIC_SUBJECT";
        ////            body = "EMAIL_USER_REGISTRATION_PUBLIC_BODY";
        ////            break;
        ////        case MessageType.UserRegistrationVerified:
        ////            subject = "EMAIL_USER_REGISTRATION_VERIFIED_SUBJECT";
        ////            body = "EMAIL_USER_REGISTRATION_VERIFIED_BODY";
        ////            if (HttpContext.Current != null)
        ////            {
        ////                custom = new ArrayList();
        ////                custom.Add(HttpContext.Current.Server.UrlEncode(user.Username));
        ////            }
        ////            break;
        ////        case MessageType.PasswordReminder:
        ////            subject = "EMAIL_PASSWORD_REMINDER_SUBJECT";
        ////            body = "EMAIL_PASSWORD_REMINDER_BODY";
        ////            break;
        ////        case MessageType.ProfileUpdated:
        ////            subject = "EMAIL_PROFILE_UPDATED_SUBJECT";
        ////            body = "EMAIL_PROFILE_UPDATED_BODY";
        ////            break;
        ////        case MessageType.UserUpdatedOwnPassword:
        ////            subject = "EMAIL_USER_UPDATED_OWN_PASSWORD_SUBJECT";
        ////            body = "EMAIL_USER_UPDATED_OWN_PASSWORD_BODY";
        ////            break;
        ////    }
        ////    subject = Localization.Localization.GetSystemMessage(locale, settings, subject, user, Localization.Localization.GlobalResourceFile, custom, "", settings.AdministratorId);
        ////    body = Localization.Localization.GetSystemMessage(locale, settings, body, user, Localization.Localization.GlobalResourceFile, custom, "", settings.AdministratorId);
        ////    SendEmail(settings.Email, UserController.GetUserById(settings.PortalId, toUser).Email, subject, body);
        ////    return "";
        ////}

        public static string SendMail(string MailFrom, string MailTo, string Bcc, string Subject, string Body, string Attachment, string BodyType, string SMTPServer, string SMTPAuthentication, string SMTPUsername,
        string SMTPPassword)
        {
            MailFormat objBodyFormat = MailFormat.Text;
            if (!String.IsNullOrEmpty(BodyType))
            {
                switch (BodyType.ToLower())
                {
                    case "html":
                        objBodyFormat = MailFormat.Html;
                        break;
                    case "text":
                        objBodyFormat = MailFormat.Text;
                        break;
                }
            }
            return SendMail(MailFrom, MailTo, "", Bcc, System.Net.Mail.MailPriority.Normal, Subject, objBodyFormat, System.Text.Encoding.UTF8, Body, Attachment,
            SMTPServer,  SMTPAuthentication, SMTPUsername, SMTPPassword);
        }
        public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body, string Attachment,
        string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword)
        {
            bool SMTPEnableSSL = HostSettings.SMTPEnableSSL;
            return SendMail(MailFrom, MailTo, Cc, Bcc, Priority, Subject, BodyFormat, BodyEncoding, Body, Attachment,
            SMTPServer, SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
        }
        public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body, string Attachment,
        string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        {
            return SendMail(MailFrom, MailTo, Cc, Bcc, MailFrom, Priority, Subject, BodyFormat, BodyEncoding, Body,
            Attachment.Split('|'), SMTPServer,  SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
        }
        public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body, string[] Attachment,
        string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        {
            return SendMail(MailFrom, MailTo, Cc, Bcc, MailFrom, Priority, Subject, BodyFormat, BodyEncoding, Body,
            Attachment, SMTPServer,  SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
        }
        public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, string ReplyTo, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body,
        string[] Attachment, string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach (string myAtt in Attachment)
            {
                if (!String.IsNullOrEmpty(myAtt))
                    attachments.Add(new Attachment(myAtt));
            }
            return SendMail(MailFrom, MailTo, Cc, Bcc, ReplyTo, Priority, Subject, BodyFormat, BodyEncoding, Body,
            attachments, SMTPServer,  SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL);
        }
        //public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, string ReplyTo, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body,
        //List<System.Net.Mail.Attachment> Attachments, string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        //{
        //    string retValue = "";
        //    //if (!IsValidEmailAddress(MailFrom, PortalSettings.Current != null ? PortalSettings.Current.PortalId : Null.NullInteger))
        //    //{
        //    //    ArgumentException ex = new ArgumentException(string.Format("EXCEPTION_InvalidEmailAddress", PortalSettings.Current), MailFrom);
        //    //    return ex.Message;
        //    //}

        //    if (string.IsNullOrEmpty(SMTPServer) && !string.IsNullOrEmpty(HostSettings.SMTPServer))
        //    {
        //        SMTPServer = HostSettings.SMTPServer;
        //    }
        //    if (string.IsNullOrEmpty(SMTPAuthentication) && !string.IsNullOrEmpty(HostSettings.SMTPAuthentication))
        //    {
        //        SMTPAuthentication = HostSettings.SMTPAuthentication;
        //    }
        //    if (string.IsNullOrEmpty(SMTPUsername) && !string.IsNullOrEmpty(HostSettings.SMTPUsername))
        //    {
        //        SMTPUsername = HostSettings.SMTPUsername;
        //    }
        //    if (string.IsNullOrEmpty(SMTPPassword) && !string.IsNullOrEmpty(HostSettings.SMTPPassword))
        //    {
        //        SMTPPassword = HostSettings.SMTPPassword;
        //    }
        //    MailTo = MailTo.Replace(";", ",");
        //    Cc = Cc.Replace(";", ",");
        //    Bcc = Bcc.Replace(";", ",");
        //    System.Net.Mail.MailMessage objMail = null;
        //    //try
        //    //{
        //        objMail = new System.Net.Mail.MailMessage();
        //        if (!String.IsNullOrEmpty(MailFrom))
        //        {
        //            objMail.From = new MailAddress(MailFrom);
        //        }                    
        //        if (!String.IsNullOrEmpty(MailTo))
        //        {
        //            objMail.To.Add(MailTo);
        //        }
        //        if (!String.IsNullOrEmpty(Cc))
        //        {
        //            objMail.CC.Add(Cc);
        //        }
        //        if (!String.IsNullOrEmpty(Bcc))
        //        {
        //            objMail.Bcc.Add(Bcc);
        //        }
        //        if (ReplyTo != string.Empty)
        //            objMail.ReplyToList.Add(new System.Net.Mail.MailAddress(ReplyTo));
        //        objMail.Priority = (System.Net.Mail.MailPriority)Priority;
        //        objMail.IsBodyHtml = BodyFormat == MailFormat.Html;
        //        if (Attachments!=null)
        //        {
        //            foreach (Attachment myAtt in Attachments)
        //            {
        //                objMail.Attachments.Add(myAtt);
        //            }
        //        }
        //        objMail.HeadersEncoding = BodyEncoding;
        //        objMail.SubjectEncoding = BodyEncoding;
        //        objMail.Subject = HtmlUtils.StripWhiteSpace(Subject, true);
        //        objMail.BodyEncoding = BodyEncoding;
        //        System.Net.Mail.AlternateView PlainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(ConvertToText(Body), null, "text/plain");
        //        objMail.AlternateViews.Add(PlainView);
        //        if (HtmlUtils.IsHtml(Body))
        //        {
        //            System.Net.Mail.AlternateView HTMLView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
        //            objMail.AlternateViews.Add(HTMLView);
        //        }
        //    //}
        //    //catch (Exception objException)
        //    //{
        //    //    retValue = MailTo + ": " + objException.Message;
        //    //}
        //    if (objMail != null)
        //    {
        //        int SmtpPort = Null.NullInteger;
        //        int portPos = SMTPServer.IndexOf(":");
        //        if (portPos > -1)
        //        {
        //            SmtpPort = Int32.Parse(SMTPServer.Substring(portPos + 1, SMTPServer.Length - portPos - 1));
        //            SMTPServer = SMTPServer.Substring(0, portPos);
        //        }
        //        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
        //        //try
        //        //{
        //            if (!String.IsNullOrEmpty(SMTPServer))
        //            {
        //                smtpClient.Host = SMTPServer;
        //                if (SmtpPort > Null.NullInteger)
        //                {
        //                    smtpClient.Port = SmtpPort;
        //                }
        //                switch (SMTPAuthentication)
        //                {
        //                    case "":
        //                    case "0":
        //                        break;
        //                    case "1":
        //                        if (!String.IsNullOrEmpty(SMTPUsername) && !String.IsNullOrEmpty(SMTPPassword))
        //                        {
        //                            smtpClient.UseDefaultCredentials = false;
        //                            smtpClient.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
        //                        }
        //                        break;
        //                    case "2":
        //                        smtpClient.UseDefaultCredentials = true;
        //                        break;
        //                }
        //            }
        //            smtpClient.Timeout = 100;
        //            smtpClient.EnableSsl = SMTPEnableSSL;
        //            smtpClient.Send(objMail);
        //            retValue = "";
        //        //}
        //        //catch (SmtpFailedRecipientException exc)
        //        //{
        //        //    retValue = "FailedRecipient :" + exc.FailedRecipient;
        //        //}
        //        //catch (SmtpException exc)
        //        //{
        //        //    retValue = "SMTPConfigurationProblem" + exc.ToString();
        //        //}
        //        //catch (Exception objException)
        //        //{
        //        //    if (objException.InnerException != null)
        //        //    {
        //        //        retValue = string.Concat(objException.Message, Environment.NewLine, objException.InnerException.Message);
        //        //    }
        //        //    else
        //        //    {
        //        //        retValue = objException.Message;
        //        //    }
        //        //}
        //        //finally
        //        //{
        //        //    objMail.Dispose();
        //        //}
        //    }
        //    return retValue;
        //}

        //public static void SendEmail(string fromAddress, string toAddress, string subject, string body)
        //{
        //    SendEmail(fromAddress, fromAddress, toAddress, subject, body);
        //}
        //public static void SendEmail(string fromAddress, string senderAddress, string toAddress, string subject, string body)
        //{

        //    if ((string.IsNullOrEmpty(HostSettings.SMTPServer)))
        //    {
        //        //throw new InvalidOperationException("SMTP Server not configured");
        //        return;
        //    }


        //    System.Net.Mail.MailMessage emailMessage = new System.Net.Mail.MailMessage(fromAddress, toAddress, subject, body);
        //    emailMessage.Sender = new MailAddress(senderAddress);

        //    if (HtmlUtils.IsHtml(body))
        //    {
        //        emailMessage.IsBodyHtml = true;
        //    }

        //    System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(HostSettings.SMTPServer);

        //    string[] smtpHostParts = HostSettings.SMTPServer.Split(':');
        //    if (smtpHostParts.Length > 1)
        //    {
        //        smtpClient.Host = smtpHostParts[0];
        //        smtpClient.Port = Convert.ToInt32(smtpHostParts[1]);
        //    }

        //
        //    switch (HostSettings.SMTPAuthentication)
        //    {
        //        case "":
        //        case "0":
        //            // anonymous
        //            break;
        //        case "1":
        //            // basic
        //            if (!string.IsNullOrEmpty(HostSettings.SMTPUsername) & !string.IsNullOrEmpty(HostSettings.SMTPPassword))
        //            {
        //                smtpClient.UseDefaultCredentials = false;
        //                smtpClient.Credentials = new System.Net.NetworkCredential(HostSettings.SMTPUsername, HostSettings.SMTPPassword);
        //            }
        //            break;
        //        case "2":
        //            smtpClient.UseDefaultCredentials = true;
        //            break;
        //    }

        //    smtpClient.EnableSsl = HostSettings.EnableSMTPSSL;

        //    //Retry up to 5 times to send the message
        //    for (int index = 0; index < 5; index++)
        //    {
        //        try
        //        {
        //            smtpClient.Send(emailMessage);
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            if (index == 5)
        //            {
        //                ex.ToString();
        //            }
        //            System.Threading.Thread.Sleep(1000);
        //        }
        //    }
        //}
        public static string SendMail(string MailFrom, string MailTo, string Cc, string Bcc, string ReplyTo, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body,
        List<System.Net.Mail.Attachment> Attachments, string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        {
            string retValue = "";

            if (!ValidatorUtils.IsValidEmailAddress(MailFrom))
            {
                ArgumentException ex = new ArgumentException(MailFrom);
                return ex.Message;
            }

            if (string.IsNullOrEmpty(SMTPServer) && !string.IsNullOrEmpty(HostSettings.SMTPServer))
            {
                SMTPServer = HostSettings.SMTPServer;
            }
            if (string.IsNullOrEmpty(SMTPAuthentication) && !string.IsNullOrEmpty(HostSettings.SMTPAuthentication))
            {
                SMTPAuthentication = HostSettings.SMTPAuthentication;
            }
            if (string.IsNullOrEmpty(SMTPUsername) && !string.IsNullOrEmpty(HostSettings.SMTPUsername))
            {
                SMTPUsername = HostSettings.SMTPUsername;
            }
            if (string.IsNullOrEmpty(SMTPPassword) && !string.IsNullOrEmpty(HostSettings.SMTPPassword))
            {
                SMTPPassword = HostSettings.SMTPPassword;
            }
            MailTo = MailTo.Replace(";", ",");
            Cc = Cc.Replace(";", ",");
            Bcc = Bcc.Replace(";", ",");

         
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            if (!String.IsNullOrEmpty(MailFrom))
            {
                mailMessage.From = new MailAddress(MailFrom);
                //mailMessage.From = new MailAddress(MailFrom, SMTPUsername, System.Text.UTF8Encoding.UTF8);
            }
            if (!String.IsNullOrEmpty(MailTo))
            {
                mailMessage.To.Add(MailTo);
                //mailMessage.To.Add(new MailAddress(MailTo, StringUtils.GetEmailAccount(MailTo), System.Text.UTF8Encoding.UTF8));
            }
            if (!String.IsNullOrEmpty(Cc))
                mailMessage.CC.Add(Cc);
            if (!String.IsNullOrEmpty(Bcc))
                mailMessage.Bcc.Add(Bcc);
            if (ReplyTo != string.Empty)
                mailMessage.ReplyToList.Add(new System.Net.Mail.MailAddress(ReplyTo));
            //mailMessage.Priority = (System.Net.Mail.MailPriority)Priority;
            mailMessage.IsBodyHtml = BodyFormat == MailFormat.Html;

            if (Attachments != null)
            {
                foreach (Attachment myAtt in Attachments)
                {
                    mailMessage.Attachments.Add(myAtt);
                }
            }
            mailMessage.HeadersEncoding = BodyEncoding;
            mailMessage.SubjectEncoding = BodyEncoding;
            mailMessage.Subject = HtmlUtils.StripWhiteSpace(Subject, true);
            mailMessage.BodyEncoding = BodyEncoding;
            mailMessage.Body = Body;
           
            //if (HtmlUtils.IsHtml(Body))
            if (BodyFormat == MailFormat.Html)
            {
                System.Net.Mail.AlternateView HTMLView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, System.Text.UnicodeEncoding.UTF8, "text/html");
                mailMessage.AlternateViews.Add(HTMLView);
            }
            else
            {
                System.Net.Mail.AlternateView PlainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(ConvertToText(Body), System.Text.UnicodeEncoding.UTF8, "text/plain");
                mailMessage.AlternateViews.Add(PlainView);
            }
            if (mailMessage != null)
            {
                int SmtpPort = Null.NullInteger;
                int portPos = SMTPServer.IndexOf(":");
                if (portPos > -1)
                {
                    SmtpPort = Int32.Parse(SMTPServer.Substring(portPos + 1, SMTPServer.Length - portPos - 1));
                    SMTPServer = SMTPServer.Substring(0, portPos);
                }

                /* Set the SMTP server and send the email*/
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();

                try
                {
                    if (!String.IsNullOrEmpty(SMTPServer))
                    {
                        smtpClient.Host = SMTPServer;
                        if (SmtpPort > Null.NullInteger)
                            smtpClient.Port = SmtpPort;
                        smtpClient.Timeout = 100000;
                        //smtpClient.EnableSsl = SMTPEnableSSL;
                        smtpClient.EnableSsl = true;
                       // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                        //Add this line to bypass the certificate validation
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        }; 

                        System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                        smtpClient.Credentials = smtp_user_info;

                        switch (SMTPAuthentication)
                        {
                            case "":
                            case "0":
                                break;
                            case "1":
                                if (!String.IsNullOrEmpty(SMTPUsername) && !String.IsNullOrEmpty(SMTPPassword))
                                {
                                    smtpClient.UseDefaultCredentials = true;
                                    smtpClient.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                                }
                                break;
                            case "2":
                                smtpClient.UseDefaultCredentials = true;
                                break;
                        }
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        retValue = "SMTPServer is empty";
                    }
                }
                catch (SmtpFailedRecipientException exc)
                {
                    retValue = "FailedRecipient :" + exc.FailedRecipient;
                }
                catch (SmtpException exc)
                {
                    retValue = "SMTPConfigurationProblem" + exc.ToString();
                }
                catch (Exception objException)
                {
                    if (objException.InnerException != null)
                    {
                        retValue = string.Concat(objException.Message, Environment.NewLine, objException.InnerException.Message);
                    }
                    else
                    {
                        retValue = objException.Message;
                    }
                }
                finally
                {
                    mailMessage.Dispose();
                }
            }
            return retValue;
        }
        public static string SendEmail(string fromAddress, string senderAddress, string toAddress, string subject, string body, List<Attachment> Attachments)
        {
            if ((string.IsNullOrEmpty(HostSettings.SMTPServer)))
            {
                return "SMTP Server not configured";
            }

            System.Net.Mail.MailMessage emailMessage = new System.Net.Mail.MailMessage(fromAddress, toAddress, subject, body);
            emailMessage.Sender = new MailAddress(senderAddress);

            if ((HtmlUtils.IsHtml(body)))
            {
                emailMessage.IsBodyHtml = true;
            }

            foreach (Attachment myAtt in Attachments)
            {
                emailMessage.Attachments.Add(myAtt);
            }

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(HostSettings.SMTPServer);

            string[] smtpHostParts = HostSettings.SMTPServer.Split(':');
            if (smtpHostParts.Length > 1)
            {
                smtpClient.Host = smtpHostParts[0];
                smtpClient.Port = Convert.ToInt32(smtpHostParts[1]);
            }


            switch (HostSettings.SMTPAuthentication)
            {
                case "":
                case "0":
                    // anonymous
                    break;
                case "1":
                    // basic
                    if (!string.IsNullOrEmpty(HostSettings.SMTPUsername) & !string.IsNullOrEmpty(HostSettings.SMTPPassword))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential(HostSettings.SMTPUsername, HostSettings.SMTPPassword);
                    }
                    break;
                case "2":
                    // NTLM
                    smtpClient.UseDefaultCredentials = true;
                    break;
            }

            smtpClient.EnableSsl = HostSettings.SMTPEnableSSL;

            //'Retry up to 5 times to send the message
            for (int index = 1; index <= 5; index++)
            {
                try
                {
                    smtpClient.Send(emailMessage);
                    return "";
                }
                catch (Exception ex)
                {
                    if ((index == 5))
                    {
                        ex.ToString();
                    }
                    Thread.Sleep(1000);
                }
            }

            return "";

        }

        //static internal bool RouteToUserMessaging(string MailFrom, string MailTo, string Cc, string Bcc, string Subject, string Body, List<Attachment> Attachments)
        //{
        //    int totalRecords = -1;
        //    UserController user_entity = new UserController();
        //    ArrayList fromUsersList = user_entity.GetUserInfoByEmailPortaId(PortalSettings.Current.PortalId, MailFrom);
        //    UserInfo fromUser = default(UserInfo);
        //    if ((fromUsersList.Count != 0))
        //    {
        //        fromUser = (UserInfo)fromUsersList[0];
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    List<string> ToEmails = new List<string>();
        //    List<UserInfo> ToUsers = new List<UserInfo>();

        //    if ((!string.IsNullOrEmpty(MailTo)))
        //    {
        //        ToEmails.AddRange(MailTo.Split(';', ','));
        //    }

        //    if ((!string.IsNullOrEmpty(Cc)))
        //    {
        //        ToEmails.AddRange(Cc.Split(';', ','));
        //    }

        //    if ((!string.IsNullOrEmpty(Bcc)))
        //    {
        //        ToEmails.AddRange(Bcc.Split(';', ','));
        //    }

        //    foreach (string email in ToEmails)
        //    {
        //        if ((!string.IsNullOrEmpty(email)))
        //        {
        //            UserController user_obj = new UserController();
        //            ArrayList toUsersList = user_obj.GetUserInfoByEmailPortaId(PortalSettings.Current.PortalId, email);
        //            if ((toUsersList.Count != 0))
        //            {
        //                ToUsers.Add((UserInfo)toUsersList[0]);
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //Messaging.MessagingController messageController = new Messaging.MessagingController();

        //foreach (UserInfo recepient in ToUsers)
        //{
        //    Messaging.Data.Message message = new Messaging.Data.Message();
        //    message.FromUserID = fromUser.UserID;
        //    message.Subject = Subject;
        //    message.Body = Body;
        //    message.ToUserID = recepient.UserID;
        //    message.Status = Messaging.Data.MessageStatusType.Unread;

        //    messageController.SaveMessage(message);

        //}
        //return true;
        //}

        //#region Email Verified ===========================================================================================================================================
        public static bool Send_Mail(string MailNameFrom, string MailFrom, string MailFromPass, string MailNameTo, string MailTo, string Subject, string Body)
        {
            bool flag = false;
            string MailFromAccount = MailFrom.Substring(0, MailFrom.IndexOf('@'));
            System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailNameFrom, System.Text.UTF8Encoding.UTF8);
            mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailNameTo.Trim(), System.Text.UTF8Encoding.UTF8));
            mailMessage.Subject = Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;

            /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //port=25           
            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = smtp_user_info;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtp.Send(mailMessage);
            flag = true;
            return flag;
        }

        public static bool Send_Mail_By_Gmail(string MailFrom, string MailFromPass, string MailTo, string Subject, string Body)
        {
            bool flag = false;
            string MailFromAccount = StringUtils.GetEmailAccount(MailFrom);
            string MailToAccount = StringUtils.GetEmailAccount(MailTo);
            System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailFromAccount, System.Text.UTF8Encoding.UTF8);
            mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailToAccount, System.Text.UTF8Encoding.UTF8));
            mailMessage.Subject = Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
            //mailMessage.Priority = MailPriority.High;

            /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //port=25           
            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.Credentials = smtp_user_info;

            try
            {
                smtp.Send(mailMessage);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return flag;
        }

        public static bool Send_Mail_By_Gmail(string MailNameFrom, string MailFrom, string MailFromPass, string MailNameTo, string MailTo, string Subject, string Body)
        {
            bool flag = false;

            string MailFromAccount = MailFrom.Substring(0, MailFrom.IndexOf('@'));

            System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailNameFrom.Trim(), System.Text.UTF8Encoding.UTF8);
            mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailNameTo.Trim(), System.Text.UTF8Encoding.UTF8));
            mailMessage.Subject = Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
            //mailMessage.Priority = MailPriority.High;

            /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //port=25           
            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.Credentials = smtp_user_info;

            try
            {
                smtp.Send(mailMessage);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return flag;
        }

        public static bool Send_Mail_Yahoo(string MailNameFrom, string MailFrom, string MailFromPass, string MailNameTo, string MailTo, string Subject, string Body)
        {
            bool flag = false;
            string MailFromAccount = MailFrom.Substring(0, MailFrom.IndexOf('@'));
            System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailNameFrom.Trim(), System.Text.UTF8Encoding.UTF8);
            mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailNameTo.Trim(), System.Text.UTF8Encoding.UTF8));
            mailMessage.Subject = Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
            //mailMessage.Priority = MailPriority.High;

            /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
            //Khai báo Server Port Numbers cho Outgoing mail là 465, cho Incoming mail là 995.
            //Khai báo Incoming mail là pop.mailyahoo.com.vn [cư trú là Việt Nam] và Port là 995.
            //Khai báo Outgoing mail là smtp.yahoo.com.vn và Port là 465.
            //Khai báo Incoming mail là pop.mailyahoo.com và Port là 110.
            //Khai báo Outgoing mail là smtp.mail.yahoo.com và Port là 25.

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.mail.yahoo.com";
            smtp.Port = 465;
            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.Credentials = smtp_user_info;

            try
            {
                smtp.Send(mailMessage);
                flag = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return flag;
        }

        public static bool Send_Mail_By_Gmail_With_Attachment(string MailFrom, string MailFromPass, string MailTo, string Subject, string Body, string AttachmentPath)
        {
            bool flag = false; 
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            string MailFromAccount = StringUtils.GetEmailAccount(MailFrom);
            string MailToAccount = StringUtils.GetEmailAccount(MailTo); //Danh sách email được ngăn cách nhau bởi dấu ";"               
         
            String[] ALL_EMAILS = MailTo.Split(';');

            foreach (string emailaddress in ALL_EMAILS)
            {
                flag = regex.IsMatch(emailaddress);
            }

            if (flag == true)
            {
                System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(AttachmentPath);
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();      

                mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailFromAccount, System.Text.UTF8Encoding.UTF8);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailToAccount, System.Text.UTF8Encoding.UTF8));
                mailMessage.Subject = Subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
                //mailMessage.Priority = MailPriority.High;


                /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; //port=25           
                smtp.Timeout = 100000;
                smtp.EnableSsl = true;
                smtp.Credentials = smtp_user_info;

                try
                {
                    smtp.Send(mailMessage);
                    flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return flag;            
        }

        public static bool Send_Mail_By_Gmail_With_BCC_Attachment(string MailFrom, string MailFromPass, string MailTo, string Bcc, string Subject, string Body, string AttachmentPath)
        {
            bool flag = false; 
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            string MailFromAccount = StringUtils.GetEmailAccount(MailFrom);
            string MailToAccount = StringUtils.GetEmailAccount(MailTo); //Danh sách email được ngăn cách nhau bởi dấu ";"               

            String[] ALL_EMAILS = MailTo.Split(';');

            foreach (string emailaddress in ALL_EMAILS)
            {
                flag = regex.IsMatch(emailaddress);
            }

            if (flag == true)
            {
                System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                if (!string.IsNullOrEmpty(Bcc))
                    mailMessage.Bcc.Add(Bcc);

                mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailFromAccount, System.Text.UTF8Encoding.UTF8);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailToAccount, System.Text.UTF8Encoding.UTF8));
                mailMessage.Subject = Subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
                //mailMessage.Priority = MailPriority.High;



                /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; //port=25           
                smtp.Timeout = 100000;
                smtp.EnableSsl = true;
                smtp.Credentials = smtp_user_info;

                try
                {
                    smtp.Send(mailMessage);
                    flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return flag;
        }

        public static bool Send_Mail_By_Gmail_With_CC_BCC_Attachment(string MailFrom, string MailFromPass, string MailTo, string Cc, string Bcc, string Subject, string Body, string AttachmentPath)
        {
            bool flag = false;    
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            string MailFromAccount = StringUtils.GetEmailAccount(MailFrom);
            string MailToAccount = StringUtils.GetEmailAccount(MailTo); //Danh sách email được ngăn cách nhau bởi dấu ";"               
         
            String[] ALL_EMAILS = MailTo.Split(';');

            foreach (string emailaddress in ALL_EMAILS)
            {
                flag = regex.IsMatch(emailaddress);                   
            }

            if (flag == true)
            {                    
                System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(AttachmentPath);
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                if (!string.IsNullOrEmpty(Cc))
                    mailMessage.CC.Add(Cc);
                if (!string.IsNullOrEmpty(Bcc))
                    mailMessage.Bcc.Add(Bcc);


                mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailFromAccount, System.Text.UTF8Encoding.UTF8);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailToAccount, System.Text.UTF8Encoding.UTF8));
                mailMessage.Subject = Subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
                //mailMessage.Priority = MailPriority.High;



                /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; //port=25           
                smtp.Timeout = 100000;
                smtp.EnableSsl = true;
                smtp.Credentials = smtp_user_info;
                try
                {
                    smtp.Send(mailMessage);
                    flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }               
            return flag;
        }
        
        public static bool Send_Mail_By_Gmail_With_CC_BCC(string MailFrom, string MailFromPass, string MailTo, string Cc, string Bcc, string Subject, string Body)
        {
            bool flag = false;            
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            string MailFromAccount = StringUtils.GetEmailAccount(MailFrom);
            string MailToAccount = StringUtils.GetEmailAccount(MailTo); //Danh sách email được ngăn cách nhau bởi dấu ";"               
         
            String[] ALL_EMAILS = MailTo.Split(';');

            foreach (string emailaddress in ALL_EMAILS)
            {
                flag = regex.IsMatch(emailaddress);
            }

            if (flag == true)
            {
                System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(MailFromAccount, MailFromPass);
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                if (!string.IsNullOrEmpty(Cc))
                    mailMessage.CC.Add(Cc);
                if (!string.IsNullOrEmpty(Bcc))
                    mailMessage.Bcc.Add(Bcc);                       

                mailMessage.From = new System.Net.Mail.MailAddress(MailFrom, MailFromAccount, System.Text.UTF8Encoding.UTF8);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(MailTo, MailToAccount, System.Text.UTF8Encoding.UTF8));
                mailMessage.Subject = Subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.UnicodeEncoding.UTF8;
                //mailMessage.Priority = MailPriority.High;



                /* Set the SMTP server and send the email - SMTP gmail ="smtp.gmail.com" port=587*/
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; //port=25           
                smtp.Timeout = 100000;
                smtp.EnableSsl = true;
                smtp.Credentials = smtp_user_info;

                try
                {
                    smtp.Send(mailMessage);
                    flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return flag;
           
        }
        // #endregion ========================================================================================================================================================




        public static string SendMailWithTLS(string MailFrom, string MailTo, string Cc, string Bcc, string ReplyTo, System.Net.Mail.MailPriority Priority, string Subject, MailFormat BodyFormat, System.Text.Encoding BodyEncoding, string Body,
       List<System.Net.Mail.Attachment> Attachments, string SMTPServer, string SMTPAuthentication, string SMTPUsername, string SMTPPassword, bool SMTPEnableSSL)
        {
            string retValue = "";

            if (!ValidatorUtils.IsValidEmailAddress(MailFrom))
            {
                ArgumentException ex = new ArgumentException(MailFrom);
                return ex.Message;
            }

            if (string.IsNullOrEmpty(SMTPServer) && !string.IsNullOrEmpty(HostSettings.SMTPServer))
            {
                SMTPServer = HostSettings.SMTPServer;
            }
            if (string.IsNullOrEmpty(SMTPAuthentication) && !string.IsNullOrEmpty(HostSettings.SMTPAuthentication))
            {
                SMTPAuthentication = HostSettings.SMTPAuthentication;
            }
            if (string.IsNullOrEmpty(SMTPUsername) && !string.IsNullOrEmpty(HostSettings.SMTPUsername))
            {
                SMTPUsername = HostSettings.SMTPUsername;
            }
            if (string.IsNullOrEmpty(SMTPPassword) && !string.IsNullOrEmpty(HostSettings.SMTPPassword))
            {
                SMTPPassword = HostSettings.SMTPPassword;
            }
            MailTo = MailTo.Replace(";", ",");
            Cc = Cc.Replace(";", ",");
            Bcc = Bcc.Replace(";", ",");


            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            if (!String.IsNullOrEmpty(MailFrom))
            {
                mailMessage.From = new MailAddress(MailFrom);
                //mailMessage.From = new MailAddress(MailFrom, SMTPUsername, System.Text.UTF8Encoding.UTF8);
            }
            if (!String.IsNullOrEmpty(MailTo))
            {
                mailMessage.To.Add(MailTo);
                //mailMessage.To.Add(new MailAddress(MailTo, StringUtils.GetEmailAccount(MailTo), System.Text.UTF8Encoding.UTF8));
            }
            if (!String.IsNullOrEmpty(Cc))
                mailMessage.CC.Add(Cc);
            if (!String.IsNullOrEmpty(Bcc))
                mailMessage.Bcc.Add(Bcc);
            if (ReplyTo != string.Empty)
                mailMessage.ReplyToList.Add(new System.Net.Mail.MailAddress(ReplyTo));
            //mailMessage.Priority = (System.Net.Mail.MailPriority)Priority;
            mailMessage.IsBodyHtml = BodyFormat == MailFormat.Html;

            if (Attachments != null)
            {
                foreach (Attachment myAtt in Attachments)
                {
                    mailMessage.Attachments.Add(myAtt);
                }
            }
            mailMessage.HeadersEncoding = BodyEncoding;
            mailMessage.SubjectEncoding = BodyEncoding;
            mailMessage.Subject = HtmlUtils.StripWhiteSpace(Subject, true);
            mailMessage.BodyEncoding = BodyEncoding;
            mailMessage.Body = Body;

            //if (HtmlUtils.IsHtml(Body))
            if (BodyFormat == MailFormat.Html)
            {
                System.Net.Mail.AlternateView HTMLView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, System.Text.UnicodeEncoding.UTF8, "text/html");
                mailMessage.AlternateViews.Add(HTMLView);
            }
            else
            {
                System.Net.Mail.AlternateView PlainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(ConvertToText(Body), System.Text.UnicodeEncoding.UTF8, "text/plain");
                mailMessage.AlternateViews.Add(PlainView);
            }
            if (mailMessage != null)
            {
                int SmtpPort = Null.NullInteger;
                int portPos = SMTPServer.IndexOf(":");
                if (portPos > -1)
                {
                    SmtpPort = Int32.Parse(SMTPServer.Substring(portPos + 1, SMTPServer.Length - portPos - 1));
                    SMTPServer = SMTPServer.Substring(0, portPos);
                }

                /* Set the SMTP server and send the email*/
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();

                try
                {
                    if (!String.IsNullOrEmpty(SMTPServer))
                    {
                        smtpClient.Host = SMTPServer;
                        if (SmtpPort > Null.NullInteger)
                            smtpClient.Port = SmtpPort;
                        smtpClient.Timeout = 100000;
                        //smtpClient.EnableSsl = SMTPEnableSSL;
                        smtpClient.EnableSsl = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                        System.Net.NetworkCredential smtp_user_info = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                        smtpClient.Credentials = smtp_user_info;

                        switch (SMTPAuthentication)
                        {
                            case "":
                            case "0":
                                break;
                            case "1":
                                if (!String.IsNullOrEmpty(SMTPUsername) && !String.IsNullOrEmpty(SMTPPassword))
                                {
                                    smtpClient.UseDefaultCredentials = true;
                                    smtpClient.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                                }
                                break;
                            case "2":
                                smtpClient.UseDefaultCredentials = true;
                                break;
                        }

                        //Add this line to bypass the certificate validation
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        }; 
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        retValue = "SMTPServer is empty";
                    }
                }
                catch (SmtpFailedRecipientException exc)
                {
                    retValue = "FailedRecipient :" + exc.FailedRecipient;
                }
                catch (SmtpException exc)
                {
                    retValue = "SMTPConfigurationProblem" + exc.ToString();
                }
                catch (Exception objException)
                {
                    if (objException.InnerException != null)
                    {
                        retValue = string.Concat(objException.Message, Environment.NewLine, objException.InnerException.Message);
                    }
                    else
                    {
                        retValue = objException.Message;
                    }
                }
                finally
                {
                    mailMessage.Dispose();
                }
            }
            return retValue;
        }


    }
}
