namespace ActiveDirectoryTools.Interfaces
{
    internal interface IUserAccount
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
    }
}
