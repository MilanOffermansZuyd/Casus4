using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    public class PayedContact : Contact
    {
        public bool GetsPayed { get; set; }
        public PayedContact(int? id, string firstName, string lastName, byte[] picture, int distanceBetween, Location location, string description, string extrainformation, bool getPayed) 
            : base(id, firstName, lastName, picture, distanceBetween, location, description, extrainformation)
        {

        }
    }
}
