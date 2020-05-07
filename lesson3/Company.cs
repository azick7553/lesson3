using System;
using System.Collections.Generic;

namespace lesson3
{
    public partial class Company
    {
        public Company()
        {
            Models = new HashSet<Models>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Models> Models { get; set; }
    }
}
