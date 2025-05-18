using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class EscolaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da escola é obrigatório.")]
        [DisplayName("Nome da Escola")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }
        public EnderecoViewModel? Endereco { get; set; }
        public List<SupervisorEscolaViewModel>? Supervisores { get; set; }
        public List<TelefoneViewModel>? Telefones { get; set; } = new List<TelefoneViewModel>();
        public List<TurmaViewModel>? Turmas { get; set; }
        public List<ProfessorViewModel>? Professores { get; set; }
        public List<DiretorViewModel>? Diretores { get; set; }
        public List<AlunoViewModel>? Alunos { get; set; }
        public List<CoordenadorViewModel>? Coordenadores  { get; set; }
        public List<DisciplinaViewModel>? Disciplina { get; set; }
        public List<EquipamentoViewModel>? Equipamentos { get; set; }
        public List<FornecedorViewModel>? Fornecedores { get; set; }
        public List<PrestadorServicoViewModel>? PrestadorServico { get; set; }

        //FILTROS 
        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();

        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        private string NormalizarFiltro(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }

    }
}
