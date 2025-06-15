using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Infrastructure.Data;
using EstudiantesApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstudiantesApp.Tests.Repositories
{
    public class EstudianteRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Add_Estudiante()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new EstudianteRepository(context);
            var estudiante = new Estudiante { Nombre = "Juan", Documento = "123", Correo = "juan@test.com" };

            // Act
            await repo.AddAsync(estudiante);

            // Assert
            context.Estudiantes.Count().Should().Be(1);
        }

        [Fact]
        public async Task GetAll_Estudiantes()
        {
            var context = GetDbContext();
            context.Estudiantes.AddRange(
                new Estudiante { Nombre = "Juan", Documento = "123", Correo = "a@a.com" },
                new Estudiante { Nombre = "Ana", Documento = "456", Correo = "b@b.com" });
            await context.SaveChangesAsync();

            var repo = new EstudianteRepository(context);
            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_Estudiante_Materias()
        {
            var context = GetDbContext();
            var estudiante = new Estudiante
            {
                Nombre = "Carlos",
                Documento = "789",
                Correo = "c@c.com",
                MateriasEstudiantes = new List<MateriaEstudiante>
                {
                    new MateriaEstudiante
                    {
                        Materia = new Materia { Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 }
                    }
                }
            };
            context.Estudiantes.Add(estudiante);
            await context.SaveChangesAsync();

            var repo = new EstudianteRepository(context);
            var result = await repo.GetByIdAsync(estudiante.Id);

            result.Should().NotBeNull();
            result.MateriasEstudiantes.Should().HaveCount(1);
        }

        [Fact]
        public async Task Update_Estudiante()
        {
            var context = GetDbContext();
            var estudiante = new Estudiante { Nombre = "Pedro", Documento = "321", Correo = "p@p.com" };
            context.Estudiantes.Add(estudiante);
            await context.SaveChangesAsync();

            var repo = new EstudianteRepository(context);
            estudiante.Nombre = "Pedro 1";
            await repo.UpdateAsync(estudiante);

            context.Estudiantes.First().Nombre.Should().Be("Pedro 1");
        }

        [Fact]
        public async Task Delete_Estudiante()
        {
            var context = GetDbContext();
            var estudiante = new Estudiante { Nombre = "Luis", Documento = "111", Correo = "l@l.com" };
            context.Estudiantes.Add(estudiante);
            await context.SaveChangesAsync();

            var repo = new EstudianteRepository(context);
            await repo.DeleteAsync(estudiante);

            context.Estudiantes.Should().BeEmpty();
        }
    }
}
