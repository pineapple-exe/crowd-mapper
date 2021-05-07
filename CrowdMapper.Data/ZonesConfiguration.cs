using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrowdMapper.Domain.Entities;

namespace CrowdMapper.Data
{
    class ZonesConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
