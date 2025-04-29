using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class TurmaViewModel
    {
        public int TurmaId { get; set; }
        public string? NomeTurma {  get; set; }
        public int AnoLetivo { get; set; }
        public string? Turno { get; set; }

        [ForeignKey("EscolaId")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Aula>? Aulas { get; set; } 
        public List<Aluno>? Alunos  { get; set; } 
        public List<ProfessorTurma>? Professores { get;  set; }
        public ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; } = new List<TurmaDisciplina>();

    }
}
