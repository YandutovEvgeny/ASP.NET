using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.Administration
{
    [Table("Employees")]
    public class Employee
        : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Column("email")]
        public string Email { get; set; }

        [Column("role_id")]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Column("applied_promocodes_count")]
        public int AppliedPromocodesCount { get; set; }
    }
}