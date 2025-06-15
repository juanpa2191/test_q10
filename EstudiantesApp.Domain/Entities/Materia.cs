namespace EstudiantesApp.Domain.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Creditos { get; set; }
        public ICollection<MateriaEstudiante> MateriasEstudiantes { get; set; } = new List<MateriaEstudiante>();
    }
}
