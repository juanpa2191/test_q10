using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;

namespace EstudiantesApp.Application.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IMateriaEstudianteRepository _materiaEstudianteRepo;
        private readonly IMateriaRepository _materiaRepo;

        public InscripcionService(
            IMateriaEstudianteRepository materiaEstudianteRepo,
            IMateriaRepository materiaRepo)
        {
            _materiaEstudianteRepo = materiaEstudianteRepo;
            _materiaRepo = materiaRepo;
        }

        public async Task<bool> InscribirMateriaAsync(int estudianteId, int materiaId)
        {
            if (await _materiaEstudianteRepo.ExistsAsync(estudianteId, materiaId))
                return false;

            var inscritas = await _materiaEstudianteRepo.GetByEstudianteIdAsync(estudianteId);
            var materiasCupo = inscritas
                .Where(i => i.Materia.Creditos >= 4)
                .ToList();

            if (materiasCupo.Count >= 3)
                return false;

            var inscripcion = new MateriaEstudiante
            {
                EstudianteId = estudianteId,
                MateriaId = materiaId
            };

            await _materiaEstudianteRepo.AddAsync(inscripcion);
            return true;
        }
    }
}
