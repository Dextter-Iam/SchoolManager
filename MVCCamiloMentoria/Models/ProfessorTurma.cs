namespace MVCCamiloMentoria.Models
{
    public class ProfessorTurma
    {
        public int ProfessorTurmaId { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }
    }
}
