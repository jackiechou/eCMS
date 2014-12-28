﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Mail
{
    public class MailServerProviderAccountViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailAccountId")]
        public int Mail_Account_Id { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailServerProviderId")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Mail_Server_Provider_Id { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailServerProviderId")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Sender_Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailContactName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Contact_Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailAddress")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Address { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailAccount")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Account { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailPassword")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Password { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailSignature")]
        public string Mail_Signature { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailServerProviderName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Server_Provider_Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailServerProtocol")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Mail_Server_Protocol { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IncomingMailServerHost")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Incoming_Mail_Server_Host { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OutgoingMailServerHost")]
        public Nullable<int> Incoming_Mail_Server_Port { get; set; }
        public string Outgoing_Mail_Server_Host { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OutgoingMailServerPort")]
        public Nullable<int> Outgoing_Mail_Server_Port { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SSL")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string SSL { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLS")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string TLS { get; set; }
    }
}
