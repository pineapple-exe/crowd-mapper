namespace CrowdMapper.Domain
{
    public class ZoneOutputModel
    {
        public string Name { get; }
        public int CurrentVisitorsCount { get; }

        public ZoneOutputModel(string name, int currentVisitorsCount)
        {
            Name = name;
            CurrentVisitorsCount = currentVisitorsCount;
        }
    }
}
