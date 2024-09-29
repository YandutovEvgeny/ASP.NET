using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    [Table("Preferences")]
    public class Preference
        : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<CustomerPreference> Customers { get; set; }
    }
}