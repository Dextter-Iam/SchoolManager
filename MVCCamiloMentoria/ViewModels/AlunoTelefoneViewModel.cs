using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class AlunoTelefoneViewModel
    {
        public int TelefoneId { get; set; }
        public int AlunoId { get; set; }
        public AlunoViewModel? Aluno { get; set; } 
        public TelefoneViewModel? Telefone { get; set; }
    }
}
