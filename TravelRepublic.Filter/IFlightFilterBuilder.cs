using System;
using System.Collections.Generic;
using TravelRepublic.DataContracts;

namespace TravelRepublic.Filter
{
    public interface IFlightFilterBuilder
    {
        IEnumerable<Flight> ExecFilter(IEnumerable<Flight> lstFlights);
        FlightFilterBuilder SetDeptDateFilter(DateTime departtureDate);
        FlightFilterBuilder SetGroundTime(TimeSpan? groundTimeInHrs);
    }
}