using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class SupervisorEscolaViewModel
    {
        [Key]
        public int SupervisorEscolaId { get; set; }

        [Key]
        public int SupervisorId { get; set; }

        [Key]
        public int EscolaId { get; set; }

        [ForeignKey("SupervisorId")]
        public SupervisorViewModel? Supervisor { get; set; }

        [ForeignKey("EscolaId")]
        public EscolaViewModel? Escola { get; set; }
    }
}
