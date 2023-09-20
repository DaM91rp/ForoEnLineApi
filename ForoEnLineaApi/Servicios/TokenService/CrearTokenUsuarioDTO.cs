using AutoMapper;
using ForoEnLineaApi.Entidades.TokenEntities;
using ForoEnLineaApi.Interfaces.Mapping;
using Newtonsoft.Json;

namespace ForoEnLineaApi.Servicios.TokenService
{
    public class CrearTokenUsuarioDTO : IMapFrom<CrearTokenUsuarioEntity>
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CrearTokenUsuarioEntity, CrearTokenUsuarioDTO>();
        }
    }
}
