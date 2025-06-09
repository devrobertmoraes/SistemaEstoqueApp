using System;
using System.Collections.Generic;

namespace SistemaEstoqueApp
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public void AdicionarProduto(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto não pode ser vazio.");
            }
            if (produto.Preco < 0)
            {
                throw new ArgumentException("O preço do produto não pode ser negativo.");
            }
            if (produto.QuantidadeEmEstoque < 0)
            {
                throw new ArgumentException("A quantidade em estoque não pode ser negativa.");
            }

            _produtoRepository.AdicionarProduto(produto);
        }

        public List<Produto> ObterTodosProdutos()
        {
            return _produtoRepository.ObterTodosProdutos();
        }

        public Produto ObterProdutoPorId(int id)
        {
            return _produtoRepository.ObterProdutoPorId(id);
        }

        public void AtualizarProduto(Produto produto)
        {
            var produtoExistente = _produtoRepository.ObterProdutoPorId(produto.Id);
            if (produtoExistente == null)
            {
                throw new ArgumentException($"Produto com ID {produto.Id} não encontrado para atualização.");
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto não pode ser vazio.");
            }

            _produtoRepository.AtualizarProduto(produto);
        }

        public void ExcluirProduto(int id)
        {
            _produtoRepository.ExcluirProduto(id);
        }
    }
}
