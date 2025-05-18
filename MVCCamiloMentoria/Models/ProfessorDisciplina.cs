using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class ProfessorDisciplina
    {
        public int ProfessorDisciplinaId { get; set; }

        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina? Disciplina { get; set; }
    }
}
