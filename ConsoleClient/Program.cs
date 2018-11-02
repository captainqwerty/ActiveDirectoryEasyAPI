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

            userAccountTasks.MoveToOrganisationalUnit("terry.testaccount", "OU=Sales,OU=Lillyhall,OU=Staff,OU=User Accounts,DC=gen2training,DC=co,DC=uk");
            Console.WriteLine("Account moved");
            Console.ReadLine();
        }
    }
}