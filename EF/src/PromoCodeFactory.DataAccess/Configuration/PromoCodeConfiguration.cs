using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace PromoCodeFactory.DataAccess.Configuration
{
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(100);
            builder.Property(p => p.ServiceInfo).HasMaxLength(100);
            builder.Property(p => p.PartnerName).HasMaxLength(200);
        }
    }
}
