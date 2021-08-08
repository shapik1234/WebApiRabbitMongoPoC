using System;
using System.Collections.Generic;

namespace CustomerApi.Data.Entities
{
    public partial class Customer : BaseEntity
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Age { get; set; }
    }
}
