using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.AlunoId);

            builder.Property(a => a.NomeAluno).HasMaxLength(200);

            builder.HasOne(a => a.Turma)
                .WithMany(c => c.Alunos)
                .HasForeignKey(a => a.TurmaId);

            builder.ToTable("Aluno");
        }
    }
}
