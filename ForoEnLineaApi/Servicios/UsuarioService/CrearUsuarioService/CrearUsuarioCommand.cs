using System.Reflection.Metadata.Ecma335;

namespace ForoEnLineaApi.Servicios.UsuarioService.CrearUsuarioService
{
    public class CrearUsuarioCommand
    {
        public string? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Clave { get; set; }
        public string? IdPerfil { get; set; }
    }
}
