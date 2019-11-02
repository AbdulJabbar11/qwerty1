using System;
using System.Collections.Generic;

namespace qwerty1.Models
{
    public partial class Flight
    {
        public int Id { get; set; }
        public int? AirlineId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
    }
}
