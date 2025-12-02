using Microsoft.EntityFrameworkCore;
using kliniqQ.Domain.Entities;

namespace kliniqQ.Infrastructure.Persistence;

public class kliniqQDbContext: DbContext
{
    public kliniqQDbContext(DbContextOptions<kliniqQDbContext> options)
        :base(options){}


    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<Nurse> Nurses => Set<Nurse>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(kliniqQDbContext).Assembly);
    }
}
