using System;
using System.Collections.Generic;
using ActiveDirectoryTools;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var auditTool = new Audit();

            var lockedOutAccounts = auditTool.GetAllLockedOutAccounts("OU=Lillyhall,OU=Students,OU=User Accounts,DC=gen2training,DC=co,DC=uk");

            foreach (var user in lockedOutAccounts)
            {
                Console.WriteLine(user.Username);
            }

            Console.ReadLine();
        }
    }
}