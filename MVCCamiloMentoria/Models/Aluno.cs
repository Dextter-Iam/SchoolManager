using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public byte[]? Foto { get; set; }

        public DateTime DataNascimento { get; set; }

        public string? EmailEscolar { get; set; }

        public DateTime AnoInscricao { get; set; }

        public bool BolsaEscolar { get; set; }

        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }

        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public bool Excluido { get; set; } = false;
        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }

        public int EstadoId { get; set; }
        public List<Estado>? Estado { get; set; }
        public string? NomeResponsavel1 { get; set; }
        public string? Parentesco1 { get; set; }
        public string? NomeResponsavel2 { get; set; }
        public string? Parentesco2 { get; set; }

        public List<AlunoTelefone>? AlunoTelefone { get; set; }

        public List<AlunoResponsavel>? AlunoResponsavel { get; set; }

        public List<Aula>? Aulas { get; set; }
    }
}
