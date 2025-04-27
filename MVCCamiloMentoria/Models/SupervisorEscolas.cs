using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class SupervisorEscola
    {
        public int SupervisorEscolaId { get; set; }

        public int SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public Supervisor? Supervisor { get; set; }

        public int EscolaId { get; set; }
        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
    }
}
