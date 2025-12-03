namespace kliniqQ.Domain.Entities
{
    using kliniqQ.Domain.Entities.Base;

    public class Nurse : Entity
    {
        public string FirstName {get; private set;} = string.Empty;
        public string LastName {get; private set;} = string.Empty;
        public string EmployeeNumber {get; private set;} = string.Empty;
        public int? CurrentStationId {get; private set;}
        public Station? CurrentStation {get; private set;} = default!;
        public bool IsActive {get; private set;} = true;
        public DateTime CreatedAt {get; private set;} = DateTime.UtcNow;
        public void AssignStation(int stationId) => CurrentStationId = stationId;
        public void UnAssignStation() => CurrentStationId = null;

    }
}