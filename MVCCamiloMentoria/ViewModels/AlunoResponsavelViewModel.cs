using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{[NotMapped]
    public class AlunoResponsavelViewModel
    {
        public int AlunoId { get; set; }
        public AlunoViewModel? Aluno { get; set; }

        public int ResponsavelId { get; set; }
        public ResponsavelViewModel? Responsavel { get; set; }
    }
}
