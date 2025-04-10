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

            builder.HasKey(e => e.EnderecoId);

            builder.HasMany(e => e.Alunos)
                   .WithOne(e => e.Endereco);

            builder.Property(e => e.NomeRua)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.CEP)
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(e => e.NumeroRua)
                   .IsRequired();

            builder.HasOne(e => e.Estado)
                   .WithMany(e=>e.Enderecos)
                   .IsRequired();
        }
    }
}