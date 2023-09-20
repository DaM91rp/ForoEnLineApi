namespace ForoEnLineaApi.Entidades.TokenEntities
{
    public class CrearTokenUsuarioEntity
    {
        public Guid Identificador { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public string Owner { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
