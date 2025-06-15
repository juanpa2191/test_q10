namespace EstudiantesApp.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public ICollection<MateriaEstudiante> MateriasEstudiantes { get; set; } = new List<MateriaEstudiante>();
    }

}
