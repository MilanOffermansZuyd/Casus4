using Casus4;

public class Helper : Contact
{
    public Helper(int? id, string firstName, string lastName, byte[] picture, int distanceBetween, Location location, string rol, string description, string extrainformation, bool naked)
        : base(id, firstName, lastName, picture, distanceBetween,  location, rol, description, extrainformation, naked)
    {
    }
}
