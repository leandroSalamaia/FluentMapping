using ApiCliente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class PessoaJuridicaMap : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.ToTable("PessoaJuridica");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14);

            builder
                .HasIndex(x => x.Nome, "IX_Nome_Cliente")
                .IsUnique();

            builder
                .HasIndex(x => x.CNPJ, "IX_CNPJ_Cliente")
                .IsUnique();
        }
    }
}
