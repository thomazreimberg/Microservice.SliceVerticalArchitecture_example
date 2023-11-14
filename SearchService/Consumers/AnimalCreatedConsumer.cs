using AutoMapper;
using Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Database;

namespace SearchService.Consumers
{
    public class AnimalCreatedConsumer
    {
        private readonly IMapper _mapper;

        public AnimalCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AnimalCreated> animalCreated)
        {
            Console.WriteLine("Consuming animal created " + animalCreated.Message.Id);

            var animal = _mapper.Map<Animal>(animalCreated.Message);

            await animal.SaveAsync();
        }
    }
}
