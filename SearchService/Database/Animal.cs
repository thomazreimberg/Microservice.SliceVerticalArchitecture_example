using MongoDB.Entities;

namespace SearchService.Database
{
    public class Animal : Entity
    {
        public int PublicId { get; set; }
        public int Age { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public int Weight { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
