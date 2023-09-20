using ForoEnLineaApi.Entidades.UsuarioEntities;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface IUsuarioRepositiry
    {
        Task CrearUsuarioAsync(CrearUsuarioEntity crearUsuarioEntity);
    }
}
