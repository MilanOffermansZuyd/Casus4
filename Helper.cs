using Casus4;

public class Helper : Contact
{
    public Helper(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, bool naked)
            : base(id, firstName, lastName, picture, 0, location, description, extraInformation, naked)
    {
    }
}
