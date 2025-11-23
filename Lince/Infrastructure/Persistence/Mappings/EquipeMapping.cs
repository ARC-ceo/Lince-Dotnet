using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class EquipeMapping : IEntityTypeConfiguration<Equipe>
{
    public void Configure(EntityTypeBuilder<Equipe> b)
    {
        b.ToTable("LC_EQUIPE");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);
        
        b.Property(x => x.Descricao)
            .HasMaxLength(200);
        
        // 1..N
        b.HasMany(x => x.Supervisores)
            .WithOne(x => x.Equipe)
            .HasForeignKey(x => x.EquipeId);
        
        // 1..N
        b.HasMany(x => x.Operadores)
            .WithOne(x => x.Equipe)
            .HasForeignKey(x => x.EquipeId);
    }
}