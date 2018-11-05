# ActiveDirectoryTools

This library gives quick to use methods for Active Directory covering tools to manage accounts, groups and audit tasks.  The API can only perform these tasks if the script / program executing the API is ran from an account with permissions to perform the task being used.

README is still no where near done... Currently it's just notes...

## Features

### Audit Tasks Setup and Usage

Creating an instance of the AuditTtasks class.
```
var auditTasks = new AuditTasks();
```

DoesOrganisationalUnitExist(string organisationalUnit)

Using this method and supplying the distinguised name of the organisational unit will return a booleon for if it exists or not.

```
var ouExists = auditTasks.DoesOrganisationalUnitExist("OU=Test Ou,DC=gen2training,DC=co,DC=uk");
Console.WriteLine($"OU Exists: {ouExists}")

// If the Ou exists it will return true, if not it will return false.
```

GetAllLockedOutAccounts()

Returns a UserAccount object for each locked account. 

```
var lockedOutAccounts = auditTasks.GetAllLockedOutAccounts();

foreach (var lockedAccount in lockedOutAccounts)
{
    Console.WriteLine(lockedAccount.Username);
}
```

### User Account Tasks

Creating an instance of the UserAccountTasks class.
```
var userAccountTasks = new UserAccountTasks();
```

GetUserAccountDetails(string username)

For the supplied username, if the account is found a UserAccount model is returned with populated values based on the information from the account.

```
var user = userAccountTasks.GetUserAccountDetails("antony.bragg");

Console.WriteLine($"{user.FirstName} {user.LastName}'s locked out status is: {user.LockedOut}");

// Result: "Antony Bragg's locked out statis is: False"
```     

UnlockAccount(string username)

Supplied account will be unlocked. 

```
userAccountTasks.UnlockAccount("antony.bragg");
```

SetUsersPassword(string username, string password) or SetUsersPassword(string username, string password, bool expireNow) 

Supplying a username and a new password will reset the users password.  If no bool is supplied as a third property the account password will not be immedietly expired, if true is supplied the account password will be reset and expired forcing the user to set a new password at log on.

```
userAccountTasks.SetUsersPassword("antony.bragg", "DemoPassword123"); // Resets password
userAccountTasks.SetUsersPassword("antony.bragg", "DemoPassword123", true); // Resets password and expires password

```

MoveToOrganisationalUnit(string username, string newOrganisationalUnit)

The supplied user account will be moved to the given organisational unit if it exists.  The organisational unit's distinguised name is required.

```
userAccountTasks.MoveToOrganisationalUnit("antony.bragg", "OU=Disabled Accounts,DC=gen2training,DC=co,DC=uk");
```
GetLastLogOn(string username)

Not recommended for use yet.

GetThumbnailPhoto(string username)

Returns the users thumbnail photo in a byte format.  In the next version of the software this will be exportable in a range of formats.

```
var photo = userAccountTasks.GetThumbnailPhoto("antony.bragg");
```

### Group Account Tasks

Creating an instance of the GroupAccountTasks class.

```
var groupAccountTasks = new GroupAccountTasks();
```

RemoveUserFromGroup(string username, string groupName)

```
groupAccountTasks.RemoveUserFromGroup("antony.bragg", "Domain Admins");
```

AddUsertoGroup(string username, string groupName)

```
groupAccountTasks.AddUsertoGroup("antony.bragg", "Domain Admins");
```

GetGroupMembers(string groupName)

```
var membersOfGroup = groupAccountTasks.GetGroupMembers("Domain Admins");

foreach (var user in membersOfGroup)
{
    Console.WriteLine($"{user.FirstName} {user.LastName}");
}
```

GetGroupDetails(string groupName)

```
var group = groupAccountTasks.GetGroupDetails("Domain Admins");
            
Console.WriteLine(group.Name);
Console.WriteLine(group.Description);
foreach (var user in group.GroupMembers)
{
                Console.WriteLine($"{user.Username}");
}
```

### Prerequisites

[.NET 3.5 or above](https://www.microsoft.com/net/download/dotnet-framework-runtime)

### Installing

Installation from NuGet

```
PM> Install-Package CaptainQwerty.ActiveDirectoryEasyAPI -Version 1.0.0
```

## Authors

* **[captainqwerty](https://github.com/captainqwerty)** - *Initial work*

## Acknowledgements

* Icon made by [Freepik](https://www.freepik.com/) from [www.flaticon.com](https://www.flaticon.com) and is licensed by [CC 3.0 BY](http://creativecommons.org/licenses/by/3.0/)
