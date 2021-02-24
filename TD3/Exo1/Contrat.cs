using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo1
{
    class Contrat
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public List<string> cities { get; set; }
        public string country_code { get; set;  }

        public override string ToString()
        {
            string res = "\tNom : " + name
                + "\n\t" + "Nom commercial : " + commercial_name;

            if (cities != null)
            {
                foreach (string city in cities)
                    res += "\n\tVille : " + city;
            }
            return res + "\n";
        }
    }
}
