using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepGenericRepositoryPatternWebApiCore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
    }
}
