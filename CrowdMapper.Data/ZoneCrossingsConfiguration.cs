using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrowdMapper.Domain.Entities;

namespace CrowdMapper.Data
{
    class ZoneCrossingsConfiguration : IEntityTypeConfiguration<ZoneCrossing>
    {
        public void Configure(EntityTypeBuilder<ZoneCrossing> builder)
        {
            builder.Property(e => e.Direction).IsRequired();
            builder.Property(e => e.Timestamp).IsRequired();
            builder.Property(e => e.ZoneId).IsRequired();
        }
    }
}
