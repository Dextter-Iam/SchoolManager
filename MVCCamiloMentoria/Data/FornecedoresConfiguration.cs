using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class FornecedoresConfiguration : IEntityTypeConfiguration<Fornecedor>
    {

        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.NomeEmpresa)
                   .IsRequired()
                   .HasMaxLength(190);

            builder.Property(f => f.CPF)
                   .HasMaxLength(11);

            builder.Property(f => f.CNPJ)
                   .HasMaxLength(14);

            builder.Property(f => f.FinalidadeFornecedor)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.Fornecedor)
                   .HasForeignKey(e => e.FornecedorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Escola)
                   .WithMany(f => f.Fornecedores)
                   .HasForeignKey(f => f.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
