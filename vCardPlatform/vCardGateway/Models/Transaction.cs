using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardGateway.Models
{
    public enum TransactionType { D, C }
    public class Transaction
    {
        public long Id { get; set; }
        public string FromUser { get; set; }
        public string FromEntity { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
        public string Payment_Type { get; set; }
        public string Payment_Reference { get; set; }
        public decimal Pair_Transaction { get; set; }
        public decimal Pair_Vcard { get; set; }
        public string Description { get; set; }
        public decimal Custom_Options { get; set; }
        public decimal Custom_Data { get; set; }
        public decimal Created_At { get; set; }
        public decimal Updated_At { get; set; }
        public decimal Deleted_At { get; set; }
        public int Category { get; set; }
    }
}