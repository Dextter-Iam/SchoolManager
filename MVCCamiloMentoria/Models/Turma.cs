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
        public int TurmaId { get; set; }
        public string? NomeTurma {  get; set; }
        public int AnoLetivo { get; set; }
        public string? Turno { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Aula>? Aulas { get; set; } 
        public List<Aluno>? Alunos  { get; set; } 
        public List<ProfessorTurma>? Professores { get;  set; }
        public List<Disciplina>? Disciplinas { get;  set; }

    }
}
