using System;
using System.Collections.Generic;
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
        /// <param name="expireNow">User reset password at next logon.</param>
        public void PasswordReset(string username, string password, bool expireNow = true)
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
    }
}
