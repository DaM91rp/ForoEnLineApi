namespace ForoEnLineaApi.Servicios.TokenService
{
    public class CrearTokenUsuarioCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? UserCliId { get; set; }
    }
}
