namespace MVCCamiloMentoria.Models
{
    public class Escola
    {
        public int EscolaId { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        
        public List<Turma> Turmas { get; set; } = new List<Turma>();
        public List<Professor> Professores { get; set; } = new List<Professor>();
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
        public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
        public List<PrestadorServico> PrestadorServico { get; set; } = new List<PrestadorServico>();
    }
}