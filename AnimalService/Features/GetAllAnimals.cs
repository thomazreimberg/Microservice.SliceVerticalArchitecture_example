using AnimalService.Contract.Animal;
using AnimalService.Database;
using Carter;
using MediatR;
using static AnimalService.Features.GetAllAnimals;

namespace AnimalService.Features
{
    public static class GetAllAnimals
    {
        public class Query : IRequest<List<AnimalDto>>
        {
        }
    }

    internal sealed class Handler : IRequestHandler<Query, List<AnimalDto>>
    {
        private readonly AnimalDbContext _dbContext;

        public Handler(AnimalDbContext animalDbContext)
        {
            _dbContext = animalDbContext;
        }

        public async Task<List<AnimalDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var 
        }
    }

    public class GetAllAnimalsEndpoint : ICarterModule
    {
        public GetAllAnimalsEndpoint()
        {
            
        }
    }
}
