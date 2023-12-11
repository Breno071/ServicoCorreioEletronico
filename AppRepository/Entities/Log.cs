using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Entities
{
    [Table("TB_LOG")]
    public class Log
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = string.Empty;
        public DateTime DtInclusao { get; set; } = DateTime.Now;
        public LogType LogType { get; set; }
    }

    public enum LogType
    {
        Info,
        Error
    }
}
