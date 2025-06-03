using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstoqueApp
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }

        public Produto()
        {

        }

        public Produto(int id, string nome, decimal preco, int quantidadeEmEstoque)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
        }

        public Produto(string nome, decimal preco, int quantidadeEmEstoque)
        {
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
        }

        public void ExibirInformacoes()
        {
            Console.WriteLine($"ID: {Id}, Nome: {Nome}, Preço: {Preco:C}, Quantidade em Estoque: {QuantidadeEmEstoque}");
        }
    }
}
