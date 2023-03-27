using MediatR;
using System;
using System.Collections.Generic;
using SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public Guid produtoId  { get; set; }
        public int Qtde { get; set; }
    }
}
