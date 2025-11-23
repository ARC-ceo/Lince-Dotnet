using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class SupervisorMapping : IEntityTypeConfiguration<Supervisor>
{
    public void Configure(EntityTypeBuilder<Supervisor> b)
    {
        b.ToTable("LC_SUPERVISOR");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);
        
        b.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);
        
        b.HasIndex(x => x.Email)
            .IsUnique();
        
        b.Property(x => x.Telefone)
            .HasMaxLength(11);

    }
}