using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCarWash.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        //[Required(ErrorMessage = "You must provide a phone number")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        public string  Address { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Client()
        {
            Orders = new List<Order>();
        }
    }
}