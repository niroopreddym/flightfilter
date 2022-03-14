using System;
using System.Collections.Generic;
using TravelRepublic.DataContracts;
using TravelRepublic.Filter;

namespace TravelRepublic.FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FlightBuilder fb = new FlightBuilder();
            var lstFlights = fb.GetFlights();

            IFlightFilterBuilder filter = new FlightFilterBuilder();
            filter.SetGroundTime(TimeSpan.FromHours(2));
            var res = filter.ExecFilter(lstFlights);
            StdOut(res);

            Console.ReadKey();
        }

        static void StdOut(IEnumerable<Flight> response)
        {
            foreach (var item in response)
            {
                foreach (var segment in item.Segments)
                {
                    Console.WriteLine("departureTime:" + segment.DepartureDate + " arrivalTime:" + segment.ArrivalDate);
                }
            }
        }
    }
}
