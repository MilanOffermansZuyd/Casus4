namespace Casus4
{
    public class MakeUpArtist: PayedContact
    {
        public bool GetsResourcesPaid { get; set; }
        public MakeUpArtist(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, bool getsPayed, bool getsResourcesPaid)
            : base(id, firstName, lastName, picture, location, description, extraInformation, getsPayed)
        {
            GetsResourcesPaid = getsResourcesPaid;
        }

        DAL dal = new DAL();
        public void Add(MakeUpArtist makeUpArtist)
        {
            dal.AddMakeUpArtist(makeUpArtist);
        }
        public override void Remove(int id)
        {
            dal.DeleteContact(id);
        }
        public void Edit(Model model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
