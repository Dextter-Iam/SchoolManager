using ClosedXML.Excel;
using MVCCamiloMentoria.Models;
using System.Globalization;

namespace DateInject
{
    public class PlanilhaHelper
    {
        public List<Aluno> LerAlunos(string caminho)
        {
            var alunos = new List<Aluno>();

            using (var workbook = new XLWorkbook(caminho))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    string[] formatos = { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy" };

                    DateTime dataNascimento;
                    DateTime anoInscricao;

                    DateTime.TryParseExact(row.Cell(2).GetString(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento);
                    DateTime.TryParseExact(row.Cell(4).GetString(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out anoInscricao);

                    var aluno = new Aluno
                    {
                        Nome = row.Cell(1).GetString(),
                        DataNascimento = dataNascimento,
                        EmailEscolar = row.Cell(3).GetString(),
                        AnoInscricao = anoInscricao,
                        BolsaEscolar = row.Cell(5).GetValue<int>() == 1,
                        TurmaId = row.Cell(6).GetValue<int>(),
                        EnderecoId = row.Cell(7).GetValue<int>(),
                        Excluido = row.Cell(8).GetValue<bool>(),
                        EscolaId = row.Cell(9).GetValue<int>(),
                        EstadoId = row.Cell(10).GetValue<int>()
                    };

                    alunos.Add(aluno);
                }
            }

            return alunos;
        }
    }
}
