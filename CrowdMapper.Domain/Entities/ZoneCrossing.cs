using System;

namespace CrowdMapper.Domain.Entities
{
    public enum CrossingDirection
    {
        Enter,
        Exit
    }
    public class ZoneCrossing
    {
        public int Id { get; set; }
        public CrossingDirection Direction { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
