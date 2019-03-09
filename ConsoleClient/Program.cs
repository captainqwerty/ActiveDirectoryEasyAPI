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

            // SEEMS TO BE WORKING

            //var user = userAccountTasks.GetUserAccountDetails(username);
            //Console.WriteLine($"{user.FirstName} {user.LastName} {user.LockedOut}");

            //userAccountTasks.UnlockAccount(username);
            //Console.WriteLine("Account unlocked");

            //Console.WriteLine("Enter a new password:");
            //var password = Console.ReadLine();
            //userAccountTasks.SetUsersPassword(username,password);
            //Console.WriteLine("Password set");

            //var photo = userAccountTasks.GetThumbnailPhoto(username);
            //photo.ExportToDisk(Thumbnail.Format.JPG, "C:\\");
            //Console.WriteLine(photo.Length);
            //Console.WriteLine("Photo saved.");

            //var shouldBeTrue = auditTasks.DoesDistinguishedNameExist("OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine($"Should be True: {shouldBeTrue}");
            //var shouldBeFalse = auditTasks.DoesDistinguishedNameExist("OU=Managers,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine($"Should be False: {shouldBeFalse}");
            //var shouldFail = auditTasks.DoesDistinguishedNameExist("Bacon");
            //Console.WriteLine($"Should fail: {shouldFail}");

            //userAccountTasks.MoveToOrganisationalUnit(username, "OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine("Account moved");











            // NEEDS FURTHER TESTING

            //var lockedOutAccounts = userAccountTasks.GetAllLockedOutAccounts();
            //foreach (var lockedAccount in lockedOutAccounts)
            //{
            //    Console.WriteLine(lockedAccount.Username);
            //}



            // DOES NOT WORK

            //var lastLogon = userAccountTasks.GetLastLogOn(username);
            //Console.WriteLine(lastLogon);



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





            Console.ReadLine();
        }
    }
}