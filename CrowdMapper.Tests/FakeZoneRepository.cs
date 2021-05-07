using CrowdMapper.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using CrowdMapper.Domain.Repositories;

namespace CrowdMapper.Tests
{
    class FakeZoneRepository : IZoneRepository
    {
        private readonly List<Zone> _zones;

        public FakeZoneRepository(List<Zone> zones)
        {
            _zones = zones;
        }
        
        public Zone FetchZone(string zone)
        {
            Zone theZone = _zones.FirstOrDefault(z => z.Name == zone);
            return theZone;
        }

        public void SaveZone(Zone theZone)
        {
            if (!_zones.Contains(theZone))
            {
                _zones.Add(theZone);
            }
        }

        public List<Zone> FetchAll()
        {
            return _zones;
        }
    }
}
