using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo2
{
    class Station
    {
        public int number { get; set; }

        public override string ToString()
        {
            return "\tnuméro station : " + number + "\n";
        }

    }
}
