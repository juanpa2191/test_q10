using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Controllers;
using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EstudiantesApp.Tests.UnitTests.Controllers
{
    public class MateriasControllerTests
    {
        private readonly Mock<IMateriaRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MateriasController _controller;

        public MateriasControllerTests()
        {
            _repoMock = new Mock<IMateriaRepository>();
            _mapperMock = new Mock<IMapper>();
            _controller = new MateriasController(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Index()
        {
            var materias = new List<Materia> { new Materia { Id = 1, Nombre = "Matemáticas" } };
            var dtos = new List<MateriaDto> { new MateriaDto { Id = 1, Nombre = "Matemáticas" } };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(materias);
            _mapperMock.Setup(m => m.Map<IEnumerable<MateriaDto>>(materias)).Returns(dtos);

            var result = await _controller.Index();

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult!.Model.Should().BeEquivalentTo(dtos);
        }

        [Fact]
        public void Create_ViewResult()
        {
            var result = _controller.Create();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task Create()
        {
            var dto = new MateriaDto { Id = 1 };
            var entity = new Materia { Id = 1 };
            _mapperMock.Setup(m => m.Map<Materia>(dto)).Returns(entity);

            var result = await _controller.Create(dto);

            result.Should().BeOfType<RedirectToActionResult>()
                .Which.ActionName.Should().Be("Index");

            _repoMock.Verify(r => r.AddAsync(entity), Times.Once);
        }

        [Fact]
        public async Task Edit()
        {
            var materia = new Materia { Id = 1 };
            var dto = new MateriaDto { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(materia);
            _mapperMock.Setup(m => m.Map<MateriaDto>(materia)).Returns(dto);

            var result = await _controller.Edit(1);

            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            viewResult.Model.Should().Be(dto);
        }

        [Fact]
        public async Task Delete()
        {
            var materia = new Materia { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(materia);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<RedirectToActionResult>()
                .Which.ActionName.Should().Be("Index");

            _repoMock.Verify(r => r.DeleteAsync(materia), Times.Once);
        }

        [Fact]
        public async Task Delete_No_Encuentra()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Materia)null);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
