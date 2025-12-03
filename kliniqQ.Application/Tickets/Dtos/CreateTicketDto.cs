namespace kliniqQ.Application.Tickets.Commands.CreateTicket;

public sealed record CreatedTicketDto(
    int TicketId,
    string TicketNumber,
    int PatientId,
    int StationId,
    int? AssignedNurseId,
    string Status,
    DateTime IssedAt
);
