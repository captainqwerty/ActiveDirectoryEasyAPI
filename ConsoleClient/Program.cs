using System;
using ActiveDirectoryTools;
using ActiveDirectoryTools.Models;

namespace ConsoleClient
{
    internal class Program
    {
        private static void Main()
        {
            // Used to sample and test code.
            var auditTasks = new AuditTasks();
            var userAccountTasks = new UserAccountTasks();
            var groupAccountTasks = new GroupAccountTasks();

            Console.WriteLine("Username: ");
            var username = Console.ReadLine();

            //var user = userAccountTasks.GetUserAccountDetails("antony.bragg");
            //Console.WriteLine($"{user.FirstName} {user.LastName} {user.LockedOut}");

            //var lockedOutAccounts = userAccountTasks.GetAllLockedOutAccounts();
            //foreach (var lockedAccount in lockedOutAccounts)
            //{
            //    Console.WriteLine(lockedAccount.Username);
            //}

            //userAccountTasks.UnlockAccount("18ttest");
            //Console.WriteLine("Account unlocked");

            //userAccountTasks.SetUsersPassword("18ttest","Bacon123*");
            //Console.WriteLine("Password set");

            //var lastLogon = userAccountTasks.GetLastLogOn(username);
            //Console.WriteLine(lastLogon);

            //var photo = userAccountTasks.GetThumbnailPhoto(username);
            //photo.ExportToDisk(Thumbnail.Format.JPG, "C:\\");
            //Console.WriteLine("Photo saved.");

            //Console.WriteLine(photo.Length);

            //userAccountTasks.MoveToOrganisationalUnit("18Ttest", "OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine("Account moved");

            //groupAccountTasks.RemoveUserFromGroup("18Ttest", "Role - Student");
            //Console.WriteLine("Removed from Group");

            //groupAccountTasks.AddUsertoGroup("18Ttest","Role - UTC Student");
            //Console.WriteLine("Added to Group");

            //var membersOfGroup = groupAccountTasks.GetGroupMembers("Domain Admins");
            //foreach (var user in membersOfGroup)
            //{
            //    Console.WriteLine($"{user.FirstName} {user.LastName}");
            //}

            //var group = groupAccountTasks.GetGroupDetails("Domain Admins");
            //Console.WriteLine(group.Name);
            //Console.WriteLine(group.Description);
            //foreach (var user in group.GroupMembers)
            //{
            //    Console.WriteLine($"{user.Username}");
            //}

            //groupAccountTasks.CreateGroup("Test Group", GroupAccountTasks.GroupType.Security, GroupAccountTasks.GroupScope.Universal, "OU=Security Groups,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine("Group created");

            //groupAccountTasks.RenameGroup("Test Group", "Renamed Test Group");
            //Console.WriteLine("Group renamed");

            //var shouldBeTrue = auditTasks.DoesOrganisationalUnitExist("OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //var shouldBeFalse = auditTasks.DoesOrganisationalUnitExist("OU=Managers,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine($"Should be True: {shouldBeTrue}");
            //Console.WriteLine($"Should be False: {shouldBeFalse}");

            Console.ReadLine();
        }
    }
}