using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Controllers;
using EstudiantesApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EstudiantesApp.Tests
{
    public class EstudiantesControllerTests
    {
        private readonly Mock<IEstudianteRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EstudiantesController _controller;

        public EstudiantesControllerTests()
        {
            _mockRepo = new Mock<IEstudianteRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new EstudiantesController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Index_RetornaEstudiantes()
        {
            var entities = new List<Estudiante> { new Estudiante { Id = 1, Nombre = "Juan" } };
            var dtos = new List<EstudianteDto> { new EstudianteDto { Id = 1, Nombre = "Juan" } };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mockMapper.Setup(m => m.Map<IEnumerable<EstudianteDto>>(entities)).Returns(dtos);

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EstudianteDto>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create()
        {
            var dto = new EstudianteDto { Id = 0, Nombre = "Luis" };
            var entity = new Estudiante { Id = 1, Nombre = "Luis" };

            _mockMapper.Setup(m => m.Map<Estudiante>(dto)).Returns(entity);

            var result = await _controller.Create(dto);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Edit()
        {
            var dto = new EstudianteDto { Id = 1, Nombre = "Carlos" };
            var entity = new Estudiante { Id = 1, Nombre = "Carlos" };

            _mockMapper.Setup(m => m.Map<Estudiante>(dto)).Returns(entity);

            var result = await _controller.Edit(1, dto);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Delete()
        {
            var entity = new Estudiante { Id = 2, Nombre = "Mario" };
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(entity);

            var result = await _controller.Delete(2);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Delete_No_Encuentra_Estudiante()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Estudiante)null);
            var result = await _controller.Delete(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
