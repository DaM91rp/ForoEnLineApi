using ForoEnLineaApi.Entidades.TokenEntities;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task CrearTokenUsuarioAsync(CrearTokenUsuarioEntity crearTokenUsuarioEntity);
    }
}
