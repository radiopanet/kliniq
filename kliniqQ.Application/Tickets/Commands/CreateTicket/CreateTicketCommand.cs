using MediatR;

namespace kliniqQ.Application.Tickets.Commands.CreateTicket;

public sealed record CreateTicketCommand(
    int PatientId,
    int StationId,
    int? AssignedNurseId
) : IRequest<CreatedTicketDto>;