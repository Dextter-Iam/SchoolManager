using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class AlunoTelefoneViewModel
    {
        public AlunoViewModel? Aluno { get; set; }

        public TelefoneViewModel? Telefone { get; set; }

        public int DDD { get; set; }
        public int Numero { get; set; }
    }
}
