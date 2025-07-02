namespace Casus4
{
    public class MakeUpArtist: PayedContact
    {
        public bool GetsResourcesPaid { get; set; }
        public MakeUpArtist(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, bool getsPayed, bool getsResourcesPaid)
            : base(id, firstName, lastName, picture, 0, location, description, extraInformation, getsPayed)
        {
            GetsResourcesPaid = getsResourcesPaid;
        }
    }
}
