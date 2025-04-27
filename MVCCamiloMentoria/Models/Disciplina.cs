using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class Disciplina
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }

        public List<ProfessorDisciplina>? Professores { get; set; }

        public ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; } = new List<TurmaDisciplina>();

        public List<Aula>? Aulas { get; set; } 
    }
}
