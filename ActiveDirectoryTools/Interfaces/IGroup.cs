using System.Collections.Generic;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools.Interfaces
{
    internal interface IGroup
    {
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<UserAccount> GroupMembers { get; set; }
    }
}
