using Dapper;
using ForoEnLineaApi.DataAccess.DataBase;
using ForoEnLineaApi.Entidades.UsuarioEntities;
using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.Interfaces.Repositories;
using System.Data;

namespace ForoEnLineaApi.DataAccess.Repositorios
{
    public class UsuarioRepositiry : IUsuarioRepositiry
    {
        private readonly IDataBase _dataBase;

        public UsuarioRepositiry(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<IDataBase>();
            _dataBase = services.First(s => s.GetType() == typeof(SqlDataBase));
        }

        public async Task CrearUsuarioAsync(CrearUsuarioEntity crearUsuarioEntity)
        {
            using (var cnx = _dataBase.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pIdUsuario", crearUsuarioEntity.IdUsuario);
                parameters.Add("@pNombreUsuario", crearUsuarioEntity.NombreUsuario);
                parameters.Add("@pCorreoElectronico", crearUsuarioEntity.CorreoElectronico);
                parameters.Add("@pClave", crearUsuarioEntity.Clave);
                parameters.Add("@pIdPerfil", crearUsuarioEntity.IdPerfil);
                var result = await cnx.ExecuteAsync(
                    "sp_insertarUsuario",
                    param: parameters,
                    commandTimeout: 0,
                    commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
