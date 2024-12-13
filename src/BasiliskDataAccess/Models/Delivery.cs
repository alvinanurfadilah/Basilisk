using System;
using System.Collections.Generic;

namespace BasiliskDataAccess.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
