using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardPlatform.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public decimal MaxLimit { get; set; }
        public decimal EarningPercentage { get; set; }
        public Authentication Authentication { get; set; }
    }
}