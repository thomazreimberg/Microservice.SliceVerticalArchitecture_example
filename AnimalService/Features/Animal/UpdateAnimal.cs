using AnimalService.Contract.Animal;
using AnimalService.Database.Entities;
using AnimalService.Database;
using Carter;
using MediatR;
using Mapster;

namespace AnimalService.Features.Animal
{
    public static class UpdateAnimal
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

            public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
            {
                var animal = _dbContext.Animals.FirstOrDefault(x => x.Id == command.Id) ?? throw new Exception("Animal was not found.");

                animal.Id = command.Id;
                animal.Age = command.Age;
                animal.Name = command.Name;
                animal.Type = command.Type;
                animal.Breed = command.Breed;
                animal.Sex = command.Sex;
                animal.Weight = command.Weight;
                animal.Color = command.Color;
                animal.Description = command.Description;
                animal.CoverImageUrl = command.CoverImageUrl;
                animal.Status = command.Status;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return animal.Id;
            }
        }

        public class CreateAnimalEndPoint : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPost("api/animals/{id}", async (Guid id, CreateDto request, ISender sender) =>
                {
                    var command = request.Adapt<CreateAnimal.Command>();
                    command.Id = id;
                    var result = await sender.Send(request) ?? throw new Exception("Problem saving changes.");
                });
            }
        }
    }
}
