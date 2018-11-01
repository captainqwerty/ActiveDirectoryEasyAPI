using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class GroupAccountTasks
    {
        public Group GetGroupDetails(string groupName)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            using (var groupResult = GroupPrincipal.FindByIdentity(principalContext, groupName))
            {
                if (groupResult == null) return null;

                var group = new Group
                {
                    Name = groupResult.Name,
                    Description = groupResult.Description
                };

                return group;
            }
        }

        public IEnumerable<UserAccount> GetGroupMembers(string groupName)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            using (var groupResult = GroupPrincipal.FindByIdentity(principalContext, groupName))
            {
                if (groupResult == null) return null;

                var groupResultMembers = groupResult.GetMembers();

                var groupMembers = new List<UserAccount>();
                var userAccountTasks = new UserAccountTasks();

                foreach (var user in groupResultMembers)
                {
                    groupMembers.Add(userAccountTasks.GetUserAccountDetails(user.SamAccountName));
                }

                return groupMembers;
            }
        }

        public void AddUsertoGroup(string username, string groupName)
        {
            // Add user to a group
        }

        public void RemoveUserFromGroup(string username, string groupName)
        {
            // Remove user from a group
        }
    }
}
