using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class ResponsavelConfiguration
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.ToTable("Responsavel");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(r => r.Endereco)
                   .WithOne()
                   .HasForeignKey<Responsavel>(s => s.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.Responsavel)
                   .HasForeignKey(e => e.ResponsavelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Alunos)
                   .WithMany(r => r.Responsaveis)
                   .UsingEntity(ra => ra.ToTable("ResponsavelAluno"));

        }
    }
}
