using System;
using System.Security.Principal;

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
        public string Company { get; set; }
        public string Department { get; set; }
        public DateTime? LastLogonDateTime { get; set; }
        public bool LockedOut { get; set; }
        public SecurityIdentifier Sid { get; set; }
        public DateTime? WhenCreated { get; set; }
    }
}
