namespace EstudiantesApp.Application.Services
{
    public interface IInscripcionService
    {
        Task<bool> InscribirMateriaAsync(int estudianteId, int materiaId);
    }
}
