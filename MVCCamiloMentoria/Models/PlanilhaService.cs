using ClosedXML.Excel;
using MVCCamiloMentoria.Models;
using System.Collections.Generic;
using System.Globalization;

namespace ImportadorDeAlunos.Services
{
    public class PlanilhaService
    {
        public List<Aluno> LerAlunosDaPlanilha(string caminhoArquivo)
        {
            var alunos = new List<Aluno>();

            using (XLWorkbook workbook = new XLWorkbook(caminhoArquivo))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Pula cabeçalho

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
