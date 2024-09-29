using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    [Table("Customers")]
    public class Customer
        : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Column("email")]
        public string Email { get; set; }

        public virtual ICollection<CustomerPreference> Preferences { get; set; }
    }
}