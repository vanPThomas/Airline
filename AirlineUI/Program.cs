using AirlineClassLibrary;

namespace AirlineUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirLine airLine = AirLine.GetInstance();
            Catering catering = new Catering();
            Sales sales = new Sales();
            Finance finance = new Finance();

            airLine.FlightEvent += sales.SalesArgsMessage;
            airLine.FlightEvent += finance.FinanceArgsMessage;
            airLine.FlightEvent += catering.CateringArgsMessage;
            catering.CateringEvent += finance.FinanceVSCatering;
            airLine.CreateDataFromFile();

            
        }
    }
}