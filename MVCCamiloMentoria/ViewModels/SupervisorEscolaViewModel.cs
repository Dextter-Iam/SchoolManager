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

        public SupervisorViewModel? Supervisor { get; set; }

        public EscolaViewModel? Escola { get; set; }
    }
}
