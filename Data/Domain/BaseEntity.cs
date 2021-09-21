using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdatedBy { get; set; }
        public string LastUpdatedByName { get; set; }

        public DateTime? Deleted { get; set; }
        public int? DeletedBy { get; set; }
        public string DeletedByName { get; set; }

    }
}
