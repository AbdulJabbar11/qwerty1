using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace qwerty1.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
