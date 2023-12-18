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
        public string Specie { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public char Sex { get; set; }
        public AnimalType AnimalType { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Processed { get; set; } = false;
        public DateTime? ProcessedDate { get; set; }
    }
}
