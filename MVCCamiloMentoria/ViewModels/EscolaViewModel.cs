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

        // Não representa diretamente o modelo de entidade.
        public int EstadoId { get; set; }
        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [DisplayName("Nome da Rua")]
        [StringLength(200)]
        public string? NomeRua { get; set; }

        [Required(ErrorMessage = "O número da rua é obrigatório.")]
        [DisplayName("Número")]
        [Range(1, int.MaxValue, ErrorMessage = "O número da rua deve ser maior que zero.")]
        public int NumeroRua { get; set; }

        [DisplayName("Complemento")]
        [StringLength(150, ErrorMessage = "O complemento deve ter no máximo 150 caracteres.")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [DisplayName("CEP")]
        [Range(1000000, 99999999, ErrorMessage = "CEP inválido.")]
        public int CEP { get; set; }

        public EnderecoViewModel? Endereco { get; set; }

        public List<SupervisorEscolaViewModel>? Supervisores { get; set; }

        public List<TelefoneViewModel>? Telefones { get; set; }

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
