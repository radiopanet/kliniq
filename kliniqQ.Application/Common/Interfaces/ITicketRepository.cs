namespace kliniqQ.Application.Common.Interfaces;

using kliniqQ.Domain.Entities;

public interface ITicketRepository
{
    Task<Ticket?> GetTodayTicketForPatientAsync(int patientId, CancellationToken cancellationToken);
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
}