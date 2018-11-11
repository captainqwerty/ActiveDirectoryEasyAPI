using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class AuditTasks
    {
        public List<UserAccount> GetAllLockedOutAccounts()
        {
            var accountTools = new UserAccountTasks();

            var lockedUsers = new List<UserAccount>();

            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var userPrincipal = new UserPrincipal(context);
  
                var search = new PrincipalSearcher(userPrincipal);

                foreach (var result in search.FindAll())
                {
                    var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, result.SamAccountName);

                    if (user != null && !user.IsAccountLockedOut()) continue;

                    lockedUsers.Add(accountTools.GetUserAccountDetails(user.UserPrincipalName));
                }

                lockedUsers.Sort();

                return lockedUsers;
            }
        }

        public bool DoesOrganisationalUnitExist(string organisationalUnit)
        {
            var organisationalUnitExists = DirectoryEntry.Exists("LDAP://" + organisationalUnit);

            return organisationalUnitExists;
        }

        public static List<UserPrincipal> GetInactiveUsers(TimeSpan inactivityTime)
        {
            var users = new List<UserPrincipal>();

            using (var domain = Domain.GetCurrentDomain())
            {
                foreach (DomainController domainController in domain.DomainControllers)
                {
                    using (var context = new PrincipalContext(ContextType.Domain, domainController.Name))
                    using (var userPrincipal = new UserPrincipal(context))
                    using (var searcher = new PrincipalSearcher(userPrincipal))
                    using (var results = searcher.FindAll())
                    {
                        users.AddRange(results.OfType<UserPrincipal>().Where(u => u.LastLogon.HasValue));
                    }
                }
            }

            return users.Where(u1 => !users.Any(u2 => u2.UserPrincipalName == u1.UserPrincipalName && u2.LastLogon > u1.LastLogon))
                .Where(u => (DateTime.Now - u.LastLogon) >= inactivityTime).ToList();
        }
    }
}
