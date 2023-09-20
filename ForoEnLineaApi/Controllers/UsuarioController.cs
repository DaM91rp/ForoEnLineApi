using ForoEnLineaApi.Entidades.UsuarioEntities;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.Servicios.UsuarioService.CrearUsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForoEnLineaApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly ICryptography _cryptography;
        private readonly IUsuarioRepositiry _usuarioRepositiry;

        public UsuarioController(ICryptography cryptography, IUsuarioRepositiry usuarioRepositiry)
        {
            _cryptography = cryptography;
            _usuarioRepositiry = usuarioRepositiry;
        }


        [HttpPost("crearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioCommand request)
        {
            
            CrearUsuarioEntity cue = new CrearUsuarioEntity();
            cue.IdUsuario =  request.IdUsuario;
            cue.NombreUsuario = request.NombreUsuario;
            cue.CorreoElectronico = request.CorreoElectronico;
            if (request.Clave != null)
            {
                cue.Clave = _cryptography.Encrypt(request.Clave);
            }

            await _usuarioRepositiry.CrearUsuarioAsync(cue);

            return Ok();
        }
    }
}
