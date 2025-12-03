namespace kliniqQ.Domain.Entities
{
    using kliniqQ.Domain.Entities.Base;

    public class Patient: Entity
    {
        public string NationalId {get; private set;} = string.Empty;
        public DateTime? DateOfBirth {get; private set;}
        public string FullName {get; private set;} = string.Empty;
        public string? PhoneNumber {get; private set;}
        public string? Gender {get; private set;}
        public DateTime CreatedAt{get; private set;} = DateTime.UtcNow;
        public Patient(string nationalId, string fullName)
        {
            NationalId = nationalId;
            FullName = fullName;
        }
    }
}