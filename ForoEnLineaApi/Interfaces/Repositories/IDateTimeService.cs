namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime NowUtc { get; }
    }
}
