using ForoEnLineaApi.Interfaces.Repositories;

namespace ForoEnLineaApi.Infrasestructure
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
