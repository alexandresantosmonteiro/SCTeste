using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(ProdutoAgg.IProdutoRepository produtoRepository, SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this._produtoRepository = produtoRepository;
            this._solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            int condicaoPagamento_Valor = 0;
            var produto = _produtoRepository.Obter(request.produtoId);

            if ((produto.Preco.Value * request.Qtde) > 50000)
                condicaoPagamento_Valor = 30;

            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(produto, request.Qtde, condicaoPagamento_Valor);

            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(produto.Events);

            return Task.FromResult(true);
        }
    }
}
