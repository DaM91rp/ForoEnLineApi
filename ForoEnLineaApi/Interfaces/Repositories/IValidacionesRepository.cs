using ForoEnLineaApi.Entidades.AudienciaEntities;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface IValidacionesRepository
    {
        Task<int> ValidarUsuarioAsync(string? usuario, string? clave);
        Task<IEnumerable<AudienciasEntity>> GetListaAudienciasAsync();
    }
}
