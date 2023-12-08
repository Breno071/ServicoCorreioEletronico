using Utills.Enums;

namespace Utills.Models
{
    public sealed class AdoptRequest
    {
        public Adopter Adopter { get; set; } = new();
        public string Breed { get; set; } = string.Empty;
        public AnimalType AnimalType { get; set; }
    }
}
