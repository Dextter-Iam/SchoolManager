using ImportadorDeAlunos.Services;
using SeuProjeto.Data; // Ajuste para onde está seu DbContext
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando importação...");

        string caminhoPlanilha = @"C:\caminho\para\sua\planilha.xlsx";

        var planilhaService = new PlanilhaService();
        var alunos = planilhaService.LerAlunosDaPlanilha(caminhoPlanilha);

        Console.WriteLine($"Foram encontrados {alunos.Count} alunos.");

        using (var context = new SeuDbContext())
        {
            context.Alunos.AddRange(alunos);
            context.SaveChanges();
        }

        Console.WriteLine("Importação concluída!");
    }
}
