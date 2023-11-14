using AutoMapper;
using Events;
using SearchService.Database;

namespace SearchService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {

            CreateMap<AnimalCreated, Animal>();
            CreateMap<AnimalUpdated, Animal>();
        }
    }
}
