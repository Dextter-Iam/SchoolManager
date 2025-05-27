using MVCCamiloMentoria.Models;

namespace DateInject
{
    public class Importador
    {
        private readonly EscolaContext _context;
        private readonly PlanilhaHelper _planilhaHelper;

        public Importador(EscolaContext context, PlanilhaHelper planilhaHelper)
        {
            _context = context;
            _planilhaHelper = planilhaHelper;
        }

        public void Executar()
        {
            string caminhoPlanilha = @"C:\Users\camilo.eduardo\Documents\Camilo.Eduardo\alunos_teste.xlsx";

            Console.WriteLine("Lendo a planilha...");
            var alunos = _planilhaHelper.LerAlunos(caminhoPlanilha);

            Console.WriteLine($"Total de alunos encontrados: {alunos.Count}");

            _context.Aluno.AddRange(alunos);
            _context.SaveChanges();

            Console.WriteLine("Importação concluída com sucesso.");
        }
    }
}
