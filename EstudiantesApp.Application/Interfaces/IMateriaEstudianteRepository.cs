using EstudiantesApp.Domain.Entities;

namespace EstudiantesApp.Application.Interfaces
{
    public interface IMateriaEstudianteRepository
    {
        Task<IEnumerable<MateriaEstudiante>> GetByEstudianteIdAsync(int estudianteId);
        Task AddAsync(MateriaEstudiante inscripcion);
        Task DeleteAsync(MateriaEstudiante inscripcion);
        Task<bool> ExistsAsync(int estudianteId, int materiaId);
    }
}
