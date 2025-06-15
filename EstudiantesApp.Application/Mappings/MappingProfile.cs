using AutoMapper;
using EstudiantesApp.Application.DTOs;
using EstudiantesApp.Domain.Entities;

namespace EstudiantesApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Estudiante, EstudianteDto>()
                .ForMember(dest => dest.MateriasInscritas, opt => opt.MapFrom(src =>
                    src.MateriasEstudiantes.Select(me => new InscripcionDto
                    {
                        EstudianteId = me.EstudianteId,
                        MateriaId = me.MateriaId,
                        MateriaNombre = me.Materia.Nombre,
                        Creditos = me.Materia.Creditos
                    })));

            CreateMap<EstudianteDto, Estudiante>();

            CreateMap<Materia, MateriaDto>().ReverseMap();

            CreateMap<MateriaEstudiante, InscripcionDto>()
                .ForMember(dest => dest.MateriaNombre, opt => opt.MapFrom(src => src.Materia.Nombre))
                .ForMember(dest => dest.Creditos, opt => opt.MapFrom(src => src.Materia.Creditos));

            CreateMap<InscripcionDto, MateriaEstudiante>();
        }
    }
}
