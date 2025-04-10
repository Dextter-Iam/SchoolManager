using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class AlunoTelefoneConfiguration : IEntityTypeConfiguration<AlunoTelefone>
    {
        public void Configure(EntityTypeBuilder<AlunoTelefone> builder)
        {

            builder.ToTable("AlunoTelefone"); 

            builder.HasKey(at => new {at.AlunoId, at.TelefoneId});

            builder.HasOne(at => at.Aluno)
                   .WithMany(a => a.Telefones)
                   .HasForeignKey(at => at.AlunoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(at => at.Telefone)
                   .WithMany(t => t.Alunos)
                   .HasForeignKey(at => at.TelefoneId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}