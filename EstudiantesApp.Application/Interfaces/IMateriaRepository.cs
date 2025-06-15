using EstudiantesApp.Domain.Entities;

namespace EstudiantesApp.Application.Interfaces
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia?> GetByIdAsync(int id);
        Task AddAsync(Materia materia);
        Task UpdateAsync(Materia materia);
        Task DeleteAsync(Materia materia);
    }
}
