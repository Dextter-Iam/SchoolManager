namespace MVCCamiloMentoria.Models
{
    public class Aula
    {
        public int Id { get; set; }

        public DateTime HorarioInicio { get; set; }

        public DateTime HorarioFim {  get; set; }

        public int ProfessorId { get; set; }

        public Professor Professor { get; set; } 

        public int TurmaId { get; set; }

        public Turma Turma { get; set; }

        public bool ConfirmacaoPresenca {  get; set; }

    }
}
