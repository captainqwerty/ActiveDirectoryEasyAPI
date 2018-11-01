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
            var auditTool = new AuditTasks();

            var doesItExist = auditTool.DoesOrganisationalUnitExist("OU=Lillyhall,OU=Students,OU=User Accounts,DC=gen2training,DC=co,DC=uk");

            Console.Write(doesItExist);

            Console.ReadKey();
        }
    }
}