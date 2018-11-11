using System.Collections.Generic;
using ActiveDirectoryTools.Interfaces;
using ActiveDirectoryTools.Models;

namespace ActiveDirectoryTools
{
    public class Group : IGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserAccount> GroupMembers { get; set; }
    }
}