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

        [Required(ErrorMessage = "Selecione um estado.")]
        [DisplayName("Estado (UF)")]

        public List<EstadoViewModel>? Estados { get; set; }
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

    }
}
