using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class PrestadorServicoConfiguration
    {
        public void Configure  (EntityTypeBuilder <PrestadorServico> builder)
        {
            builder.ToTable("PrestadorServico");

            builder.HasKey(ps=>ps.Id);

            builder.Property(ps => ps.CPF)
                   .HasMaxLength(11);

            builder.Property(ps => ps.CNPJ)
                   .HasMaxLength(14);

            builder.Property(ps=>ps.EmpresaNome)
                   .IsRequired()
                   .HasMaxLength(190);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.PrestadorServico)
                   .HasForeignKey(e => e.PrestadorServicoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(ps => ps.ServicoFinalidade)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasOne(ps => ps.Escola)
                   .WithMany(ps => ps.PrestadorServico)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
