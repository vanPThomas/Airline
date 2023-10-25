namespace AirlineClassLibrary
{
    public  class CateringEventArgs
    {
        public CateringEventArgs(string message, List<Flight> flights)
        {
            this.message = message;
            ShowMessage();
            this.flights = flights;
        }

        string message = "BOE";

        public void ShowMessage()
        {
            Console.WriteLine(message);
        }

        public List<Flight> flights { get; set; }
    }
}
