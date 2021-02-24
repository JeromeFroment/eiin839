using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo3
{
    class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }

        public override string ToString()
        {
            return "\tnom station : " + name + "\n" + 
                "\tnuméro de station : " + number + "\n" +
                "\tadresse : " + address + "\n" +
                "\tnom contrat : " + contractName + "\n";
        }

    }
}