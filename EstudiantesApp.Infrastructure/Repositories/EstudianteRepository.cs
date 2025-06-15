using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Domain.Entities;
using EstudiantesApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesApp.Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly AppDbContext _context;

        public EstudianteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _context.Estudiantes
                .Include(e => e.MateriasEstudiantes)
                .ThenInclude(me => me.Materia)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }
    }

}
