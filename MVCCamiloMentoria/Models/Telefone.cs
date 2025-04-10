using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MVCCamiloMentoria.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int DDD { get; set; }
        public int Numero { get; set; }
        public int? EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public int? PrestadorServicoId { get; set; }
        public PrestadorServico? PrestadorServico { get; set; }
        public int? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public int? DiretorId { get; set; }
        public Diretor? Diretor { get; set; }
        public int? CoordenadorId { get; set; }
        public Coordenador? Coordenador { get; set; }
        public int? ResponsavelId { get; set; }
        public Responsavel? Responsavel { get; set; }
        public int? SupervisorId { get; set; }
        public Supervisor? Supervisor { get; set; }
        public int? ProfessorId { get; set; }
        public Professor? Professor { get; set; }
        public List<AlunoTelefone>? Alunos { get; set; }
    }
}
