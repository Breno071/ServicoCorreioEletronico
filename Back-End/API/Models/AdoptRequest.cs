using API.Enums;

namespace API.Models
{
    public sealed class AdoptRequest
    {
        public string Adopter { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public AnimalType AnimalType { get; set; }
    }
}
