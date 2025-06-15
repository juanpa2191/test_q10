using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Application.Services;
using EstudiantesApp.Domain.Entities;
using Moq;
using Xunit;

namespace EstudiantesApp.Tests.UnitTests.Services
{
    public class InscripcionServiceTests
    {
        private readonly Mock<IMateriaEstudianteRepository> _mockMateriaEstudianteRepo;
        private readonly Mock<IMateriaRepository> _mockMateriaRepo;
        private readonly InscripcionService _service;

        public InscripcionServiceTests()
        {
            _mockMateriaEstudianteRepo = new Mock<IMateriaEstudianteRepository>();
            _mockMateriaRepo = new Mock<IMateriaRepository>();
            _service = new InscripcionService(
                _mockMateriaEstudianteRepo.Object,
                _mockMateriaRepo.Object);
        }

        [Fact]
        public async Task InscribirMateria()
        {
            // Arrange
            int estudianteId = 1;
            int materiaId = 1;
            _mockMateriaRepo.Setup(x => x.GetByIdAsync(materiaId)).ReturnsAsync((Materia)null);
            var inscritas = new List<MateriaEstudiante>
            {
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } },
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } },
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } }
            };
            _mockMateriaEstudianteRepo.Setup(x => x.GetByEstudianteIdAsync(estudianteId))
                .ReturnsAsync(inscritas);
            // Act
            var result = await _service.InscribirMateriaAsync(estudianteId, materiaId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task InscribirMateria_validacion()
        {
            // Arrange
            int estudianteId = 1;
            int materiaId = 1;
            var materia = new Materia { Id = 1, Nombre = "Matemáticas", Creditos = 5 };
            var inscritas = new List<MateriaEstudiante>
            {
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } },
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } },
                new MateriaEstudiante { Materia = new Materia { Creditos = 4 } }
            };

            _mockMateriaRepo.Setup(x => x.GetByIdAsync(materiaId)).ReturnsAsync(materia);
            _mockMateriaEstudianteRepo.Setup(x => x.GetByEstudianteIdAsync(estudianteId))
                .ReturnsAsync(inscritas);

            // Act
            var result = await _service.InscribirMateriaAsync(estudianteId, materiaId);

            // Assert
            Assert.False(result);
        }
    }
}
