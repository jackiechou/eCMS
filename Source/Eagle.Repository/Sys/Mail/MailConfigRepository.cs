using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository
{
    public class MailConfigRepository
    {
        public EntityDataContext context { get; set; }
        public MailConfigRepository(EntityDataContext context)
        {
            this.context = context;
        }
         public void GetCcAndBcc(out string ccMail, out string bccMail, int functionID)
        {
            var resultCC = (from o in context.LS_tblOnlineProcess
                            join cc in context.LS_tblOnlineProcessMailCc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                            join emp in context.HR_tblEmp on cc.EmpID equals emp.EmpID
                            where o.FunctionID == functionID
                            select emp.Email).ToArray();
            var resultBcc = (from o in context.LS_tblOnlineProcess
                             join cc in context.LS_tblOnlineProcessMailBcc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                             join emp in context.HR_tblEmp on cc.EmpID equals emp.EmpID
                                where o.FunctionID == functionID
                                select emp.Email).ToArray();
            ccMail = string.Join(";", resultCC);
            bccMail = string.Join(";", resultBcc);
        }
    }
   
}
