using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Application.Services;
using EstudiantesApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EstudiantesApp.Web.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly IEstudianteRepository _estudianteRepo;
        private readonly IMateriaRepository _materiaRepo;
        private readonly IMateriaEstudianteRepository _inscripcionRepo;
        private readonly IInscripcionService _inscripcionService;
        private readonly IMapper _mapper;

        public InscripcionesController(
            IEstudianteRepository estudianteRepo,
            IMateriaRepository materiaRepo,
            IMateriaEstudianteRepository inscripcionRepo,
            IInscripcionService inscripcionService,
            IMapper mapper)
        {
            _estudianteRepo = estudianteRepo;
            _materiaRepo = materiaRepo;
            _inscripcionRepo = inscripcionRepo;
            _inscripcionService = inscripcionService;
            _mapper = mapper;
        }
        [HttpGet("Inscripciones")]
        public async Task<IActionResult> Index(int estudianteId)
        {
            var inscripciones = await _inscripcionRepo.GetByEstudianteIdAsync(estudianteId);
            var dto = _mapper.Map<List<InscripcionDto>>(inscripciones);
            ViewBag.EstudianteId = estudianteId;
            return View(dto);
        }

        public async Task<IActionResult> Create(int estudianteId)
        {
            var materias = await _materiaRepo.GetAllAsync();
            ViewBag.Materias = _mapper.Map<List<MateriaDto>>(materias);
            ViewBag.EstudianteId = estudianteId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int estudianteId, int materiaId)
        {
            var success = await _inscripcionService.InscribirMateriaAsync(estudianteId, materiaId);

            if (!success)
            {
                TempData["Error"] = "No se pudo inscribir. Revisa las restricciones de inscripción.";
            }

            return RedirectToAction(nameof(Index), new { estudianteId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int estudianteId, int materiaId)
        {
            var inscripcion = new MateriaEstudiante
            {
                EstudianteId = estudianteId,
                MateriaId = materiaId
            };

            await _inscripcionRepo.DeleteAsync(inscripcion);
            return RedirectToAction(nameof(Index), new { estudianteId });
        }
    }
}
