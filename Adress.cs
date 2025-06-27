using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    class Adress: Location
    {
        public Adress(int? id, string street, string houseNumber, string postalCode, string city, string country)
            : base(id, street, houseNumber, postalCode, city, country)
        {
        }
    }
}
