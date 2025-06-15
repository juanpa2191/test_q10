using EstudiantesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstudiantesApp.Infrastructure.Data.Configurations
{
    public class MateriaEstudianteConfiguration : IEntityTypeConfiguration<MateriaEstudiante>
    {
        public void Configure(EntityTypeBuilder<MateriaEstudiante> builder)
        {
            builder.HasKey(me => new { me.EstudianteId, me.MateriaId });

            builder.HasOne(me => me.Estudiante)
                   .WithMany(e => e.MateriasEstudiantes)
                   .HasForeignKey(me => me.EstudianteId);

            builder.HasOne(me => me.Materia)
                   .WithMany(m => m.MateriasEstudiantes)
                   .HasForeignKey(me => me.MateriaId);
        }
    }
}
