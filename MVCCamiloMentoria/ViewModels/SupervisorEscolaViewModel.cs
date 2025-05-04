using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class SupervisorEscolaViewModel
    {
        public int SupervisorEscolaId { get; set; }

        public int SupervisorId { get; set; }
        public int EscolaId { get; set; }
        public SupervisorViewModel? Supervisor { get; set; }
        public EscolaViewModel? Escola { get; set; }
    }
}
