using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}