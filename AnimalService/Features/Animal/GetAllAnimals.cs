using AnimalService.Contract.Animal;
using AnimalService.Database;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static AnimalService.Features.Animal.GetAllAnimals;

namespace AnimalService.Features.Animal
{
    public static class GetAllAnimals
    {
        public class Query : IRequest<List<AnimalDto>>
        {
        }

        internal sealed class Handler : IRequestHandler<Query, List<AnimalDto>>
        {
            public readonly AnimalDbContext _dbContext;

            public Handler(AnimalDbContext animalDbContext)
            {
                _dbContext = animalDbContext;
            }

            public async Task<List<AnimalDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var animalResponse = await _dbContext.Animals.Select(s => new AnimalDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    Breed = s.Breed,
                    Color = s.Color,
                    Description = s.Description,
                    Sex = s.Sex,
                    CreatedAt = s.CreatedAt,
                    Status = s.Status,
                    Type = s.Type,
                    Weight = s.Weight,
                    PublicId = s.PublicId,
                    CoverImageUrl = s.CoverImageUrl,
                    UpdatedAt = s.UpdatedAt,

                }).ToListAsync();

                return animalResponse;
            }
        }
    }

    public class GetAllAnimalsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/animals", async (ISender sender) =>
            {
                var query = new GetAllAnimals.Query { };

                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
