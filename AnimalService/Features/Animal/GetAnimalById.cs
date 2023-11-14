using AnimalService.Contract.Animal;
using AnimalService.Database;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Features.Animal
{
    public static class GetAnimalById
    {
        public class Query : IRequest<AnimalDto>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Query, AnimalDto>
        {
            private readonly AnimalDbContext _dbContext;

            public Handler(AnimalDbContext animalDbContext)
            {
                _dbContext = animalDbContext;
            }

            public async Task<AnimalDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var animalResponse = await _dbContext.Animals
                    .Where(x => x.Id == request.Id)
                    .Select(s => new AnimalDto()
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

                    }).FirstOrDefaultAsync();

                if (animalResponse is null)
                    throw new Exception("Animal was not found.");

                return animalResponse;
            }
        }
    }

    public class GetAllAnimalByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/animals/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetAnimalById.Query { Id = id };

                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
