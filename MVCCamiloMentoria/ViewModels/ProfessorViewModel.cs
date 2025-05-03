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
        public string? Nome { get; set; }

        public byte[]? Foto { get; set; }
        public IFormFile? FotoUpload { get; set; }
        public List<TelefoneViewModel>? Telefones { get; set; }

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }
        public EscolaViewModel? Escola { get; set; }
        public int Matricula {  get; set; }
        public List<AulaViewModel>? Aulas { get; set; }
        public List<DisciplinaViewModel>? Disciplina { get; set; }
        public List<ProfessorTurmaViewModel>? Turmas { get; set; }
        public List<ProfessorDisciplinaViewModel>? Disciplinas { get; set; }
        public List<int>? TurmaIds { get; set; } = new List<int>();
        public List<int>? DisciplinaIds { get; set; } = new List<int>(); 
    }

}
