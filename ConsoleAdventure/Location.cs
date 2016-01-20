using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    public class Location
    {
        public string name;
        public List<Location> locations;
        public List<string> enterText;

        public Location()
        {

        }

        public Location(string name, List<Location> locations)
        {
            this.name = name;
            this.locations = locations;
        }
    }
}
