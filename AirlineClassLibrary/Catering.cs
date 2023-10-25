namespace AirlineClassLibrary
{
    public class Catering
    {

        public event EventHandler<CateringEventArgs> CateringEvent;
        public Dictionary<string, List<string>> AirportsToFoodOrders { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> CreateFoodReport(List<Flight> flights)
        {
            Dictionary<string, List<string>> AirportsToFoodOrders = new Dictionary<string, List<string>>();

            foreach (Flight flight in flights)
            {
                if(AirportsToFoodOrders.ContainsKey(flight.Route.Departure))
                    for(int i = 0; i < flight.CateringOrders.Count; i++)
                        AirportsToFoodOrders[flight.Route.Departure].Add(flight.CateringOrders[i].foodName);
                else
                {
                    AirportsToFoodOrders.Add(flight.Route.Departure, new List<string>());
                    for (int i = 0; i < flight.CateringOrders.Count; i++)
                        AirportsToFoodOrders[flight.Route.Departure].Add(flight.CateringOrders[i].foodName);
                }
            }
            Console.WriteLine("Catering Orders Updated");
            if (CateringEvent != null)
                CateringEvent(this, new CateringEventArgs("Catering Changed, updating Finance report!", flights));
            return AirportsToFoodOrders;
        }

        public void CateringArgsMessage(object source, FlightEventArgs args)
        {
            AirportsToFoodOrders = CreateFoodReport(args.flights);
        }
    }
}
