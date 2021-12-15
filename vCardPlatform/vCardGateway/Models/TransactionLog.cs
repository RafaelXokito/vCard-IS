using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardGateway.Models
{
    public class TransactionLog
    {
        public int Id { get; set; }
        public string FromUser { get; set; }
        public string FromEntity { get; set; }
        public string ToUser { get; set; }
        public string ToEntity { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public decimal OldBalance { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}