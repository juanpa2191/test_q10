using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesApp.Infrastructure.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly AppDbContext _context;

        public MateriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Materias
                .Include(m => m.MateriasEstudiantes)
                .ToListAsync();
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            return await _context.Materias
                .Include(m => m.MateriasEstudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Materia materia)
        {
            _context.Materias.Update(materia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Materia materia)
        {
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }
}
