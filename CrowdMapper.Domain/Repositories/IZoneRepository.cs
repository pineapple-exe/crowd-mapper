using CrowdMapper.Domain.Entities;
using System.Collections.Generic;

namespace CrowdMapper.Domain.Repositories
{
    public interface IZoneRepository
    {
        Zone FetchZone(string zone);
        void SaveZone(Zone theZone);
        List<Zone> FetchAll();
    }
}