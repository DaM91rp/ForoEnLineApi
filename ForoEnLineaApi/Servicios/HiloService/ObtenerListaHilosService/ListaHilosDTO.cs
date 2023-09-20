using AutoMapper;
using ForoEnLineaApi.Entidades.HiloEntities;
using ForoEnLineaApi.Interfaces.Mapping;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ForoEnLineaApi.Servicios.HiloService.ObtenerListaHilosService
{
    public class ListaHilosDTO : IMapFrom<HilosListaEntity>
    {
        public string? Titulo { get; set; }
        public string? Cuerpo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<HilosListaEntity, ListaHilosDTO>();
        }
    }
}
