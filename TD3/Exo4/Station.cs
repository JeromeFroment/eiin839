using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Exo4
{
    class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Station closest { get; set; }

        public Position position { get; set; }

        public struct Position
        {
            public Position(double x, double y)
            {
                latitude = x;
                longitude = y;
            }

            public double latitude { get; set;  }
            public double longitude { get; set;  }

            public override string ToString() => $"({latitude}, {longitude})";
        }

        public override string ToString()
        {
            String str = "\tnom station : " + name + "\n" +
                "\tnuméro de station : " + number + "\n" +
                "\tadresse : " + address + "\n" +
                "\tnom contrat : " + contractName + "\n" +
                "\tposition : " + position + "\n";
            if (this.closest != null)
                str += "\nPlus proche station : \n" + this.closest.ToString() + "\n" +
                    "\tDistance : " + (new GeoCoordinate(this.position.latitude, this.position.longitude)).GetDistanceTo(new GeoCoordinate(this.closest.position.latitude, this.closest.position.longitude)) + " m";
            return str;
        }

        public override bool Equals(object obj)
        {
            Station s = (Station)obj;
            if (s.number == this.number)
                return true;
            return false;
        }

        public void GetClosestStation(List<Station> stations)
        {
            Station closest = stations[1];
            var coor1 = new GeoCoordinate(this.position.latitude, this.position.longitude);
            var distance = 0.0;

            if (!stations[0].Equals(this))
                distance = coor1.GetDistanceTo(new GeoCoordinate(stations[0].position.latitude, stations[0].position.longitude));
            else
                distance = coor1.GetDistanceTo(new GeoCoordinate(stations[1].position.latitude, stations[1].position.longitude));

            foreach (Station s in stations)
            {
                if (!s.Equals(this))
                {
                    var coor2 = new GeoCoordinate(s.position.latitude, s.position.longitude);
                    var distanceTemp = coor1.GetDistanceTo(coor2);
                    if (distanceTemp <= distance)
                    {
                        distance = distanceTemp;
                        closest = s;
                    }
                }
            }
            this.closest = closest;
        }

    }
}