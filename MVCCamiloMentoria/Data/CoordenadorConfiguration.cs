using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class CoordenadorConfiguration : IEntityTypeConfiguration<Coordenador>
    {
        public void Configure(EntityTypeBuilder<Coordenador> builder)
        {
            builder.ToTable("Coordenador");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Matricula)
                   .HasMaxLength(6)
                   .IsRequired();

            builder.Property(p => p.Excluido)
                    .HasDefaultValue(false);

            builder.HasMany(c => c.Telefones)
                   .WithOne(e => e.Coordenador)
                   .HasForeignKey(t => t.CoordenadorId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(c => c.Endereco)
                   .WithOne()
                   .HasForeignKey<Coordenador>(c => c.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Escola)
                   .WithMany(e => e.Coordenadores)
                   .HasForeignKey(c => c.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
