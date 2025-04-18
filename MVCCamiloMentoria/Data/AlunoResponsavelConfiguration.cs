using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data.Configurations
{
    public class AlunoResponsavelConfiguration : IEntityTypeConfiguration<AlunoResponsavel>
    {
        public void Configure(EntityTypeBuilder<AlunoResponsavel> builder)
        {
            builder.HasKey(ar => new { ar.AlunoId, ar.ResponsavelId });

            builder.HasOne(ar => ar.Aluno)
                   .WithMany(a => a.AlunoResponsavel)
                   .HasForeignKey(ar => ar.AlunoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ar => ar.Responsavel)
                   .WithMany(r => r.AlunoResponsavel)
                   .HasForeignKey(ar => ar.ResponsavelId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AlunoResponsavel");
        }
    }
}
