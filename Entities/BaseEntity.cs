using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public string CreatedByName { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [Required]
        public int LastUpdatedBy { get; set; }
        [Required]
        public string LastUpdatedByName { get; set; }
        public DateTime? Deleted { get; set; }
        public int? DeletedBy { get; set; }
        public string DeletedByName { get; set; }
    }
}
