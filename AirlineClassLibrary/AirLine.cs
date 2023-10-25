namespace AirlineClassLibrary
{
    public class AirLine
    {

        List <Flight> flights = new List <Flight> ();
        private static AirLine instance;
        private AirLine()
        {
            
        }

        public static AirLine GetInstance()
        {
            if (instance != null)
                return instance;
            else
            {
                instance = new AirLine();
                return instance;
            }
        }

        public event EventHandler<FlightEventArgs> FlightEvent;

        public void CreateDataFromFile()
        {
            using (StreamReader sr = File.OpenText("airlineData.txt"))
            {
                string input = null;
                string[] inputArray;
                while ((input = sr.ReadLine()) != null)
                {
                    inputArray = input.Split(",");
                    int check;
                    if (Int32.TryParse(inputArray[2], out check))
                    {
                        Flight flight = new Flight ();
                        flight.FlightNumber = Int32.Parse(inputArray[0]);
                        flight.FlightDate = DateTime.Parse(inputArray[1]);
                        flight.SeatsSold = check;
                        flight.AirPlane.Name = inputArray[3];
                        flight.AirPlane.FuelCostPerPassPerHundredKM = Int32.Parse(inputArray[4]);
                        flight.AirPlane.NumberOfSeats = Int32.Parse(inputArray[5]);
                        flight.AirPlane.Speed = Int32.Parse(inputArray[6]);
                        flight.Route.Departure = inputArray[7];
                        flight.Route.Destination = inputArray[8];
                        flight.Route.Distance = Int32.Parse(inputArray[9]);
                        if (inputArray.Length > 10)
                            for(int i = 10; i < inputArray.Length +1; i++)
                                flight.Route.Stops.Add(inputArray[i]);
                            
                        flights.Add (flight);
                        Console.WriteLine("Flight added");
                        if (FlightEvent != null)
                            FlightEvent(this, new FlightEventArgs("FLIGHT DATA CHANGE", flight));
                    }
                }
            }
        }

        public List<Flight> GetFlights() { return flights; }
    }
}
