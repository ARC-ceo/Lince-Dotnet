using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class SetorMapping : IEntityTypeConfiguration<Setor>
{
    public void Configure(EntityTypeBuilder<Setor> b)
    {
        b.ToTable("LC_SETOR");
        
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
        b.HasMany(x => x.Cameras)
            .WithOne(x => x.Setor)
            .HasForeignKey(x => x.SetorId);
    }
}