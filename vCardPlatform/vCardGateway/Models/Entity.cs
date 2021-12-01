using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vCardGateway.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public int MaxLimit { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }
    }

    public enum CategoryType
    {
        Credit,
        Debit
    }
}