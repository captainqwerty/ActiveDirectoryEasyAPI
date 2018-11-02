using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryTools.Interfaces
{
    public interface IOrganisationalUnit
    {
        string Name { get; set; }
        string DistinguishedName { get; set; }
    }
}