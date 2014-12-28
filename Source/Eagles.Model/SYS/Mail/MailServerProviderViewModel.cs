using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Mail
{
    public class MailServerProviderViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MailServerProviderId")]
        public int Mail_Server_Provider_Id { get; set; }

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
