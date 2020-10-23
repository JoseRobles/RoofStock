using System;
using System.Collections.Generic;

namespace Roofstock01
{
    public partial class Properties
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal GrossYield { get; set; }
    }
}
