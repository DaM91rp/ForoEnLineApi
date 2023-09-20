using System.Data;

namespace ForoEnLineaApi.Interfaces
{
    public interface IDataBase
    {
        IDbConnection GetConnection();
    }
}
