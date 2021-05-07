using CrowdMapper.Domain.Entities;
using CrowdMapper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CrowdMapper.Data.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private CrowdMapperDbContext _context;

        public ZoneRepository(CrowdMapperDbContext context)
        {
            _context = context;
        }

        public Zone FetchZone(string zone)
        {
            Zone theZone = _context.Zones.Include(z => z.Crossings).FirstOrDefault(z => z.Name == zone);
            return theZone;
        }

        public void SaveZone(Zone theZone)
        {
            if (theZone.Id == 0)
            {
                _context.Zones.Add(theZone);
            }
            else
            {
                _context.Zones.Update(theZone);
            }

            _context.SaveChanges();
        }

        public List<Zone> FetchAll()
        {
            List<Zone> allZones = _context.Zones.Include(z => z.Crossings).ToList();
            return allZones;
        }
    }
}
