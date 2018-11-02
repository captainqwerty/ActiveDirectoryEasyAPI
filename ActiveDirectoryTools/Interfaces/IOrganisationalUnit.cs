namespace ActiveDirectoryTools.Interfaces
{
    public interface IOrganisationalUnit
    {
        string Name { get; set; }
        string DistinguishedName { get; set; }
    }
}