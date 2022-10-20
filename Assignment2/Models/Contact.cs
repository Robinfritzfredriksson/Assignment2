using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class Contact // Class där vi kan hämta ifrån och hur vi vill att en contact ska se ut och innehålla
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;



        public string FullName => $"{FirstName} {LastName}";
    }
}
