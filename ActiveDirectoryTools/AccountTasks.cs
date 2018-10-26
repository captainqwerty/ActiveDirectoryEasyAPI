using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class AccountTasks
    {
        /// <summary>
        /// Reset a user account password. The account will also be unlocked if it is currently locked.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">Enter the users new password</param>
        /// <param name="expireNow">Expire the password so the user must reset password at next logon?</param>
        public void SetUsersPassword(string username, string password, bool expireNow = false)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, username))
                {
                    if (user == null) return;
                
                    user.UnlockAccount();

                    user.SetPassword(password);

                    if(expireNow)
                    {
                        user.ExpirePasswordNow();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the users thumbnail photo.
        /// </summary>
        /// <param name="username">The username of the account photo to retrieve</param>
        /// <returns>Thumbnail phoot in bytes</returns>
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
                    return bytes = user.Properties["thumbnailPhoto"].Value as byte[];
                } 
            }
        }

        /// <summary>
        /// Unlock a locked user account without resetting their password.
        /// </summary>
        /// <param name="username">Username of the locked account to be unlocked</param>
        public void UnlockAccount(string username)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, username))
                {
                    user?.UnlockAccount();
                }
            }
        }

        /// <summary>
        /// Gets the users last log on in a DateTime format.
        /// </summary>
        /// <param name="username">Username to get the last logon time stamp</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A UserAccount model</returns>
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
                    Sid = user.Sid,
                    Username = user.SamAccountName
                };

                using (var directoryEntry = user.GetUnderlyingObject() as DirectoryEntry)
                {
                    if (directoryEntry == null) return userAccount;

                    userAccount.Company = directoryEntry.Properties["company"].Value.ToString();
                    userAccount.Department = directoryEntry.Properties["department"].Value.ToString();
                    userAccount.JobTitle = directoryEntry.Properties["title"].Value.ToString();
                    userAccount.Office = directoryEntry.Properties["physicalDeliveryOfficeName"].Value.ToString();
                    userAccount.WhenCreated = Convert.ToDateTime(directoryEntry.Properties["whenCreated"].Value);
                    userAccount.thumbnailPhoto = directoryEntry.Properties["thumbnailPhoto"].Value as byte[];
                }

                return userAccount;
            }
        }
    }
}
