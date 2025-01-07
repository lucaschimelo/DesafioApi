using Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infra.Data.Mapping
{
    public class FormaCompraMap : IEntityTypeConfiguration<FormaCompra>
    {
        public void Configure(EntityTypeBuilder<FormaCompra> builder)
        {
            builder.ToTable("FormaCompra");

            builder.HasKey(prop => prop.CodFor);

            builder.Property(p => p.CodFor)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(prop => prop.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasColumnType("varchar(40)");            
        }
    }
}
