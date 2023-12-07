using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utills.Models;

namespace AppRepository.Entities
{
    public class PendentEmail
    {
        [Key]
        public long Id { get; set; }
        public Email Email { get; set; } = new Email();
        public bool Processed { get; set; } = false;
        public DateTime? ProcessedDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
