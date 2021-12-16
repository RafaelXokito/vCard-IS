using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCardPlatform.Models
{
    public class Filter
    {
        public string FromUser { get; set; }
        public string Type { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
