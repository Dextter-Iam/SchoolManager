using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class DisciplinaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        [Display(Name = "Nome da Disciplina")]
        public string? Nome { get; set; }

        [Display(Name = "Escola")]
        public int EscolaId { get; set; }

        public EscolaViewModel? Escola { get; set; }

        public List<TurmaDisciplinaViewModel>? TurmaDisciplinas { get; set; }
    }
}
