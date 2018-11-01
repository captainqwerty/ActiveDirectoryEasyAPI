using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class AuditTasks
    {
        public List<UserAccount> GetAllLockedOutAccounts(string organisationalUnit)
        {
            var accountTools = new AccountTasks();

            var lockedUsers = new List<UserAccount>();

            using (var context = new PrincipalContext(ContextType.Domain, "GEN2TRAINING", organisationalUnit))
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
    }
}
