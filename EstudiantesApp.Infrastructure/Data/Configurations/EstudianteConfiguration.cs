using EstudiantesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstudiantesApp.Infrastructure.Data.Configurations
{
    public class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Documento).IsUnique();
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Correo).IsRequired().HasMaxLength(100);
        }
    }
}
