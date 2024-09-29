using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.Administration
{
    [Table("Roles")]
    public class Role
        : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}