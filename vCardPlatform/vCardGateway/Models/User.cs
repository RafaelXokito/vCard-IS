using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardGateway.Models
{
    public class User
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
        public decimal MaximumLimit { get; set; }
        public decimal Balance { get; set; }
        public string Photo { get; set; }
    }
}