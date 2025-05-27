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
                    var aluno = new Aluno
                    {
                        Nome = row.Cell(1).GetString(),
                        Foto = System.IO.File.Exists(row.Cell(2).GetString())
                            ? System.IO.File.ReadAllBytes(row.Cell(2).GetString())
                            : null,
                        DataNascimento = DateTime.ParseExact(row.Cell(3).GetString(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        EmailEscolar = row.Cell(4).GetString(),
                        AnoInscricao = DateTime.ParseExact(row.Cell(5).GetString(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        BolsaEscolar = row.Cell(6).GetBoolean(),
                        TurmaId = row.Cell(7).GetValue<int>(),
                        EnderecoId = row.Cell(8).GetValue<int>(),
                        Excluido = row.Cell(9).GetBoolean(),
                        EscolaId = row.Cell(10).GetValue<int>(),
                        EstadoId = row.Cell(11).GetValue<int>()
                    };

                    alunos.Add(aluno);
                }
            }

            return alunos;
        }
    }
}
