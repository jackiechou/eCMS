using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.User
{
    public class UserEditModel
    {
        public System.Guid ApplicationCode { get; set; }
        public int UserId { get; set; }
        public System.Guid UserCode { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsAnonymous { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsSuperUser { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsLockedOut { get; set; }
        public Nullable<bool> UpdatePassword { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string LastIPAddress { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public string Comment { get; set; }
    }
}
