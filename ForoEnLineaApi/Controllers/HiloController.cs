using AutoMapper;
using ForoEnLineaApi.Entidades.HiloEntities;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.Servicios.HiloService;
using ForoEnLineaApi.Servicios.HiloService.ObtenerListaHilosService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForoEnLineaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HiloController : ControllerBase
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IMapper _mapper;

        public HiloController(IHilosRepository hilosRepository, IMapper mapper)
        {
            _hilosRepository = hilosRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("crearHilo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CrearHiloAsync([FromBody] CrearHiloCommand request)
        {
            CrearHiloEntity che = new CrearHiloEntity();
            che.Titulo = request.Titulo;
            che.Cuerpo = request.Cuerpo;
            che.UsuarioId = request.UsuarioId;

            await _hilosRepository.CrearHiloAsync(che);
            return Ok();
        }

        [HttpPost]
        [Route("listaHilos")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ListaHilosDTO>> ListadoHilosAsync([FromBody] ListaHilosCommand request)
        {
            HiloIdListaEntity che = new HiloIdListaEntity();
            che.IdUsuario = request.IdUsuario;


            var result = await _hilosRepository.ListaHilosAsync(che);
            return _mapper.Map<IEnumerable<ListaHilosDTO>>(result);
        }
    }
}
