using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class AlertaMapping : IEntityTypeConfiguration<Alerta>
{
    public void Configure(EntityTypeBuilder<Alerta> b)
    {
        b.ToTable("LC_ALERTA");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Motivo)
            .IsRequired()
            .HasMaxLength(200);
        
        b.Property(x => x.NivelAlerta)
            .IsRequired()
            .HasMaxLength(20);
        
        b.Property(x => x.DataHora)
            .IsRequired();
        
    }
}