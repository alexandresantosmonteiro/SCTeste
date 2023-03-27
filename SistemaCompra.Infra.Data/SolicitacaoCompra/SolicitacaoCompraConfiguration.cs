using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");

            builder
                .Property(x => x.ProdutoId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(x => x.CondicaoPagamento_Valor)
                .HasColumnType("decimal")
                .IsRequired();

            builder
                .Property(x => x.Qtde)
                .HasColumnType("Int")
                .IsRequired();
        }
    }
}
