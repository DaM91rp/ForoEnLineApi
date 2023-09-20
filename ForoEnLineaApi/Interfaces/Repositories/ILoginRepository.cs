using ForoEnLineaApi.Entidades.AuthEntities;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<LoginEntity> obtenerUsuarioAsync(string Usuario, string Clave);
    }
}
