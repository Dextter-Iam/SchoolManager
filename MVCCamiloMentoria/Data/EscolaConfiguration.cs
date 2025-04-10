using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EscolaConfiguration : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.ToTable("Escola");
            builder.HasKey(e => e.Id);


            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.EnderecoId)
                   .IsRequired();

            builder.HasOne(e => e.Endereco)
                   .WithOne()
                   .HasForeignKey<Escola>(e => e.EnderecoId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(e => e.Turmas)
                   .WithOne(t => t.Escola)
                   .HasForeignKey(t => t.EscolaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.PrestadorServico)
                   .WithOne(e => e.Escola)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Fornecedores)
                   .WithOne(e => e.Escola)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Equipamentos)
                   .WithOne(e => e.Escola)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.Escola)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
