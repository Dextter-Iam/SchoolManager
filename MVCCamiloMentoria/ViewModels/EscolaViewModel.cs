using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class EscolaViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? NomeRua { get; set; }
        public int NumeroRua { get; set; }
        public string? Complemento { get; set; }
        public int CEP { get; set; }
        public int? EstadoId { get; set; }

        public Estado? Estado { get; set; }
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
