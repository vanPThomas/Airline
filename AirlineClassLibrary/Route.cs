using System;

namespace AirlineClassLibrary
{
    public class Route
    {
        public string Departure { get; set; }
        public string Destination { get; set;}
        public List <string> Stops { get; set; } = new List<string>();
        public double Distance;

        public bool Equals(Route obj)
        {
            bool isEqual = false;
            if (Departure == obj.Departure && Destination == obj.Destination && Distance == obj.Distance)
                isEqual = true;

            return isEqual;
        }
    }
}
