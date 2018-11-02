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

            //userAccountTasks.MoveToOrganisationalUnit("18Ttest", "OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
            //Console.WriteLine("Account moved");
            //Console.ReadLine();

            groupAccountTasks.RemoveUserFromGroup("18Ttest", "Role - Student");
            Console.WriteLine("Removed from Group");

            groupAccountTasks.AddUsertoGroup("18Ttest","Role - UTC Student");
            Console.WriteLine("Added to Group");

            Console.ReadLine();
        }
    }
}