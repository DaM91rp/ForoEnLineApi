using System.Runtime.CompilerServices;

namespace ForoEnLineaApi.Utils
{
    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public string ApplicationDisplayName { get; set; }
        public string ApplicationId { get; set; }
        public long LongRequestTimeMilliSeconds { get; set; }
        public long SlidingExpirationCacheTimeSeconds { get; set; }
        public string Titulo { get; set; }
    }
}
