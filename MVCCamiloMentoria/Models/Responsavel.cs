namespace MVCCamiloMentoria.Models
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public List<Aluno>? Alunos { get; set; }


    }
}
