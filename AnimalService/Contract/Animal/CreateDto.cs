using AnimalService.Database.Entities;

namespace AnimalService.Contract.Animal
{
    public class CreateDto
    {
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
}
