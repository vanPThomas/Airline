namespace AirlineClassLibrary
{
    public class Sales
    {
        public Dictionary<Route, List<double>> SalesReport { get; set; } = new Dictionary<Route, List<double>>();

        public Dictionary<Route, List<double>> CreateReport(List<Flight> flights)
        {
            Dictionary<Route, List<double>> RoutePopularity = new Dictionary<Route, List<double>>();
            Dictionary<Route, List<Flight>> RouteToFlights = new Dictionary<Route, List<Flight>>();

            foreach (Flight flight in flights)
            {
                bool containsRoute = false;
                Route xRoute = new Route();
                foreach(Route route in RouteToFlights.Keys)
                    if(route.Equals(flight.Route))
                    {
                        containsRoute = true;
                        xRoute = route;
                    }

                if(containsRoute)
                    RouteToFlights[xRoute].Add(flight);
                else
                {
                    RouteToFlights.Add(flight.Route, new List<Flight>());
                    RouteToFlights[flight.Route].Add(flight);
                }
            }
            foreach(Route route in RouteToFlights.Keys)
            {
                double popularity = 0;
                foreach(Flight flight in RouteToFlights[route])
                {
                    double flightPopularity = (flight.SeatsSold / flight.AirPlane.NumberOfSeats) * 100;
                    if(popularity == 0)
                        popularity = flightPopularity;
                    else
                        popularity = (flightPopularity + popularity)/2;
                }
                List <double> RouteInfo = new List<double>();
                RouteInfo.Add(popularity);
                RouteInfo.Add(RouteToFlights[route].Count);
                RoutePopularity.Add(route, RouteInfo);
            }
            Console.WriteLine("Sales Report Updated");
            return RoutePopularity;
        }

        public Dictionary<Route, List<double>> CreateReport(List<Flight> flights, double limit)
        {
            Dictionary<Route, List<double>> FullRoutePopularity = CreateReport(flights);
            Dictionary<Route, List<double>> RoutePopularity = new Dictionary<Route, List<double>>();

            foreach(Route route in FullRoutePopularity.Keys)
            {
                if (FullRoutePopularity[route][0] < limit)
                    RoutePopularity.Add(route, FullRoutePopularity[route]);
            }
            return RoutePopularity;
        }

        public void SalesArgsMessage (object source, FlightEventArgs args)
        {
            SalesReport = CreateReport(args.flights);
        }
    }
}
