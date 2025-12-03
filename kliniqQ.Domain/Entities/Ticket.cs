namespace kliniqQ.Domain.Entities
{
    using kliniqQ.Domain.Entities.Base;
    public class Ticket : Entity
    {
        public string TicketNumber { get;  set; } = string.Empty;

        public DateTime IssuedAt { get;  set; } = DateTime.UtcNow;

        public DateTime? CalledAt { get; private set; }
        public DateTime? ServiceStartAt { get; private set; }
        public DateTime? ServiceEndAt { get; private set; }
        public DateOnly IssuedDate { get;  set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public TicketStatus Status { get;  set; } = TicketStatus.Waiting;
        public int PatientId { get;  set; }
        public Patient Patient { get;  set; } = default!;
        public int StationId { get;  set; } = default!;
        public Station? Station { get;  set; } = default!;
        public int? AssignedNurseId { get;  set; }
        public Nurse? AssignedNurse { get;  set; }

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