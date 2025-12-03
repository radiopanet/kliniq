namespace kliniqQ.Domain.Entities
{
    using kliniqQ.Domain.Entities.Base;

    public class Station: Entity
    {
        public string StationName {get; private set;} = string.Empty;
        public bool IsActive {get; private set;} = true;

        public Station(string stationName)
        {
            StationName = stationName;
        }

        public void ActivateStation() => IsActive = true;
        public void DeActivateStation() => IsActive = false;
    }
}