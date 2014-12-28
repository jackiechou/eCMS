using Eagle.Common.Utilities;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.Mail;

namespace Eagle.Repository.Mail
{
    public class MailAccountRespository
    {
        public static Mail_Accounts GetDetails(int? Mail_Account_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Accounts
                            where x.Mail_Account_Id == Mail_Account_Id
                            select x;
                return query.FirstOrDefault();
            }
        }

        public static MailServerProviderAccountViewModel GetFullDetails(int? Mail_Account_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = (from x in dbContext.Mail_Accounts
                             join y in dbContext.Mail_Server_Providers on x.Mail_Server_Provider_Id equals y.Mail_Server_Provider_Id
                             where x.Mail_Account_Id == Mail_Account_Id
                             select new MailServerProviderAccountViewModel()
                             {
                                 Mail_Account_Id = x.Mail_Account_Id,                               
                                 Mail_Server_Provider_Id = x.Mail_Server_Provider_Id,
                                 Mail_Sender_Name = x.Mail_Sender_Name,
                                 Mail_Contact_Name = x.Mail_Contact_Name,
                                 Mail_Address = x.Mail_Address,
                                 Mail_Account = x.Mail_Account,
                                 Mail_Password = x.Mail_Password,
                                 Mail_Signature = x.Mail_Signature,
                                 Mail_Server_Provider_Name = y.Mail_Server_Provider_Name,
                                 Mail_Server_Protocol = y.Mail_Server_Protocol,
                                 Incoming_Mail_Server_Host = y.Incoming_Mail_Server_Host,
                                 Incoming_Mail_Server_Port = y.Incoming_Mail_Server_Port,
                                 Outgoing_Mail_Server_Host = y.Outgoing_Mail_Server_Host,
                                 Outgoing_Mail_Server_Port = y.Outgoing_Mail_Server_Port,
                                 SSL = y.SSL,
                                 TLS = y.TLS
                             }).FirstOrDefault();
                return query;
            }
        }

        public static List<Mail_Accounts> GetList()
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Accounts.OrderBy(o => o.Mail_Sender_Name)
                            select x;
                return query.ToList();
            }
        }

        public static List<Mail_Accounts> FindInfoByMail_Address(string Mail_Address)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Accounts
                            where x.Mail_Address == Mail_Address
                            select x;

                return query.ToList();
            }
        }

        public static List<Mail_Accounts> SelectPage(int startRowIndex, int pageSize)
        {
            return GetList().Skip(startRowIndex).Take(pageSize).ToList();
        }

        public static int SelectCount()
        {
            return GetList().Count();
        }

        public static IQueryable<Mail_Accounts> FetchData(int take, int pageSize)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from p in dbContext.Mail_Accounts
                            .OrderBy(o => o.Mail_Sender_Name)
                            .Take(take)
                            .Skip(pageSize)
                            select p;
                return query;
            }
        }

        public static bool IsIDExists(int ID)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = dbContext.Mail_Accounts.FirstOrDefault(c => c.Mail_Account_Id.Equals(ID));
                return (query != null);
            }
        }

        ////INSERT- UPDATE - DELETE 
        public static int[] Insert(MailAccountViewModel model)
        {
            int[] returnValue = new int[2];
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    bool isDuplicate = IsIDExists(model.Mail_Account_Id);
                    if (isDuplicate == false)
                     {
                         Mail_Accounts entity = new Mail_Accounts();
                         entity.Mail_Server_Provider_Id = model.Mail_Server_Provider_Id;
                         entity.Mail_Sender_Name = model.Mail_Sender_Name;
                         entity.Mail_Contact_Name = model.Mail_Contact_Name;
                         entity.Mail_Address = model.Mail_Address;
                         entity.Mail_Account = StringUtils.GetEmailAccount(model.Mail_Address);
                         entity.Mail_Password = model.Mail_Password;
                         entity.Mail_Signature = model.Mail_Signature;
                         dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                         returnValue[0] = dbContext.SaveChanges();
                         returnValue[1] = entity.Mail_Account_Id;
                         transcope.Complete();
                     }
                }
            }
            return returnValue;
        }

        public static int Update(MailAccountViewModel model)
        {
            int i = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    bool IDExist = IsIDExists(model.Mail_Account_Id);
                    if (IDExist == true)
                    {
                        Mail_Accounts entity = dbContext.Mail_Accounts.Single(x => x.Mail_Account_Id == model.Mail_Account_Id);
                        entity.Mail_Server_Provider_Id = model.Mail_Server_Provider_Id;
                        entity.Mail_Sender_Name = model.Mail_Sender_Name;
                        entity.Mail_Contact_Name = model.Mail_Contact_Name;
                        entity.Mail_Address = model.Mail_Address;
                        entity.Mail_Account = StringUtils.GetEmailAccount(model.Mail_Address);
                        entity.Mail_Password = model.Mail_Password;
                        entity.Mail_Signature = model.Mail_Signature;
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        i = dbContext.SaveChanges();
                        transcope.Complete();
                    }
                }
            }
            return i;
        }

        public static void Delete(int Mail_Account_Id)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Mail_Accounts entity = dbContext.Mail_Accounts.Single(x => x.Mail_Account_Id == Mail_Account_Id);
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    int i = dbContext.SaveChanges();
                    transcope.Complete();
                }
            }
        }
    }
}
