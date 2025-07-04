using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    public abstract class PayedContact : Contact
    {
        public bool GetsPayed { get; set; }
        public PayedContact(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extrainformation, bool getPayed) 
            : base(id, firstName, lastName, picture, location, description, extrainformation)
        {

        }

        DAL dal = new DAL();
        public void Add(Model model)
        {
            throw new NotImplementedException();
        }
        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void Edit(Model model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
