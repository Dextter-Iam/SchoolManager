using Microsoft.EntityFrameworkCore;

namespace MVCCamiloMentoria.Models
{
    public class Escola
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public List<Turma>? Turmas { get; set; }
        public List<Coordenador>? Coordenadores { get; set; }
        public List<Diretor>? Diretores { get; set; }
        public List<Professor>? Professores { get; set; }
        public List<Aluno>? Alunos { get; set; }
        public List<Disciplina>? Disciplina { get; set; }
        public List<Equipamento>? Equipamentos { get; set; }
        public List<Fornecedor>? Fornecedores { get; set; }
        public List<PrestadorServico>? PrestadorServico { get; set; }

        public List<SupervisorEscola>? SupervisorEscolas { get; set; }
    }
}
