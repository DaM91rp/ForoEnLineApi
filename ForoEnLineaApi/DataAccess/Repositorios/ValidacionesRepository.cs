using Dapper;
using ForoEnLineaApi.DataAccess.DataBase;
using ForoEnLineaApi.Entidades.AudienciaEntities;
using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.Interfaces.Repositories;
using System.Data;

namespace ForoEnLineaApi.DataAccess.Repositorios
{
    public class ValidacionesRepository : IValidacionesRepository
    {
        private readonly IDataBase _database;

        public ValidacionesRepository(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _database = services.First(s => s.GetType() == typeof(SqlDataBase));
        }
        public async Task<int> ValidarUsuarioAsync(string? usuario, string? clave)
        {
            using (var cnx = _database.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pUsuario", usuario);
                parameters.Add("@pClave", clave);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "sp_validarUsuario",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    int result = 0;

                    while (reader.Read())
                    {
                        result = reader.IsDBNull(reader.GetOrdinal("respuesta")) ? default : reader.GetInt32(reader.GetOrdinal("respuesta"));
                    }

                    return result;
                }
            }
        }

        public async Task<IEnumerable<AudienciasEntity>> GetListaAudienciasAsync()
        {
            using(var cnx = _database.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                using(var reader = await cnx.ExecuteReaderAsync(
                    "sp_ObtenerListaAudiencias",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    var result = new List<AudienciasEntity>();

                    while (reader.Read())
                    {
                        var entity = new AudienciasEntity
                        {
                            Nombre = reader.IsDBNull(reader.GetOrdinal("NombreAudiencia")) ? default : reader.GetString(reader.GetOrdinal("NombreAudiencia")).ToString()
                        };

                        result.Add(entity);
                    }

                    return result;
                }
            }
        }
    }
}
