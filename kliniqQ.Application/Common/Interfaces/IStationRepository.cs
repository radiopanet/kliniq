using kliniqQ.Domain.Entities;

namespace kliniqQ.Application.Common.Interfaces;

public interface IStationRepository
{
    Task<Station?> GetByIdAsync(int stationId, CancellationToken cancellationToken);
}