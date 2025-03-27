using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Models
{
    public class Turma
    {
        [Key]
        public int TurmaId { get; set; }

        public List<Aluno> Alunos  { get; set; }

        [Required]
        public string? NomeTurma {  get; set; }


        [Required]
        public int AnoLetivo { get; set; }
        public List<ProfessorTurma> Professores { get; internal set; }
    }
}
