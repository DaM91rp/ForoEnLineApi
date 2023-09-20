using AutoMapper;
using ForoEnLineaApi.Entidades.AuthEntities;
using ForoEnLineaApi.Interfaces.Mapping;

namespace ForoEnLineaApi.Servicios.AuthService.LoginService
{
    public class LoginDTO : IMapFrom<LoginEntity>
    {
        public string? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginEntity,LoginDTO>();
        }
    }
}
