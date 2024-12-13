using System;
using System.Collections.Generic;

namespace BasiliskDataAccess.Models
{
    public partial class Region
    {
        public Region()
        {
            SalesmanEmployeeNumbers = new HashSet<Salesman>();
        }

        public long Id { get; set; }
        public string City { get; set; } = null!;
        public string? Remark { get; set; }

        public virtual ICollection<Salesman> SalesmanEmployeeNumbers { get; set; }
    }
}
