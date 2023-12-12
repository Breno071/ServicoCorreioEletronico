using AppRepository.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRepository.Entities
{
    [Table("TB_ADOPT_REQUEST")]
    public sealed class AdoptRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Adopter Adopter { get; set; } = new();
        public string Breed { get; set; } = string.Empty;
        public AnimalType AnimalType { get; set; }
    }
}
