using Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infra.Data.Mapping
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(prop => prop.Codl);

            builder.Property(p => p.Codl)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();          

            builder.Property(prop => prop.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("varchar(40)");

            builder.Property(prop => prop.Editora)
                .IsRequired()
                .HasColumnName("Editora")
                .HasColumnType("varchar(40)");

            builder.Property(prop => prop.Edicao)
                .IsRequired()
                .HasColumnName("Edicao")
                .HasColumnType("integer");

            builder.Property(prop => prop.AnoPublicacao)
                .IsRequired()
                .HasColumnName("AnoPublicacao")
                .HasColumnType("varchar(4)");

            builder.Ignore(x => x.Assuntos);
            builder.Ignore(x => x.Autores);
            builder.Ignore(x => x.FormaCompras);
            builder.Ignore(x => x.LivroFormaComprasDTO);
        }
    }
}
