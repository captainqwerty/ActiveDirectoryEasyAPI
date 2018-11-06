using System;
using ActiveDirectoryTools;

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

            var user = userAccountTasks.GetUserAccountDetails("antony.bragg");
            Console.WriteLine($"{user.FirstName} {user.LastName} {user.LockedOut}");

            //userAccountTasks.UnlockAccount("18ttest");
            //Console.WriteLine("Account unlocked");

            //userAccountTasks.SetUsersPassword("18ttest","Bacon123*");
            //Console.WriteLine("Password set");

            //var lastLogon = userAccountTasks.GetLastLogOn("Administrator");
            //Console.WriteLine(lastLogon);

            var photo = userAccountTasks.GetThumbnailPhoto("antony.bragg");

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

            //var shouldBeTrue = auditTasks.DoesOrganisationalUnitExist("OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //var shouldBeFalse = auditTasks.DoesOrganisationalUnitExist("OU=Managers,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine($"Should be True: {shouldBeTrue}");
            //Console.WriteLine($"Should be False: {shouldBeFalse}");

            //var lockedOutAccounts = auditTasks.GetAllLockedOutAccounts();
            //foreach (var lockedAccount in lockedOutAccounts)
            //{
            //    Console.WriteLine(lockedAccount.Username);
            //}

            Console.ReadLine();
        }
    }
}