using System;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.DataContracts;
using TravelRepublic.Filter.Models;

namespace TravelRepublic.Filter
{
    public class FlightFilterBuilder : IFlightFilterBuilder
    {
        private FilterParameters _filterParams;
        public FlightFilterBuilder() : this(new FilterParameters()) { }
        public FlightFilterBuilder(FilterParameters filterParams)
        {
            _filterParams = filterParams;
        }

        ////SetDeptDateFilter sets the dept filter used to override the default params at any point in time.
        public FlightFilterBuilder SetDeptDateFilter(DateTime departtureDate)
        {
            _filterParams.DepartureDate = departtureDate;
            return this;
        }

        ////SetGroundTime sets the groundTime filter value
        public FlightFilterBuilder SetGroundTime(TimeSpan? groundTimeInHrs)
        {
            _filterParams.GroundTime = groundTimeInHrs;
            return this;
        }

        //ExecFilter execs the filter
        public IEnumerable<Flight> ExecFilter(IEnumerable<Flight> lstFlights)
        {
            var filteredList = new List<Flight>();

            DepartureFilter(lstFlights.ToList(), filteredList);
            GroundTimeFilter(filteredList);

            return filteredList;
        }

        private void GroundTimeFilter(List<Flight> responseList)
        {
            if (_filterParams.GroundTime != null)
            {
                var filteredList = new List<Flight>();
                foreach (var item in responseList)
                {
                    var flight = new Flight() { Segments = new List<Segment>() };

                    DateTime? prevArrivalTime = null;
                    foreach (var segment in item.Segments)
                    {
                        if (prevArrivalTime != null)
                        {
                            var ts = segment.DepartureDate - prevArrivalTime;
                            if (ts >= _filterParams.GroundTime)
                            {
                                flight.Segments.Add(segment);
                            }
                        }
                        else
                        {
                            prevArrivalTime = segment.ArrivalDate;
                        }
                    }

                    if (flight.Segments.Count > 0)
                    {
                        filteredList.Add(flight);
                    }
                }

                responseList.Clear();
                responseList.AddRange(filteredList.ToList());
            }
        }

        private void DepartureFilter(IEnumerable<Flight> lstFlights, IList<Flight> responseList)
        {
            if (_filterParams.DepartureDate != null)
            {
                foreach (var item in lstFlights)
                {
                    var flight = new Flight() { Segments = new List<Segment>() };
                    foreach (var segment in item.Segments)
                    {
                        if (segment.ArrivalDate >= _filterParams.DepartureDate)
                        {
                            flight.Segments.Add(segment);
                        }
                    }

                    if (flight.Segments.Count > 0)
                    {
                        responseList.Add(flight);
                    }
                }
            }
        }
    }
}
