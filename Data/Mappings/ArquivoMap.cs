using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOVAteste.Models;

namespace NOVAteste.Data.Mappings
{
    // Mapeamento do nosso model para gerar o banco de dados
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            // Tabela
            builder.ToTable("Arquivo");
            
            // Chave Primária
            builder.HasKey(x => x.Id);
            
            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200);
            
            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Dados)
                .IsRequired()
                .HasColumnName("Dados")
                .HasColumnType("VARBINARY")
                .HasMaxLength(Int32.MaxValue);
            
            // Índices
            builder.HasIndex(x => x.Descricao, "IV_Arquivo_Descricao")
                .IsUnique();
        }
    }
}