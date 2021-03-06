using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardPlatform.Models
{
    public class GeneralLog
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Entity { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public long ResponseTime { get; set; }
    }
}