using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EstudiantesApp.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteRepository _repo;
        private readonly IMapper _mapper;

        public EstudiantesController(IEstudianteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repo.GetAllAsync();
            var model = _mapper.Map<IEnumerable<EstudianteDto>>(data);
            return View(model); 
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstudianteDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Estudiante>(dto);
            await _repo.AddAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            if (estudiante == null) return NotFound();

            var dto = _mapper.Map<EstudianteDto>(estudiante);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EstudianteDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Estudiante>(dto);
            await _repo.UpdateAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            if (estudiante == null) return NotFound();

            await _repo.DeleteAsync(estudiante);
            return RedirectToAction(nameof(Index));
        }
    }
}
