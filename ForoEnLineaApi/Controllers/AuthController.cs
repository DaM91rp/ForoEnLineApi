using AutoMapper;
using ForoEnLineaApi.Infrasestructure;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.Servicios.AuthService.LoginService;
using ForoEnLineaApi.Servicios.TokenService;
using ForoEnLineaApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForoEnLineaApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IValidacionesRepository _validacionesRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly ICryptography _cryptography;
        private readonly IDateTimeService _dateTimeService;

        public AuthController(IConfiguration configuration, IMapper mapper, IValidacionesRepository validacionesRepository, ILoginRepository loginRepository, 
            ICryptography cryptography, IDateTimeService dateTimeService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _validacionesRepository = validacionesRepository;
            _loginRepository = loginRepository;
            _cryptography = cryptography;
            _dateTimeService = dateTimeService;

        }

        [HttpPost]
        [Route("obtenerUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginCommand request)
        {
            LoginDTO login = new LoginDTO();
            string? sClave = string.Empty;

            if (!string.IsNullOrEmpty(request.Clave))
            {
                sClave = _cryptography.Encrypt(request.Clave);
            }

            int respuesta = await _validacionesRepository.ValidarUsuarioAsync(request.Usuario, sClave);
            if (respuesta == 1)
            {
                var result = await _loginRepository.obtenerUsuarioAsync(request.Usuario, sClave);

                return _mapper.Map<LoginDTO>(result);
            }
            else
            {
                throw new BadHttpRequestException("PI PI PI no tiene una cuenta con nosotros");
            }
        }

    }
}
