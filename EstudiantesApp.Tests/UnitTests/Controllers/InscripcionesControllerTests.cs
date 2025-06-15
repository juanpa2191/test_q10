using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Application.Services;
using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EstudiantesApp.Tests.UnitTests.Controllers
{
    public class InscripcionesControllerTests
    {
        private readonly Mock<IEstudianteRepository> _estRepo;
        private readonly Mock<IMateriaRepository> _matRepo;
        private readonly Mock<IMateriaEstudianteRepository> _matEstRepo;
        private readonly Mock<IInscripcionService> _inscSer;
        private readonly Mock<IMapper> _mapper;
        private readonly InscripcionesController _controller;

        public InscripcionesControllerTests()
        {
            _estRepo = new Mock<IEstudianteRepository>();
            _matRepo = new Mock<IMateriaRepository>();
            _matEstRepo = new Mock<IMateriaEstudianteRepository>();
            _inscSer = new Mock<IInscripcionService>();
            _mapper = new Mock<IMapper>();
            _controller = new InscripcionesController(_estRepo.Object, _matRepo.Object, _matEstRepo.Object, _inscSer.Object, _mapper.Object);
        }

        [Fact]
        public async Task Index()
        {
            var dtos = new List<InscripcionDto>();
            var materiaEstudiante = new List<MateriaEstudiante>();
            int estudianteId = 1;

            _matEstRepo.Setup(r => r.GetByEstudianteIdAsync(estudianteId)).ReturnsAsync(materiaEstudiante);
            _mapper.Setup(m => m.Map<List<InscripcionDto>>(materiaEstudiante)).Returns(dtos);

            var result = await _controller.Index(estudianteId);

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult!.Model.Should().BeEquivalentTo(dtos);
        }
    }
}
