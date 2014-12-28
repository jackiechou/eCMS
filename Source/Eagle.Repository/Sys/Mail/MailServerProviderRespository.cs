using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Mail
{
    public class MailServerProviderRespository
    {
        public static List<Mail_Server_Providers> GetList()
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Server_Providers
                            select x;
                return query.ToList();
            }
        }

        public static Mail_Server_Providers GetDetail(int Mail_Server_Provider_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Server_Providers
                            where x.Mail_Server_Provider_Id == Mail_Server_Provider_Id
                            select x;
                return query.SingleOrDefault();
            }
        }

        //////INSERT- UPDATE - DELETE 
        public static int Insert(string Mail_Server_Provider_Name, string Mail_Server_Protocol, string Incoming_Mail_Server_Host, int Incoming_Mail_Server_Port,
            string Outgoing_Mail_Server_Host, int Outgoing_Mail_Server_Port, string SSL, string TLS)
        {
            int returnValue = 0;
            using (EntityDataContext context = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Mail_Server_Providers entity = new Mail_Server_Providers();
                    entity.Mail_Server_Provider_Name = Mail_Server_Provider_Name;
                    entity.Mail_Server_Protocol = Mail_Server_Protocol;
                    entity.Incoming_Mail_Server_Host = Incoming_Mail_Server_Host;
                    entity.Incoming_Mail_Server_Port = Incoming_Mail_Server_Port;
                    entity.Outgoing_Mail_Server_Host = Outgoing_Mail_Server_Host;
                    entity.Outgoing_Mail_Server_Port = Outgoing_Mail_Server_Port;
                    entity.SSL = SSL;
                    entity.TLS = TLS;
                    int affectedRow = 0;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        returnValue = entity.Mail_Server_Provider_Id;                                       
                    }
                    transcope.Complete();
                }
            }
            return returnValue;
        }

        public static int Update(int Mail_Server_Provider_Id, string Mail_Server_Provider_Name, string Mail_Server_Protocol, string Incoming_Mail_Server_Host, int Incoming_Mail_Server_Port,
            string Outgoing_Mail_Server_Host, int Outgoing_Mail_Server_Port, string SSL, string TLS)
        {
            int i = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Mail_Server_Providers entity = dbContext.Mail_Server_Providers.Single(x => x.Mail_Server_Provider_Id == Mail_Server_Provider_Id);
                    entity.Mail_Server_Provider_Name = Mail_Server_Provider_Name;
                    entity.Mail_Server_Protocol = Mail_Server_Protocol;
                    entity.Incoming_Mail_Server_Host = Incoming_Mail_Server_Host;
                    entity.Incoming_Mail_Server_Port = Incoming_Mail_Server_Port;
                    entity.Outgoing_Mail_Server_Host = Outgoing_Mail_Server_Host;
                    entity.Outgoing_Mail_Server_Port = Outgoing_Mail_Server_Port;
                    entity.SSL = SSL;
                    entity.TLS = TLS;
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    i = dbContext.SaveChanges();
                    transcope.Complete();
                }
            }
            return i;
        }

        public static int Delete(int Mail_Server_Provider_Id)
        {
            int i = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Mail_Server_Providers entity = dbContext.Mail_Server_Providers.Single(x => x.Mail_Server_Provider_Id == Mail_Server_Provider_Id);
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    i = dbContext.SaveChanges();
                    transcope.Complete();
                }
                return i;
            }
        }
    }
}
