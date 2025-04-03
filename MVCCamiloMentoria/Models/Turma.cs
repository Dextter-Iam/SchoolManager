using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string? NomeTurma {  get; set; }
        public int AnoLetivo { get; set; }
        public string? Turno { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Aula> Aulas { get; set; } = new List<Aula>();
        public List<Aluno> Alunos  { get; set; } = new List<Aluno>();
        public List<ProfessorTurma> Professores { get;  set; } = new List<ProfessorTurma>();
        public List<Disciplina> Disciplinas { get;  set; } = new List<Disciplina> ();

    }
}
