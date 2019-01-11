using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class GroupAccountTasks
    {
        public enum GroupType
        {
            Security, Distribution
        }

        public enum GroupScope
        {
            Global, Local, Universal
        }

        public Group GetGroupDetails(string groupName)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            using (var groupResult = GroupPrincipal.FindByIdentity(principalContext, groupName))
            {
                if (groupResult == null) throw new NoMatchingPrincipalException();

                var group = new Group
                {
                    Name = groupResult.Name,
                    Description = groupResult.Description
                };

                group.GroupMembers = GetGroupMembers(groupName);

                return group;
            }
        }

        public IEnumerable<UserAccount> GetGroupMembers(string groupName)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            using (var groupResult = GroupPrincipal.FindByIdentity(principalContext, groupName))
            {
                if (groupResult == null) throw new NoMatchingPrincipalException();

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
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain))
                {
                    var group = GroupPrincipal.FindByIdentity(pc, groupName);
                    group.Members.Add(pc, IdentityType.SamAccountName, username);
                    group.Save();
                }
            }
            catch (DirectoryServicesCOMException e)
            {
                // Error
            }
        }

        public void RemoveUserFromGroup(string username, string groupName)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    var group = GroupPrincipal.FindByIdentity(context, groupName);
                    group.Members.Remove(context, IdentityType.SamAccountName, username);
                    group.Save();
                }
            }
            catch (DirectoryServicesCOMException e)
            {
                // Error
            }
        }

        public void CreateGroup(string groupName, GroupType groupType, GroupScope groupScope, string description = null, string organisationUnit = null, IEnumerable<UserAccount> members = null)
        {
            System.DirectoryServices.AccountManagement.GroupScope scope;

            switch (groupScope)
            {
                case GroupScope.Local:
                    scope = System.DirectoryServices.AccountManagement.GroupScope.Local;
                    break;
                case GroupScope.Global:
                    scope = System.DirectoryServices.AccountManagement.GroupScope.Global;
                    break;
                case GroupScope.Universal:
                    scope = System.DirectoryServices.AccountManagement.GroupScope.Universal;
                    break;
                default:
                    scope = System.DirectoryServices.AccountManagement.GroupScope.Universal;
                    break;
            }

            bool isSecurityGroup;
            switch (groupType)
            {
                case GroupType.Distribution:
                    isSecurityGroup = false;
                    break;
                case GroupType.Security:
                    isSecurityGroup = true;
                    break;
                default:
                    isSecurityGroup = false;
                    break;
            }

            // NEED TO CHECK IF OU IS A DISTINGUISED NAME THAT EXISTS
            var context = organisationUnit != null ? new PrincipalContext(ContextType.Domain, null, organisationUnit) : new PrincipalContext(ContextType.Domain);

            using (context)
            using (var groupPrincipal = new GroupPrincipal(context))
            {
                groupPrincipal.Name = groupName;
                groupPrincipal.SamAccountName = groupName;
                groupPrincipal.Description = description;
                groupPrincipal.IsSecurityGroup = isSecurityGroup;
                groupPrincipal.GroupScope = scope;
                groupPrincipal.Save();
            }

            if (members == null) return;
            foreach (var user in members)
            {
                AddUsertoGroup(user.Username, groupName);
            }
        }

        public void ConvertGroup(string groupName, GroupScope groupScope, GroupType groupType)
        {
            
        }

        public void RenameGroup(string groupName, string newGroupName)
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            using (var group = GroupPrincipal.FindByIdentity(context, groupName))
            {
                var groupEntry = (DirectoryEntry)group.GetUnderlyingObject();
                groupEntry.Rename(newGroupName);
                groupEntry.CommitChanges();
            }
        }
    }
}
