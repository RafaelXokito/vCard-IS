using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBWayAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReference { get; set; }
        public int ClassificationId { get; set; }
    }
}