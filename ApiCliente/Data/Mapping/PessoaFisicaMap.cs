using ApiCliente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.ToTable("PessoaFisica");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.DataNascimento)
                .IsRequired()
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(10);

            builder
                .HasIndex(x => x.Nome, "IX_Nome_Cliente")
                .IsUnique();

            builder
                .HasIndex(x => x.CPF, "IX_CPF_Cliente")
                .IsUnique();
        }
    }
}
