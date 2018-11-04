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

Get Last Logon

Get Thumbnail Photo

Move to Organisational Unit

### Group Account Tasks

Remove User from Group

Add User to Group

Get Group Members

Get Group Details

### Prerequisites

[.NET 3.5 or above](https://www.microsoft.com/net/download/dotnet-framework-runtime)

### Installing

A step by step series of examples that tell you how to get a development env running

Installation from NuGet

```
Example... Once it is on NuGet...
```

## Authors

* **[captainqwerty](https://github.com/captainqwerty)** - *Initial work*

## Acknowledgements

* Icon made by [Freepik](https://www.freepik.com/) from [www.flaticon.com](https://www.flaticon.com) and is licensed by [CC 3.0 BY](http://creativecommons.org/licenses/by/3.0/)
