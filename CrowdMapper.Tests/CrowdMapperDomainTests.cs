using NUnit.Framework;
using System.Collections.Generic;
using CrowdMapper.Domain.Entities;
using System;
using System.Linq;
using CrowdMapper.Domain;

namespace CrowdMapper.Tests
{
    public class Tests
    {
        [Test]
        public void EnterNewZone()
        {
            List<Zone> zones = new();

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Enter(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.True(zones.Count == 1);
            Zone zone = zones.FirstOrDefault(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(1, zone.Crossings.Count);
            ZoneCrossing crossing = zone.Crossings.Single();
            Assert.AreEqual(CrossingDirection.Enter, crossing.Direction);
        }

        [Test]
        public void EnterPreExistingZone()
        {
            List<Zone> zones = new();
            Zone preExistingZone = new();
            preExistingZone.Name = "zoneA";
            preExistingZone.Crossings = new List<ZoneCrossing>
            {
                new ZoneCrossing()
            };
            preExistingZone.Crossings.Single().Direction = CrossingDirection.Enter;
            zones.Add(preExistingZone);

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Enter(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.AreEqual(1, zones.Count);
            Zone zone = zones.FirstOrDefault(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(2, zone.Crossings.Count);

            foreach (ZoneCrossing crossing in zone.Crossings)
            {
                Assert.AreEqual(CrossingDirection.Enter, crossing.Direction);
            }
        }

        [Test]
        public void EnterSameZoneTwice()
        {
            List<Zone> zones = new();

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Enter(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));
            crowdMapperDomain.Enter(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.AreEqual(1, zones.Count);
            Zone zone = zones.FirstOrDefault(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(2, zone.Crossings.Count);

            foreach (ZoneCrossing crossing in zone.Crossings)
            {
                Assert.AreEqual(CrossingDirection.Enter, crossing.Direction);
            }
        }

        [Test]
        public void ExitNewZone()
        {
            List<Zone> zones = new();

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Exit(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.AreEqual(1, zones.Count);
            Zone zone = zones.Single(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(1, zone.Crossings.Count);
            ZoneCrossing zoneCrossing = zone.Crossings.Single();
            Assert.AreEqual(CrossingDirection.Exit, zoneCrossing.Direction);
        }

        [Test]
        public void ExitPreExistingZone()
        {
            List<Zone> zones = new();
            Zone preExistingZone = new();
            preExistingZone.Name = "zoneA";
            preExistingZone.Crossings = new List<ZoneCrossing>
            {
                new ZoneCrossing()
            };
            preExistingZone.Crossings.Single().Direction = CrossingDirection.Exit;
            zones.Add(preExistingZone);

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Exit(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.AreEqual(1, zones.Count);
            Zone zone = zones.FirstOrDefault(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(2, zone.Crossings.Count);

            foreach (ZoneCrossing crossing in zone.Crossings)
            {
                Assert.AreEqual(CrossingDirection.Exit, crossing.Direction);
            }
        }

        [Test]
        public void ExitSameZoneTwice()
        {
            List<Zone> zones = new();

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            string zoneName = "zoneA";

            crowdMapperDomain.Exit(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));
            crowdMapperDomain.Exit(zoneName, new DateTimeOffset(new DateTime(2021, 05, 06)));

            Assert.AreEqual(1, zones.Count);
            Zone zone = zones.FirstOrDefault(z => z.Name == zoneName);
            Assert.NotNull(zone);
            Assert.AreEqual(2, zone.Crossings.Count);

            foreach (ZoneCrossing crossing in zone.Crossings)
            {
                Assert.AreEqual(CrossingDirection.Exit, crossing.Direction);
            }
        }

        [Test]
        public void ListAllZones()
        {
            List<Zone> allZones = new() 
            { 
                new Zone() 
                { 
                    Id = 1,
                    Name = "zoneA",
                    Crossings = new List<ZoneCrossing>() 
                    { 
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 1,
                            Direction = CrossingDirection.Enter
                        },
                        
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 1,
                            Direction = CrossingDirection.Enter
                        },
                    } 
                },

                new Zone()
                {
                    Id = 2,
                    Name = "zoneB",
                    Crossings = new List<ZoneCrossing>()
                    {
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 2,
                            Direction = CrossingDirection.Enter
                        },

                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 2,
                            Direction = CrossingDirection.Exit
                        },
                    }
                },

                new Zone()
                {
                    Id = 3,
                    Name = "zoneC",
                    Crossings = new List<ZoneCrossing>()
                    {
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 3,
                            Direction = CrossingDirection.Exit
                        },

                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            ZoneId = 3,
                            Direction = CrossingDirection.Exit
                        },
                    }
                },
            };

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(allZones));

            List<ZoneOutputModel> output = crowdMapperDomain.List();

            Assert.AreEqual(allZones.Count, output.Count);

            ZoneOutputModel zomA = output.Single(zom => zom.Name == "zoneA");
            ZoneOutputModel zomB = output.Single(zom => zom.Name == "zoneB");
            ZoneOutputModel zomC = output.Single(zom => zom.Name == "zoneC");
            Assert.AreEqual(2, zomA.CurrentVisitorsCount);
            Assert.AreEqual(0, zomB.CurrentVisitorsCount);
            Assert.AreEqual(-2, zomC.CurrentVisitorsCount);
        }

        [Test]
        public void ClearOneZone()
        {
            List<Zone> zones = new()
            {
                new Zone()
                {
                    Name = "zoneA",
                    Crossings = new List<ZoneCrossing>()
                    {
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            Direction = CrossingDirection.Enter
                        }
                    }
                },

                new Zone()
                {
                    Name = "zoneB",
                    Crossings = new List<ZoneCrossing>()
                    {
                        new ZoneCrossing()
                        {
                            Timestamp = new DateTimeOffset(new DateTime(2021, 05, 07)),
                            Direction = CrossingDirection.Enter
                        }
                    }
                }
            };

            CrowdMapperDomain crowdMapperDomain = new(new FakeZoneRepository(zones));

            crowdMapperDomain.ClearZone("zoneA");

            Assert.AreEqual(0, zones.Single(z => z.Name == "zoneA").Crossings.Count);
            Assert.AreEqual(1, zones.Single(z => z.Name == "zoneB").Crossings.Count);
        }
    }
}