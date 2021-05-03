using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text.RegularExpressions;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class AuditTasks
    {
        public bool DoesDistinguishedNameExist(string distinguishedName)
        {
            try
            {
                var distinguishedNameExists = DirectoryEntry.Exists("LDAP://" + distinguishedName);
                return distinguishedNameExists;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw new CustomException($"Are you sure that was the correct format for a Distinguished name? Please check: {distinguishedName}");
            }
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

        public static List<UserAccount> GetUsersInOU(string organistionalUnit)
        {
            var users = new List<UserAccount>();

            // create your domain context and define the OU container to search in
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "DOMAINNAME",
                                                        "OU=SomeOU,dc=YourCompany,dc=com");

            // define a "query-by-example" principal - here, we search for a UserPrincipal (user)
            UserPrincipal qbeUser = new UserPrincipal(ctx);

            // create your principal searcher passing in the QBE principal    
            PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

            // find all matches
            foreach (var found in srch.FindAll())
            {
                // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
                //users.Add(found.DisplayName);
            }

            return users;
        }
    }
}
