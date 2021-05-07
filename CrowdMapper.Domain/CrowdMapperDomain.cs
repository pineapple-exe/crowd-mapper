using CrowdMapper.Domain.Entities;
using CrowdMapper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdMapper.Domain
{
    public class CrowdMapperDomain
    {
        private IZoneRepository _zoneRepository;

        public CrowdMapperDomain(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        private void GeneralDirection(string zone, DateTimeOffset timestamp, CrossingDirection direction)
        {
            Zone theZone = _zoneRepository.FetchZone(zone);

            bool zoneWasFetched = theZone != null;

            if (!zoneWasFetched)
            {
                theZone = new Zone
                {
                    Name = zone,
                    Crossings = new List<ZoneCrossing>()
                };
            }

            ZoneCrossing zoneCrossing = new()
            {
                Direction = direction,
                Timestamp = timestamp
            };
            theZone.Crossings.Add(zoneCrossing);

            _zoneRepository.SaveZone(theZone);
        }

        public void Enter(string zone, DateTimeOffset timestamp)
        {
            GeneralDirection(zone, timestamp, CrossingDirection.Enter);
        }

        public void Exit(string zone, DateTimeOffset timestamp)
        {
            GeneralDirection(zone, timestamp, CrossingDirection.Exit);
        }

        public List<ZoneOutputModel> List()
        {
            List<Zone> allZones = _zoneRepository.FetchAll();

            var enters = allZones.SelectMany(z => z.Crossings).Where(zc => zc.Direction == CrossingDirection.Enter);
            var exits = allZones.SelectMany(z => z.Crossings).Where(zc => zc.Direction == CrossingDirection.Exit);

            return allZones.Select(z => new ZoneOutputModel(z.Name, enters.Count(zc => zc.ZoneId == z.Id) - exits.Count(zc => zc.ZoneId == z.Id))).ToList();
        }

        public void ClearZone(string zone)
        {
            Zone theZone = _zoneRepository.FetchZone(zone);

            if (theZone != null)
            { 
                theZone.Crossings.Clear();

                _zoneRepository.SaveZone(theZone);
            }
        }
    }
}
