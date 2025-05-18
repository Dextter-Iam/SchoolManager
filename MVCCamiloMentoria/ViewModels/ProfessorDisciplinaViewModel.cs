using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class ProfessorDisciplinaViewModel
    {
        public int ProfessorDisciplinaId { get; set; }

        public int ProfessorId { get; set; }
        public ProfessorViewModel? Professor { get; set; }

        public int DisciplinaId { get; set; }
        public DisciplinaViewModel? Disciplina { get; set; }
    }
}
