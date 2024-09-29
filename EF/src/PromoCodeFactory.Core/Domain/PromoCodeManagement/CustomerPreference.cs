using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    [Table("CustomerPreference")]
    public class CustomerPreference
    {
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Column("preference_id")]
        public Guid PreferenceId { get; set; }

        public virtual Preference Preference { get; set; }
    }
}