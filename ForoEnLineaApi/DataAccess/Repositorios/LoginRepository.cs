using Dapper;
using ForoEnLineaApi.DataAccess.DataBase;
using ForoEnLineaApi.Entidades.AuthEntities;
using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.Interfaces.Repositories;
using System.Data;

namespace ForoEnLineaApi.DataAccess.Repositorios
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDataBase _database;

        public LoginRepository(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _database = services.First(s => s.GetType() == typeof(SqlDataBase));
        }

        public async Task<LoginEntity> obtenerUsuarioAsync(string Usuario, string Clave)
        {
            using(var cnx = _database.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pUsuario", Usuario);
                parameters.Add("@pClave", Clave);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "sp_obtenerUsuarioLogin",
                    param: parameters,
                    commandTimeout: 0,
                    commandType: CommandType.StoredProcedure))
                {
                    var entity = new LoginEntity();

                    while (reader.Read())
                    {
                        entity = new LoginEntity
                        {
                            IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? default : reader.GetString(reader.GetOrdinal("IdUsuario")),
                            NombreUsuario = reader.IsDBNull(reader.GetOrdinal("NombreUsuario")) ? default : reader.GetString(reader.GetOrdinal("NombreUsuario")),
                            IdPerfil = reader.IsDBNull(reader.GetOrdinal("IdPerfil")) ? default : reader.GetString(reader.GetOrdinal("IdPefil"))
                        };
                    }

                    return entity;
                }
            }
        }
    }
}
