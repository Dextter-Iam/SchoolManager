using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

public class AulaConfiguration : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aula");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(a => a.HorarioInicio)
               .IsRequired();

        builder.Property(a => a.HorarioFim)
               .IsRequired();

        builder.Property(a => a.ConfirmacaoPresenca)
               .HasDefaultValue(false);


        builder.HasOne(a => a.Professor)
               .WithMany(p => p.Aulas)
               .HasForeignKey(a => a.ProfessorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Disciplina)
            .WithMany(d => d.Aulas)
            .HasForeignKey(a => a.DisciplinaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Turma)
            .WithMany(t => t.Aulas)
            .HasForeignKey(a => a.TurmaId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(a => a.AlunosPresentes)
               .WithMany(al => al.Aulas)
               .UsingEntity<Dictionary<string, object>>(
                   "PresencaAula",
                   pa => pa.HasOne<Aluno>().WithMany().HasForeignKey("AlunoId"),  
                   pa => pa.HasOne<Aula>().WithMany().HasForeignKey("AulaId"), 
                   p =>
                   {
                       p.Property<bool>("Presente").HasDefaultValue(false); 
                       p.ToTable("PresencaAula"); 
                   }
               );
    }
}
