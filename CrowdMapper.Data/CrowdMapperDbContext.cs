using System;
using Microsoft.EntityFrameworkCore;
using CrowdMapper.Domain.Entities;

namespace CrowdMapper.Data
{
    public class CrowdMapperDbContext : DbContext
    {
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneCrossing> ZoneCrossing { get; set; }

        public CrowdMapperDbContext()
        {

        }

        public CrowdMapperDbContext(DbContextOptions<CrowdMapperDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=CrowdMapper;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ZonesConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneCrossingsConfiguration());
        }
    }
}
