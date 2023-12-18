using AppRepository.Entities;
using AppRepository.Enums;

namespace API.DTOs
{
    public class AdopterRequestDTO
    {
        public AdopterDTO Adopter { get; set; } = new();
        public string Specie { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public char Sex { get; set; } 
        public AnimalType AnimalType { get; set; }
    }
}
