using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryTools
{
    internal interface IGroup
    {
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<UserAccount> GroupMembers { get; set; }
    }
}
