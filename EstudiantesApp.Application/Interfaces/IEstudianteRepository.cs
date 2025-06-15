using EstudiantesApp.Domain.Entities;

namespace EstudiantesApp.Application.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task AddAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(Estudiante estudiante);
    }
}
