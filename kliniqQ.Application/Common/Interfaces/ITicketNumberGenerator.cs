using kliniqQ.Domain.Entities;

namespace kliniqQ.Application.Common.Interfaces;

public interface ITicketNumberGenerator
{
    Task<string> GenerateAsync(CancellationToken cancellationToken);
}