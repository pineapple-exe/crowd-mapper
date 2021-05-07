using System.Collections.Generic;

namespace CrowdMapper.Domain.Entities
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ZoneCrossing> Crossings { get; set; }
    }
}
