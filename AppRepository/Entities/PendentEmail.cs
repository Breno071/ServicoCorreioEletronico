using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRepository.Entities
{
    [Table("TB_PENDENT_EMAIL")]
    public class PendentEmail
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Email Email { get; set; } = new Email();
        public bool Processed { get; set; } = false;
        public DateTime? ProcessedDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
