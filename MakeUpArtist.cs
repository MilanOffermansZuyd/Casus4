using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    class MakeUpArtist: Contact
    {
        public MakeUpArtist(int? id, string firstName, string lastName, byte[] picture, Location location, string description, string extraInformation, bool naked)
            : base(id, firstName, lastName, picture, 0, location, description, extraInformation, naked)
        {
        }
    }
}
