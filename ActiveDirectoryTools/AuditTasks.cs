using System;
using System.Collections.Generic;
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
                var qbeUser = new UserPrincipal(context);

                // create your principal searcher passing in the QBE principal    
                var srch = new PrincipalSearcher(qbeUser);

                // find all matches
                foreach (var userPrincipal in srch.FindAll())
                {
                    var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userPrincipal.SamAccountName);

                    if (!user.IsAccountLockedOut()) continue;

                    lockedUsers.Add(accountTools.GetUserAccountDetails(user.UserPrincipalName));
                    
                }

                lockedUsers.Sort();

                return lockedUsers;
            }
        }
    }
}
