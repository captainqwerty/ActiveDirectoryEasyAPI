using ActiveDirectoryTools.Interfaces;

namespace ActiveDirectoryTools.Models
{
    public class OrganisationalUnit : IOrganisationalUnit
    {
        public string Name { get; set; }
        public string DistinguishedName { get; set; }
    }
}