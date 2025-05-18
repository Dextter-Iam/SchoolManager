using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class SupervisorEscola
    {
        [Key]
        public int SupervisorEscolaId { get; set; }

        [Key]
        public int SupervisorId { get; set; }

        [Key]
        public int EscolaId { get; set; }

        [ForeignKey("SupervisorId")]
        public Supervisor? Supervisor { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
    }
}
