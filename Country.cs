using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country(int? id, string name)
        {
            Id = id ?? 0;
            Name = name;
        }
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
