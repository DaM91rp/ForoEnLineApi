using AutoMapper;

namespace ForoEnLineaApi.Interfaces.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T),GetType());
    }
}
