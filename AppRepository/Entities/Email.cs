using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Entities
{
    [Table("TB_EMAIL")]
    public class Email
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(255)]
        public string From { get; set; } = string.Empty;
        [MaxLength(255)]
        public string FromName { get; set; } = string.Empty;
        [MaxLength(255)]
        public string To { get; set; } = string.Empty;
        [MaxLength(255)]
        public string ToName { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        [MaxLength(255)]
        public string FromPassword { get; set; } = "vcxi lfqj hkll icst";
        public List<string> Attachments { get; set; } = [];
    }
}
