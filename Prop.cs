using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    public class Prop
    {
        DAL dal = new DAL();
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

        public Prop()
        {
            
        }

        public void Add(Prop prop) 
        {
            dal.AddProp(prop);
        }
        public void remove(int id)
        {
            dal.RemoveProp(id);
        }
    }
}
