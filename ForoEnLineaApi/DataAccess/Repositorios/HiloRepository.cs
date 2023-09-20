using Dapper;
using ForoEnLineaApi.Entidades.HiloEntities;
using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.DataAccess.DataBase;
using System.Data;
using System.Runtime.CompilerServices;

namespace ForoEnLineaApi.DataAccess.Repositorios
{
    public class HiloRepository : IHilosRepository
    {
        private readonly IDataBase _dataBase;

        public HiloRepository(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
        }

        public async Task CrearHiloAsync(CrearHiloEntity creaHiloEntity)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pTitulo", creaHiloEntity.Titulo);
                parameters.Add("@pCuerpo", creaHiloEntity.Cuerpo);
                parameters.Add("@pUsuarioId", creaHiloEntity.UsuarioId);
                var result = await cnx.ExecuteAsync(
                    "sp_crearHilo",
                    param: parameters,
                    commandTimeout: 0,
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

        public async Task<IEnumerable<HilosListaEntity>> ListaHilosAsync(HiloIdListaEntity hiloIdListaEntity)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pUsuarioId", hiloIdListaEntity.IdUsuario);
                using(var reader = await cnx.ExecuteReaderAsync(
                    "sp_obtenerHilosPorUsuario",
                    param: parameters,
                    commandTimeout: 0,
                    commandType: CommandType.StoredProcedure))
                {
                    var result = new List<HilosListaEntity>();

                    while (reader.Read())
                    {
                        var entity = new HilosListaEntity
                        {
                            Titulo = reader.IsDBNull(reader.GetOrdinal("Titulo")) ? default : reader.GetString(reader.GetOrdinal("Titulo")),
                            Cuerpo = reader.IsDBNull(reader.GetOrdinal("Cuerpo")) ? default : reader.GetString(reader.GetOrdinal("Cuerpo"))
                        };

                        result.Add(entity);
                    }

                    return result;
                }
            }
        }
    }
}
