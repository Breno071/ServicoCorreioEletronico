using System.ComponentModel.DataAnnotations;

namespace AppRepository.Entities
{
    public class PendentEmail
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Email Email { get; set; } = new Email();
        public bool Processed { get; set; } = false;
        public DateTime? ProcessedDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
