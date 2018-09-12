using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryTools
{
    public class Account
    {
        /// <summary>
        /// Reset an account password.
        /// </summary>
        /// <param name="username">Enter the username.</param>
        /// <param name="password">Enter the users new password.</param>
        /// <param name="expireNow">User must reset password at next logon.</param>
        public void SetUsersPassword(string username, string password, bool expireNow = true)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, username))
                {
                    if (user != null)
                    {
                        user.SetPassword(password);

                        if(expireNow)
                        {
                            user.ExpirePasswordNow();
                        }
                    }
                    else
                    {
                        // Throw error
                    }
                }
            }
        }

        public void UnlockAccount(string username)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, username))
                {
                    if (user != null)
                    {
                        user.UnlockAccount();
                    }
                }
            }
        }

        public DateTime? GetLastLogOn(string username)
        {
            DateTime? lastLogon = null;

            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, username))
                {
                    if (user != null)
                    {
                        lastLogon = user.LastLogon;
                    }
                }
            }

            return lastLogon;
        }

        public UserAccount GetUserAccountDetails(string username)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            using (var user = UserPrincipal.FindByIdentity(principalContext, username))
            {
                if (user == null) return null;

                var userAccount = new UserAccount()
                {
                    FirstName = user.GivenName,
                    LastName = user.Surname,
                    Description = user.Description,
                    LockedOut = user.IsAccountLockedOut(),
                    LastLogonDateTime = user.LastLogon,
                    EmailAddress = user.EmailAddress,
                    Sid = user.Sid
                };

                using (var directoryEntry = user.GetUnderlyingObject() as DirectoryEntry)
                {
                    if (directoryEntry == null) return userAccount;

                    userAccount.Company = directoryEntry.Properties["company"].Value.ToString();
                    userAccount.Department = directoryEntry.Properties["department"].Value.ToString();
                    userAccount.JobTitle = directoryEntry.Properties["title"].Value.ToString();
                    userAccount.Office = directoryEntry.Properties["physicalDeliveryOfficeName"].Value.ToString();
                    userAccount.WhenCreated = Convert.ToDateTime(directoryEntry.Properties["whenCreated"].Value);
                }

                return userAccount;
            }
        }
    }
}
