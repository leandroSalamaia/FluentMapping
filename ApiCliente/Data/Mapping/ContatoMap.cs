using ApiCliente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contato");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Telefone)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14);

            builder.Property(x => x.Email)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14);
        }
    }
}
