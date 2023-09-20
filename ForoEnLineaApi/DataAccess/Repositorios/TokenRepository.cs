using Dapper;
using ForoEnLineaApi.Entidades.TokenEntities;
using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.Interfaces.Repositories;
using System.Data;

namespace ForoEnLineaApi.DataAccess.Repositorios
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDataBase _database;
        private readonly IDateTimeService _dateTimeService;

        public TokenRepository(IDataBase database, IDateTimeService dateTimeService)
        {
            _database = database;
            _dateTimeService = dateTimeService;
        }

        public async Task CrearTokenUsuarioAsync(CrearTokenUsuarioEntity crearTokenUsuarioEntity)
        {
            using(var cnx = _database.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@identificador", crearTokenUsuarioEntity.Identificador);
                parameters.Add("@tipo", crearTokenUsuarioEntity.Tipo);
                parameters.Add("@valor", crearTokenUsuarioEntity.Valor);
                parameters.Add("@owner", crearTokenUsuarioEntity.Owner);
                parameters.Add("@fechaExpiracion", crearTokenUsuarioEntity.FechaExpiracion);
                parameters.Add("@fechaCreacion", _dateTimeService.Now);

                await cnx.ExecuteAsync(
                    "usp_TokenInsertar",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                    );
            }

        }
    }
}
