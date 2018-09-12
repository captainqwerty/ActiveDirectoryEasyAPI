using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryTools
{
    public class UserAccount : IUserAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Office { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public DateTime? LastLogonDateTime { get; set; }
        public bool LockedOut { get; set; }
        public SecurityIdentifier Sid { get; set; }
    }
}
