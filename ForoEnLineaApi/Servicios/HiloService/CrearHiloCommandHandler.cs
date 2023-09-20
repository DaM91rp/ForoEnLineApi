using AutoMapper;
using ForoEnLineaApi.Entidades.HiloEntities;
using ForoEnLineaApi.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForoEnLineaApi.Servicios.HiloService
{
    public class CrearHiloCommandHandler
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IMapper _mapper;

        public CrearHiloCommandHandler(IHilosRepository hilosRepository, IMapper mapper)
        {
            _hilosRepository = hilosRepository;
            _mapper = mapper;
        }

        public async Task<Unit> CrearHiloAsyncFunction(CrearHiloCommand request)
        {
            CrearHiloEntity che = new CrearHiloEntity();
            che.Titulo = request.Titulo;
            che.Cuerpo = request.Cuerpo;
            che.UsuarioId = request.UsuarioId;

            await _hilosRepository.CrearHiloAsync(che);
            return Unit.Value;
        }
    }
}
