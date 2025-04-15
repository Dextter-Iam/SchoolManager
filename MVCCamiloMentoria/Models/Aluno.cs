using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? EmailEscolar { get; set; }
        public DateTime AnoInscricao { get; set; }
        public bool BolsaEscolar { get; set; }
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }
        public int TelefoneId { get; set; }
        public Telefone? Telefone { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
        public List<AlunoTelefone>? Telefones { get; set; }
        public List<Responsavel>? Responsaveis { get; set; }
        public List<Aula>? Aulas { get; set; }
    }
}
