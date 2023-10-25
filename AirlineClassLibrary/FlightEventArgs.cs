namespace AirlineClassLibrary
{
    public class FlightEventArgs
    {
        public FlightEventArgs(string message, Flight flight)
        {
            this.flight = flight;
            this.message = message;
            ShowMessage();
        }
        public Flight flight { get; set; }
        string message = "BOE";

        public void ShowMessage()
        {
            Console.WriteLine(message);
        }

    }
}
