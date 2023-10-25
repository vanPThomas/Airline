namespace AirlineClassLibrary
{
    public class Finance
    {
        public Dictionary<int, double> fuelCostReport { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>> cateringCostReport { get; set; } = new Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>>();
        List<Flight> Flights = new List<Flight>();

        public Dictionary<int, double> CalculateFuelCost(List<Flight> flights)
        {
            Dictionary<int, double> fuelCostPerYear = new Dictionary<int, double>();
            List<int> years = GetYears(flights);
            foreach (int year in years)
            {
                double totalCost = 0;
                foreach (Flight flight in flights)
                    if (flight.FlightDate.Year == year)
                        totalCost += flight.Route.Distance * flight.SeatsSold * (flight.AirPlane.FuelCostPerPassPerHundredKM/100);
                fuelCostPerYear.Add(year, totalCost);
            }
            Console.WriteLine("Fuel Report updated");
            return fuelCostPerYear;
        }
        public Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>> CalculateCatering(List<Flight> flights)
        {
            Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>> CateringCost = new Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>>();
            List<int> years = GetYears(flights);
            List<int> months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<string> airports = GetAirports(flights);

            Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>> yearDict = new Dictionary<int, Dictionary<int, Dictionary<string, List<double>>>>();
            foreach (int year in years)
            {
                Dictionary<int, Dictionary<string, List<double>>> monthDict = new Dictionary<int, Dictionary<string, List<double>>>();
                foreach (int month in months)
                {
                    Dictionary<string, List<double>> airportDict = new Dictionary<string, List<double>>();
                    foreach (string airport in airports)
                    {
                         double numberOfFlights = 0;
                         double costOfCatering = 0;
                        foreach (Flight flight in flights)
                        {
                            if(flight.FlightDate.Year == year && flight.FlightDate.Month == month && flight.Route.Departure == airport)
                            {
                                numberOfFlights++;
                                foreach(CateringOrder cateringOrder in flight.CateringOrders)
                                    costOfCatering += cateringOrder.FoodCost;
                            }
                        }
                        airportDict.Add(airport, new List<double> { costOfCatering, numberOfFlights});
                    }
                    monthDict.Add(month, airportDict);
                }
                yearDict.Add(year, monthDict);
            }
            Console.WriteLine("Catering Finance Report updated");
            return CateringCost;
        }

        private static List<int> GetYears(List<Flight> flights)
        {
            List<int> years = new List<int>();
            foreach (Flight flight in flights)
                if (!years.Contains(flight.FlightDate.Year))
                    years.Add(flight.FlightDate.Year);
            return years;
        }
        private static List<string> GetAirports(List<Flight> flights)
        {
            List<string> airports = new List<string>();
            foreach (Flight flight in flights)
                if (!airports.Contains(flight.Route.Departure))
                    airports.Add(flight.Route.Departure);
            return airports;
        }

        public void FinanceArgsMessage(object source, FlightEventArgs args)
        {
            Flights.Add(args.flight);
            fuelCostReport = CalculateFuelCost(Flights);
        }
        public void FinanceVSCatering(object source, CateringEventArgs args)
        {
            AirLine airLine = AirLine.GetInstance();
            cateringCostReport = CalculateCatering(args.flights);
        }

    }
}
