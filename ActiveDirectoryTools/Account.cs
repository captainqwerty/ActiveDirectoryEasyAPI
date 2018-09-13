using System;
using System.DirectoryServices;
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

        public byte[] GetThumbnailPhoto(string username)
        {
            byte[] bytes = null;

            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                var userPrincipal = new UserPrincipal(principalContext)
                {
                    SamAccountName = username
                };

                var principalSearcher = new PrincipalSearcher
                {
                    QueryFilter = userPrincipal
                };

                var result = principalSearcher.FindOne();

                if (result == null) return null;
                
                using (var user = result.GetUnderlyingObject() as DirectoryEntry)
                {
                    return bytes = user.Properties["thumbnailPhoto"][0] as byte[];
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
                    userAccount.thumbnailPhoto = directoryEntry.Properties["thumbnailPhoto"][0] as byte[];
                }

                return userAccount;
            }
        }
    }
}
