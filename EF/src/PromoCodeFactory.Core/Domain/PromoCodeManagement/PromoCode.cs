using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    [Table("PromoCodes")]
    public class PromoCode
        : BaseEntity
    {
        [Column("code")]
        public string Code { get; set; }

        [Column("servie_info")]
        public string ServiceInfo { get; set; }

        [Column("begin_date")]
        public DateTime BeginDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("partner_name")]
        public string PartnerName { get; set; }

        [Column("partner_manager_id")]
        public Guid PartnerManagerId { get; set; }

        public virtual Employee PartnerManager { get; set; }

        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        public virtual Preference Preference { get; set; }
    }
}