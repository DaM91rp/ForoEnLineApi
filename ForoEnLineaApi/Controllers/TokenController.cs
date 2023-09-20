using AutoMapper;
using ForoEnLineaApi.Entidades.AuthEntities;
using ForoEnLineaApi.Entidades.TokenEntities;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.Servicios.AuthService.LoginService;
using ForoEnLineaApi.Servicios.TokenService;
using ForoEnLineaApi.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ForoEnLineaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenRepository _tokenRepository;
        private readonly IJwtService _jwtService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography;
        private readonly IValidacionesRepository _validacionesRepository;
        private readonly AppSettings _appSettings;

        public TokenController(IOptions<JwtSettings> jwtSettings,
            ITokenRepository tokenRepository, IJwtService jwtService, IDateTimeService dateTimeService,
            ILoginRepository loginRepository, IMapper mapper, ICryptography cryptography,
            IValidacionesRepository validacionesRepository, IOptions<AppSettings> appSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _tokenRepository = tokenRepository;
            _jwtService = jwtService;
            _dateTimeService = dateTimeService;
            _loginRepository = loginRepository;
            _mapper = mapper;
            _cryptography = cryptography;
            _validacionesRepository = validacionesRepository;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("crearToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CrearTokenUsuarioDTO>> CrearTokenUsuaioAsync([FromBody] CrearTokenUsuarioCommand request, [FromHeader(Name = "Client-Id")] Guid clientId)
        {
            request.UserCliId = clientId;

            LoginDTO login = new LoginDTO();
            string? sClave = string.Empty;

            // decodear el usuario y la contraseña

            //var passwordBinay = System.Convert.FromBase64String(request.Password);
            //var passwordString = System.Text.Encoding.UTF8.GetString(passwordBinay);

            //var userBinay = System.Convert.FromBase64String(request.UserName);
            //var userString = System.Text.Encoding.UTF8.GetString(userBinay);

            if (!string.IsNullOrEmpty(request.Password))
            {
                sClave = _cryptography.Encrypt(request.Password);
            }
            int respuesta = await _validacionesRepository.ValidarUsuarioAsync(request.UserName, sClave);
            if (respuesta == 1)
            {

                var listaAudiencias = new List<string>();

                if (_jwtSettings.EnableAudiences)
                {
                    var responseAudience = await _validacionesRepository.GetListaAudienciasAsync();
                    foreach (var item in responseAudience)
                    {
                        listaAudiencias.Add(item.Nombre);
                    }
                }

                var usuarioEntity = await _loginRepository.obtenerUsuarioAsync(request.UserName, sClave);
                var crearTokenUsuarioDTO = new CrearTokenUsuarioDTO();
                var accesToken = await GenerateToken(usuarioEntity, _dateTimeService.NowUtc.AddSeconds(_jwtSettings.ExpiresInSeconds), listaAudiencias);
                var expiresIn = _jwtSettings.ExpiresInSeconds;
                var TokenType = _jwtSettings.TokenType;

                crearTokenUsuarioDTO.AccessToken = accesToken;
                crearTokenUsuarioDTO.ExpiresIn = expiresIn;
                crearTokenUsuarioDTO.TokenType = TokenType;

                return crearTokenUsuarioDTO;
            }
            else
            {
                throw new BadHttpRequestException("PI PI PI no se pudo crear el token");
            }

        }

        private async Task<string> GenerateToken(LoginEntity loginEntity, DateTime expiresUtc, List<string> listaAudiencias)
        {
            var claims = new List<Claim>
            {
                new Claim("identifier", loginEntity.IdUsuario ?? "")
            };

            foreach (var auienciad in listaAudiencias)
            {
                claims.Add(new Claim("aud", auienciad ?? ""));

            }

            var token = _jwtService.Generate(claims.ToArray(), expiresUtc);

            var crearTokenUsuarioEntity = new CrearTokenUsuarioEntity
            {
                Identificador = Guid.NewGuid(),
                Owner = loginEntity.IdUsuario,
                Tipo = _jwtSettings.TokenType,
                Valor = token,
                FechaExpiracion = expiresUtc
            };

            await _tokenRepository.CrearTokenUsuarioAsync(crearTokenUsuarioEntity);

            return token;
        }


    }
}
