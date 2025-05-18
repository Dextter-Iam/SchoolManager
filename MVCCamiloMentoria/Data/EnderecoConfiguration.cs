using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Complemento)
                   .HasMaxLength(30);

            builder.Property(e => e.NomeRua)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.CEP)
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(e => e.NumeroRua)
                   .IsRequired();

        }
    }
}