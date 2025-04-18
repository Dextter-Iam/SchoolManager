using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class AlunoTelefoneConfiguration : IEntityTypeConfiguration<AlunoTelefone>
    {
        public void Configure(EntityTypeBuilder<AlunoTelefone> builder)
        {
            builder.ToTable("AlunoTelefone");
            builder.HasKey(at => new { at.AlunoId, at.TelefoneId });

            builder.HasOne(at => at.Aluno)
                   .WithMany(a => a.AlunoTelefone)
                   .HasForeignKey(at => at.AlunoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(at => at.Telefone)
                   .WithMany(t => t.Alunos)
                   .HasForeignKey(at => at.TelefoneId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(at => at.DDD)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.Property(at => at.Numero)
                   .IsRequired()
                   .HasMaxLength(12);
        }
    }
}
