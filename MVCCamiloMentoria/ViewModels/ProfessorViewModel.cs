using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class ProfessorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Foto")]
        public byte[]? Foto { get; set; }

        [DisplayName("Foto de Perfil")]
        public IFormFile? FotoUpload { get; set; }

        [DisplayName("Telefones")]
        public List<TelefoneViewModel>? Telefones { get; set; }

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [Required(ErrorMessage = "A escola é obrigatória.")]
        [DisplayName("Escola")]
        public int EscolaId { get; set; }

        public EscolaViewModel? Escola { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [DisplayName("Matrícula")]
        public int Matricula { get; set; }

        [DisplayName("Aulas")]
        public List<AulaViewModel>? Aulas { get; set; }

        [DisplayName("Disciplinas")]
        public List<DisciplinaViewModel>? Disciplina { get; set; }

        [DisplayName("Turmas")]
        public List<ProfessorTurmaViewModel>? Turmas { get; set; }

        [DisplayName("Disciplinas do Professor")]
        public List<ProfessorDisciplinaViewModel>? Disciplinas { get; set; }

        [DisplayName("Turmas Selecionadas")]
        public List<int>? TurmaIds { get; set; } = new List<int>();

        [DisplayName("Disciplinas Selecionadas")]
        public List<int>? DisciplinaIds { get; set; } = new List<int>();
    }
}
