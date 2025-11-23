using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class OperadorMapping : IEntityTypeConfiguration<Operador>
{
    public void Configure(EntityTypeBuilder<Operador> b)
    {
        b.ToTable("LC_OPERADOR");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);
        
        b.Property(x => x.Funcao)
            .IsRequired()
            .HasMaxLength(200);
        
        // 1..N
        b.HasMany(x => x.Alertas)
            .WithOne(x => x.Operador)
            .HasForeignKey(x => x.OperadorId);
    }
}