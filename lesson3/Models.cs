using System;
using System.Collections.Generic;

namespace lesson3
{
    public partial class Models
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
