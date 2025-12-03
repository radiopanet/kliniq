namespace kliniqQ.Domain.Entities
{
    using kliniqQ.Domain.Entities.Base;

    public enum TicketStatus
    {
        Waiting,
        Called,
        Serving,
        Completed,
        NoShow,
        Cancelled
    }
    public class Ticket: Entity
    {
        public string TicketNumber {get; private set;} = string.Empty;

        public DateTime IssuedAt {get; private set;} = DateTime.UtcNow;

        public DateTime? CalledAt {get; private set;}
        public DateTime? ServiceStartAt {get; private set;}
        public DateTime? ServiceEndAt {get; private set;}
        public DateOnly IssuedDate {get; private set;} = DateOnly.FromDateTime(DateTime.UtcNow);
        public TicketStatus Status {get; private set;} = TicketStatus.Waiting;
        public int PatientId {get; private set;}
        public Patient Patient {get; private set;} = default!;
        public int? StationId {get; private set;}
        public Station? Station {get; private set;} = default!;
        public int? AssignedNurseId {get; private set;}
        public Nurse? AssignedNurse {get; private set;}

        public Ticket(string ticketNumber, int patientId)
        {
            TicketNumber = ticketNumber;
            PatientId = patientId;
        }

        public void Call(int stationId, int patientId)
        {
            StationId = stationId;
            PatientId = patientId;
            CalledAt = DateTime.UtcNow;
            Status = TicketStatus.Called;
        }

        public void Serving()
        {
            ServiceStartAt = DateTime.UtcNow;
            Status = TicketStatus.Serving;
        }

        public void Completed()
        {
            ServiceEndAt = DateTime.UtcNow;
            Status = TicketStatus.Completed;
        }

        public void MarkNoShow() => Status = TicketStatus.NoShow;
        public void Cancel() => Status = TicketStatus.Cancelled;

    }
}