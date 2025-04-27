using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class ProfessorViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Telefone>? Telefones { get; set; }

        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
        public int Matricula {  get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Aula>? Aulas { get; set; }
        public List<ProfessorTurma>? Turmas { get; internal set; }
        public List<Disciplina>? Disciplinas { get; internal set; }
    }

}
