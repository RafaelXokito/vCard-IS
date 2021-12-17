using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public decimal Old_Balance { get; set; }
        public decimal New_Balance { get; set; }
        public string Payment_Type { get; set; }
        public string Payment_Reference { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
    }
}