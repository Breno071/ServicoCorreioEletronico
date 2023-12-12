using AppRepository.Entities;
using AppRepository.Enums;

namespace API.DTOs
{
    public class AdopterRequestDTO
    {
        public AdopterDTO Adopter { get; set; } = new();
        public string Breed { get; set; } = string.Empty;
        public AnimalType AnimalType { get; set; }
    }
}
