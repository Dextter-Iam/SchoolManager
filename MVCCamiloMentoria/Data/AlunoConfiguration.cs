using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.NomeAluno).HasMaxLength(200);

            builder.HasOne(a => a.Turma)
                .WithMany(a => a.Alunos)
                .HasForeignKey(a => a.TurmaId)
                .HasForeignKey(a =>a.EnderecoId);
                
            builder.ToTable("Aluno");
        }
    }
}
