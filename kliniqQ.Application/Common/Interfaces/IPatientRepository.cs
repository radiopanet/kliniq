using kliniqQ.Domain.Entities;

namespace kliniqQ.Application.Common.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(int patientId, CancellationToken cancellationToken);
}