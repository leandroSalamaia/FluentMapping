using ApiCliente.Models.Tarefa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.StatusTarefa)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.TipoTarefa)
                .IsRequired()
                .HasColumnType("int");
            
        }
    }
}