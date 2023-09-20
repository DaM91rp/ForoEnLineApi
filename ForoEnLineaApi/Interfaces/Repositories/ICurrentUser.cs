namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface ICurrentUser
    {
        string Identidfier { get; }
        string Name { get; }
        string Nombres { get; }
        string ApellidoPaterno { get; }
        string ApellidoMaterno { get; }
        string Username { get; }
        string Email { get; }
        string Celular { get; }
        bool IsAuthenticated { get; }
        string TraceIdentifier { get; }
    }
}
