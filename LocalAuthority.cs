using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    class LocalAuthority: Location
    {
        public LocalAuthority(int? id, string street, string houseNumber, string postalCode, string city, string country)
            : base(id, street, houseNumber, postalCode, city, country)
        {
        }
    }
}
