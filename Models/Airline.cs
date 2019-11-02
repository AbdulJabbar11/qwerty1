using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace qwerty1.Models
{
    public partial class Airline
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(6)]

        public string Name { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public string ShortCode { get; set; }
    }
}
