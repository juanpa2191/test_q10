using EstudiantesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstudiantesApp.Infrastructure.Data.Configurations
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Codigo).IsUnique();
            builder.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Creditos).IsRequired();
        }
    }
}
