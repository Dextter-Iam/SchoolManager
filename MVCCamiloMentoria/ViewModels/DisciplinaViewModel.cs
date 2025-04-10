using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCCamiloMentoria.ViewModels
{
    public class DisciplinaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Campo{0} é obrigatório!")]
        public string? Nome { get; set; }
    }
}
