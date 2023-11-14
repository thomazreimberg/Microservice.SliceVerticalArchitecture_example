using AnimalService.Contract.Animal;
using AnimalService.Database;
using AnimalService.Database.Entities;
using Carter;
using Mapster;
using MediatR;

namespace AnimalService.Features.Animal
{
    public static class CreateAnimal
    {
        public class Command : IRequest<Guid>
        {
            public Guid Id { get; set; }
            public int Age { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Breed { get; set; } = string.Empty;
            public string Sex { get; set; } = string.Empty;
            public int Weight { get; set; }
            public string Color { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string CoverImageUrl { get; set; } = string.Empty;
            public Status Status { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly AnimalDbContext _dbContext;

            public Handler(AnimalDbContext animalDbContext)
            {
                _dbContext = animalDbContext;
            }

            public async Task<Guid> Handle (Command command, CancellationToken cancellationToken)
            {
                var animal = new Database.Entities.Animal
                {
                    Age = command.Age,
                    Name = command.Name,
                    Type = command.Type,
                    Breed = command.Breed,
                    Sex = command.Sex,
                    Weight = command.Weight,
                    Color = command.Color,
                    Description = command.Description,
                    CoverImageUrl = command.CoverImageUrl,
                    Status = command.Status,
                };

                _dbContext.Add(animal);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return animal.Id;
            }
        }

        public class CreateAnimalEndPoint : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPost("api/animals", async (CreateDto request, ISender sender) =>
                {
                    var command = request.Adapt<CreateAnimal.Command>();
                    var result = await sender.Send(request) ?? throw new Exception("Problem saving changes.");
                });
            }
        }
    }
}
