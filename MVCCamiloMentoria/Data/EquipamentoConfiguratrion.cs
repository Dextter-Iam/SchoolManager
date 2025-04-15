using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EquipamentoConfiguration : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder <Equipamento> builder)
        {
            builder.ToTable("Equipamento");

            builder.HasKey(eq => eq.Id);

            builder.Property(eq => eq.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(eq => eq.StatusOperacional)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.HasOne(e => e.Modelo)
                   .WithMany(m => m.Equipamento)
                   .HasForeignKey(e => e.ModeloId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Escola)
                   .WithMany(e => e.Equipamentos)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
