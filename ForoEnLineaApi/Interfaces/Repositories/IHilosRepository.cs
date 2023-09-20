using ForoEnLineaApi.Entidades.HiloEntities;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface IHilosRepository
    {
        Task CrearHiloAsync(CrearHiloEntity creaHiloEntity);
        Task<IEnumerable<HilosListaEntity>> ListaHilosAsync(HiloIdListaEntity hiloIdListaEntity);
    }
}
