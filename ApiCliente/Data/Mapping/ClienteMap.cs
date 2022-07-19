using ApiCliente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCliente.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.TipoCliente)
                .HasColumnType("int")
                .HasMaxLength(1);

            builder.HasOne(x => x.PessoaFisica)
                .WithOne(x => x.Cliente)
                .HasConstraintName("FK_PessoaFisica_Cliente")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PessoaJuridica)
                .WithOne(x => x.Cliente)
                .HasConstraintName("FK_PessoaJuridica_Cliente")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Contatos)
                .WithOne(x => x.Cliente)
                .HasConstraintName("FK_Contato_Cliente")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Endereco)
                .WithOne(x => x.Cliente)
                .HasConstraintName("FK_Endereco_Cliente")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
