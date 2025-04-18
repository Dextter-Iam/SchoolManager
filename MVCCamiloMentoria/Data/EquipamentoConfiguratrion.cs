using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EquipamentoConfiguration : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder<Equipamento> builder)
        {
            builder.ToTable("Equipamento");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.StatusOperacional)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasOne(e => e.Modelo)
                   .WithMany(m => m.Equipamentos)
                   .HasForeignKey(e => e.ModeloId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Escola)
                   .WithMany(es => es.Equipamentos)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
