using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public decimal CondicaoPagamento_Valor { get; set; }
        public Guid ProdutoId { get; set; }
        public int Qtde { get; set; }

        private SolicitacaoCompra() { }
        public SolicitacaoCompra(Produto produto, int qtde, int condicaoPagamento_Valor) 
        {
            Id = Guid.NewGuid();
            ProdutoId = produto.Id;
            Qtde = qtde;
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            CondicaoPagamento_Valor = condicaoPagamento_Valor;
        }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            foreach (var item in itens)
            {
                CondicaoPagamento_Valor += (item.Produto.Preco.Value * item.Qtde);
            }
        }
    }
}
