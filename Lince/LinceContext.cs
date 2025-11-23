using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Lince;

public class LinceContext : DbContext
{
    public LinceContext(DbContextOptions<LinceContext> options) : base(options)
    {
        
    }
    
    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<Camera> Cameras { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<Operador> Operadores { get; set; }
    public DbSet<Setor> Setores { get; set; }
    public DbSet<Supervisor> Supervisores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlertaMapping());
        modelBuilder.ApplyConfiguration(new CameraMapping());
        modelBuilder.ApplyConfiguration(new EquipeMapping());
        modelBuilder.ApplyConfiguration(new OperadorMapping());
        modelBuilder.ApplyConfiguration(new SetorMapping());
        modelBuilder.ApplyConfiguration(new SupervisorMapping());
        base.OnModelCreating(modelBuilder);
    }
}