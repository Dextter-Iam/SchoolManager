using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.ViewModels
{
    public class EscolaViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public List<Turma>? Turmas { get; set; } 
        public List<Professor>? Professores { get; set; } 
        public List<Aluno>? Alunos { get; set; } 
        public List<Disciplina>? Disciplina { get; set; } 
        public List<Equipamento>? Equipamentos { get; set; } 
        public List<Fornecedor>? Fornecedores { get; set; } 
        public List<PrestadorServico>? PrestadorServico { get; set; }
    }
}