using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lince.Infrastructure.Persistence.Mappings;

public class CameraMapping : IEntityTypeConfiguration<Camera>
{
    public void Configure(EntityTypeBuilder<Camera> b)
    {
        b.ToTable("LC_CAMERA");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Localizacao)
            .IsRequired()
            .HasMaxLength(100);
        
        b.Property(x => x.Descricao)
            .HasMaxLength(200);
        
        b.Property(x => x.Status)
            .HasMaxLength(10);
        
        // 1..N
        b.HasMany(x => x.Alertas)
            .WithOne(x => x.Camera)
            .HasForeignKey(x => x.CameraId);
    }
}