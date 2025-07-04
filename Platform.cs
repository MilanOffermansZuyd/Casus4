using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus4
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }

        public Platform(int id, string name, string information)
        {
            Id = id;
            Name = name;
            Information = information;
        }

        public void AddPlatform()
        {
            throw new NotImplementedException();
        }

        public void RemovePlatform()
        {
            throw new NotImplementedException();
        }
    }
}
