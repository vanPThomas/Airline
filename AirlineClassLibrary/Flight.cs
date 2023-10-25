namespace AirlineClassLibrary
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public Route Route { get; set; } = new Route();
        public double SeatsSold { get; set; }
        public AirPlane AirPlane { get; set; } = new AirPlane();
        public List<CateringOrder> CateringOrders { get; set;} = new List<CateringOrder>();

    }
}