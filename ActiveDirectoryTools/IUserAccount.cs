using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryTools
{
    internal interface IUserAccount
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
    }
}
