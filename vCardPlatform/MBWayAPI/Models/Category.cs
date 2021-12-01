using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBWayAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
    }
}