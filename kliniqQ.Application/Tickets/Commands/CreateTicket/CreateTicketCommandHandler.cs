using MediatR;
using kliniqQ.Application.Common.Interfaces;
using kliniqQ.Domain.Entities;
using kliniqQ.Domain;

namespace kliniqQ.Application.Tickets.Commands.CreateTicket;

public sealed class CreateTicketCommandHandler
    : IRequestHandler<CreateTicketCommand, CreatedTicketDto>
{
    private readonly ITicketRepository _ticketRepo;
    private readonly IPatientRepository _patientRepo;
    private readonly IStationRepository _stationRepo;
    private readonly INurseRepository _nurseRepo;
    private readonly ITicketNumberGenerator _ticketNumberGenerator;

    public CreateTicketCommandHandler(
        ITicketRepository ticketRepo,
        IPatientRepository patientRepo,
        IStationRepository stationRepo,
        INurseRepository nurseRepo,
        ITicketNumberGenerator ticketNumberGenerator
    )
    {
        _ticketRepo = ticketRepo;
        _patientRepo = patientRepo;
        _stationRepo = stationRepo;
        _nurseRepo = nurseRepo;
        _ticketNumberGenerator = ticketNumberGenerator;
    }


    public async Task<CreatedTicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepo.GetByIdAsync(request.PatientId, cancellationToken)
                ?? throw new Exception("Patient not found!");

        var station = await _stationRepo.GetByIdAsync(request.StationId, cancellationToken)
                ?? throw new Exception("Station not found!");

        Nurse? nurse = null;
        if (request.AssignedNurseId.HasValue)
        {
            nurse = await _nurseRepo.GetByIdAsync(request.AssignedNurseId.Value, cancellationToken)
                ?? throw new Exception("Assigned Nurse not found");
        }


        var existingTicket = await _ticketRepo.GetTodayTicketForPatientAsync(
            request.PatientId, cancellationToken);

        if (existingTicket != null)
            throw new Exception("Patient already has a ticket for today");

        var tickeNumber = _ticketNumberGenerator.GenerateAsync(cancellationToken);

        var ticket = new Ticket
        {
            TicketNumber = tickeNumber,
            PatientId = request.PatientId,
            StationId = request.StationId,
            AssignedNurseId = request.AssignedNurseId,
            IssuedAt = DateTime.UtcNow,
            IssuedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Status = TicketStatus.Waiting
        };

        await _ticketRepo.AddAsync(ticket, cancellationToken);

        return new CreatedTicketDto
        (
            ticket.Id,
            ticket.TicketNumber,
            ticket.PatientId,
            ticket.StationId,
            ticket.AssignedNurseId,
            ticket.Status.ToString(),
            ticket.IssuedAt
        );
    }
}