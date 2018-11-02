using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class UserAccountTasks
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
                    return bytes = user.Properties["ThumbnailPhoto"].Value as byte[];
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
                    Username = user.SamAccountName,
                    DistinguishedName = user.DistinguishedName
                };

                using (var directoryEntry = user.GetUnderlyingObject() as DirectoryEntry)
                {
                    if (directoryEntry == null) return userAccount;

                    userAccount.Company = directoryEntry.Properties["company"].Value.ToString();
                    userAccount.Department = directoryEntry.Properties["department"].Value.ToString();
                    userAccount.JobTitle = directoryEntry.Properties["title"].Value.ToString();
                    userAccount.Office = directoryEntry.Properties["physicalDeliveryOfficeName"].Value.ToString();
                    userAccount.WhenCreated = Convert.ToDateTime(directoryEntry.Properties["whenCreated"].Value);
                    userAccount.ThumbnailPhoto = directoryEntry.Properties["ThumbnailPhoto"].Value as byte[];
                    // PROXY ADDRESS["proxyAddresses"] and is multi value strings

                    //string username = "username";
                    //string domain = "domain";

                    //List<string> emailAddresses = new List<string>();

                    //PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domain);
                    //UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, username);

                    //// Add the "mail" entry
                    //emailAddresses.Add(user.EmailAddress);

                    //// Add the "proxyaddresses" entries.
                    //PropertyCollection properties = ((DirectoryEntry)user.GetUnderlyingObject()).Properties;
                    //foreach (object property in properties["proxyaddresses"])
                    //{
                    //    emailAddresses.Add(property.ToString());
                    //}
                }

                return userAccount;
            }
        }

        public void MoveToOrganisationalUnit(string username, string newOrganisationalUnit)
        {
            var auditTasks = new AuditTasks();
            if (!auditTasks.DoesOrganisationalUnitExist(newOrganisationalUnit)) return;
                  
            var user = GetUserAccountDetails(username);

            var originalLocation = new DirectoryEntry($"LDAP://{user.DistinguishedName}");
            var newLocation = new DirectoryEntry($"LDAP://{newOrganisationalUnit}");
            originalLocation.MoveTo(newLocation);
            originalLocation.Close();
            newLocation.Close();
        }
    }
}
