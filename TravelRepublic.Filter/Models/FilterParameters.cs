using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRepublic.Filter.Models
{
    public class FilterParameters
    {
        public DateTime DepartureDate { get; set; }
        public TimeSpan? GroundTime { get; set; }

        public FilterParameters()
        {
            DepartureDate = DateTime.Now;
            GroundTime = null;
        }
    }
}
