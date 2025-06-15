using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesApp.Infrastructure.Repositories
{
    public class MateriaEstudianteRepository : IMateriaEstudianteRepository
    {
        private readonly AppDbContext _context;

        public MateriaEstudianteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MateriaEstudiante>> GetByEstudianteIdAsync(int estudianteId)
        {
            return await _context.MateriasEstudiantes
                .Include(me => me.Materia)
                .Where(me => me.EstudianteId == estudianteId)
                .ToListAsync();
        }

        public async Task AddAsync(MateriaEstudiante inscripcion)
        {
            await _context.MateriasEstudiantes.AddAsync(inscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MateriaEstudiante inscripcion)
        {
            _context.MateriasEstudiantes.Remove(inscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int estudianteId, int materiaId)
        {
            return await _context.MateriasEstudiantes
                .AnyAsync(me => me.EstudianteId == estudianteId && me.MateriaId == materiaId);
        }
    }
}
