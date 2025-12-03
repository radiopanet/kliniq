using kliniqQ.Domain.Entities;

namespace kliniqQ.Application.Common.Interfaces;

public interface INurseRepository
{
    Task<Nurse?> GetByIdAsync(int nurseId, CancellationToken cancellationToken);
}