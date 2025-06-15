using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EstudiantesApp.Controllers
{
    public class MateriasController : Controller
    {
        private readonly IMateriaRepository _repo;
        private readonly IMapper _mapper;

        public MateriasController(IMateriaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var materias = await _repo.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<MateriaDto>>(materias);
            return View(dtoList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MateriaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Materia>(dto);
            await _repo.AddAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var materia = await _repo.GetByIdAsync(id);
            if (materia == null) return NotFound();

            var dto = _mapper.Map<MateriaDto>(materia);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MateriaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Materia>(dto);
            await _repo.UpdateAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var materia = await _repo.GetByIdAsync(id);
            if (materia == null) return NotFound();

            await _repo.DeleteAsync(materia);
            return RedirectToAction(nameof(Index));
        }
    }
}
