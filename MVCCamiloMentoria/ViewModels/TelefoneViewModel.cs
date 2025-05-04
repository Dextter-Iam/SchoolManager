using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class TelefoneViewModel
    {
        public int Id { get; set; }
        public int DDD { get; set; }
        public int Numero { get; set; }
        public int EscolaId { get; set; }
        public EscolaViewModel? Escola { get; set; }
        public PrestadorServicoViewModel? PrestadorServico { get; set; }
        public FornecedorViewModel? Fornecedor { get; set; }
        public DiretorViewModel? Diretor { get; set; }
        public CoordenadorViewModel? Coordenador { get; set; }
        public ResponsavelViewModel? Responsavel { get; set; }
        public SupervisorViewModel? Supervisor { get; set; }
        public ProfessorViewModel? Professor { get; set; }
        public List<AlunoTelefoneViewModel>? Alunos { get; set; }
    }
}
