using ApiCliente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Logradouro)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.numero)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(15);

            builder.Property(x => x.Bairo)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Complemento)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Cep)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(15);

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            builder.Property(x => x.Estado)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
        }
    }
}
