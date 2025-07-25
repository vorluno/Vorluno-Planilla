using AutoMapper;
using Planilla.Application.DTOs;
using Planilla.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Planilla.Application.Mappings
{
    /// <summary>
    /// Define las reglas de mapeo entre las entidades del dominio y los DTOs.
    /// AutoMapper escaneará este ensamblado en busca de clases que hereden de Profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo de Entidad a DTO (para operaciones de lectura)
            CreateMap<Empleado, EmpleadoVerDto>();

            // Mapeo de DTO a Entidad (para operaciones de escritura/actualización)
            CreateMap<EmpleadoCrearDto, Empleado>();
            CreateMap<EmpleadoActualizarDto, Empleado>();
        }
    }
}