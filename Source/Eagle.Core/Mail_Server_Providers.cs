//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mail_Server_Providers
    {
        public int Mail_Server_Provider_Id { get; set; }
        public string Mail_Server_Provider_Name { get; set; }
        public string Mail_Server_Protocol { get; set; }
        public string Incoming_Mail_Server_Host { get; set; }
        public Nullable<int> Incoming_Mail_Server_Port { get; set; }
        public string Outgoing_Mail_Server_Host { get; set; }
        public Nullable<int> Outgoing_Mail_Server_Port { get; set; }
        public string SSL { get; set; }
        public string TLS { get; set; }
    }
}
