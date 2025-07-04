using Casus4;

public class Helper : Contact
{
    public Helper(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation)
            : base(id, firstName, lastName, picture, location, description, extraInformation)
    {
    }
    public Helper(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, Platform platform)
    : base(id, firstName, lastName, picture, platform, location, description, extraInformation)
    {
    }
}
