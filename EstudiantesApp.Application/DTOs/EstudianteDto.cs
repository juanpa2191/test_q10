namespace EstudiantesApp.Application.DTOs
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public List<InscripcionDto> MateriasInscritas { get; set; } = new List<InscripcionDto>();
    }
}
