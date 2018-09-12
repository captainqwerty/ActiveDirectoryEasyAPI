using System;
using System.Collections.Generic;
using ActiveDirectoryTools;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Account Details Collection");
            Console.Write("Username: ");
            var username = Console.ReadLine();

            var accountTools = new Account();

            var userAccount = accountTools.GetUserAccountDetails(username);

            Console.WriteLine($"Name: {userAccount.FirstName} {userAccount.LastName}");
            Console.WriteLine("Locked out:" + userAccount.LockedOut);
            Console.WriteLine($"Last Logon Time: {userAccount.LastLogonDateTime}");
            Console.WriteLine($"When Created: {userAccount.WhenCreated}");

            Console.ReadLine();
        }
    }
}