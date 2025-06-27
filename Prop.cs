using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    class Prop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Prop(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Prop(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
