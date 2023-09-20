using System.Globalization;

namespace ForoEnLineaApi.Utils
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public long ExpiresInSeconds { get; set; }
        public string TokenType { get; set; }
        public bool EnableAudiences { get; set; }
        public bool ValidateAudience { get; set; }
        public bool Enabled { get; set; } 
    }
}
