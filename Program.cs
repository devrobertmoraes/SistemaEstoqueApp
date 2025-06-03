using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SistemaEstoqueApp;

public class Program
{
    private static ProdutoRepository _produtoRepository = new ProdutoRepository();

    public static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao sistema de Gestão de estoque");

        bool continuar = true;

        while (continuar)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarProduto();
                    break;
                case "2":
                    ListarProdutos();
                    break;
                case "3":
                    AtualizarProduto();
                    break;
                case "4":
                    ExcluirProduto();
                    break;
                case "5":
                    continuar = false;
                    Console.WriteLine("Saindo do sistema, até mais!");
                    break;
                default:
                    Console.WriteLine("Opção inválida, escolha um número entre 1 e 5");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para sair.");
            Console.ReadKey();
            Console.Clear();
        }

        static void ExibirMenu()
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Atualizar Produto");
            Console.WriteLine("4. Excluir Produto");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
        }

        static void AdicionarProduto()
        {
            Console.WriteLine("\n--- Adicionar Novo Produto ---");

            Console.Write("Nome: ");

            string nome = Console.ReadLine();

            decimal preco;
            Console.Write("Preco: ");
            while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out preco))
            {
                Console.WriteLine("Preço inválido! Por favor, digite um número que seja válido! (ex: 12,36 ou 12.36): ");
            }

            int quantidade;
            Console.Write("Quantidade em Estoque: ");
            while (!int.TryParse(Console.ReadLine(), out quantidade))
            {
                Console.WriteLine("Quantidade inválida! Digite um número inteiro: ");
            }

            Produto novoProduto = new Produto(nome, preco, quantidade);

            try
            {
                _produtoRepository.AdicionarProduto(novoProduto);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao adicionar produto no banco de dados: {ex.Message}");
                Console.WriteLine($"Verifique sua string de conexão e se o banco de dados e a tabela existem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        static void ListarProdutos()
        {
            Console.WriteLine("\n--- Lista de Produtos ---");

            try
            {
                List<Produto> produtos = _produtoRepository.ObterTodosProdutos();

                if (produtos.Count == 0)
                {
                    Console.WriteLine("Nenhum produto cadastrado.");
                    return;
                }

                Console.WriteLine("{0,-5} {1,-30} {2,-10} {3,-15}", "ID", "Nome", "Preço", "Quantidade");
                Console.WriteLine("---------------------------------------------------------------");

                foreach (var produto in produtos)
                {
                    Console.WriteLine("{0,-5} {1,-30} {2,-10:C2} {3,-15}",
                        produto.Id,
                        produto.Nome,
                        produto.Preco,
                        produto.QuantidadeEmEstoque);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao listar produtos do banco de dados: {ex.Message}");
                Console.WriteLine($"Verifique sua string de conexão e se o banco de dados e a tabela existem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        static void AtualizarProduto()
        {
            Console.WriteLine("\n--- Atualizar Produto ---");
            Console.Write("Digite o ID do produto que deja atualizar: ");         
            if (!int.TryParse(Console.ReadLine(), out int idParaAtualizar))
            {
                Console.WriteLine("ID inválido! Por favor, digite um número inteiro.");
                return;
            }

            try
            {
                Produto produtoExistente = _produtoRepository.ObterProdutoPorId(idParaAtualizar);

                if (produtoExistente == null)
                {
                    Console.WriteLine($"Produto com ID {idParaAtualizar} não encontrado.");
                    return;
                }

                Console.WriteLine($"Produto encontrado => Nome: {produtoExistente.Nome}, Preço: {produtoExistente.Preco}, Quantidade: {produtoExistente.QuantidadeEmEstoque}");
                
                Console.Write($"Novo Nome ( Deixe em branco para manter '{produtoExistente.Nome}'): ");
                string novoNome = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    produtoExistente.Nome = novoNome;
                }

                Console.Write($"Novo Preço ( Deixe em branco para manter '{produtoExistente.Preco}'): ");
                string novoPrecoStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(novoPrecoStr))
                {
                    if (decimal.TryParse(novoPrecoStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal novoPreco))
                    {
                        produtoExistente.Preco = novoPreco;
                    }
                    else
                    {
                        Console.WriteLine("Preço inválido. Mantendo o preço original.");
                    }
                }

                Console.Write($"Nova quantidade em estoque ( Deixe em branco para manter '{produtoExistente.QuantidadeEmEstoque}'): ");
                string novaQuantidadeStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(novaQuantidadeStr))
                {
                    if (int.TryParse(novaQuantidadeStr, out int novaQuantidade))
                    {
                        produtoExistente.QuantidadeEmEstoque = novaQuantidade;
                    }
                    else
                    {
                        Console.WriteLine("Quantidade inválida. Mantendo o preço original.");
                    }
                }

                _produtoRepository.AtualizarProduto(produtoExistente);

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao latualizar produto do banco de dados: {ex.Message}");
                Console.WriteLine($"Verifique sua string de conexão e se o banco de dados e a tabela existem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        static void ExcluirProduto()
        {
            Console.WriteLine("\n--- Excluir Produto ---");
            Console.Write("Digite o ID do produto que deseja excluir: ");
            int idParaExcluir;
            if (!int.TryParse(Console.ReadLine(), out idParaExcluir))
            {
                Console.WriteLine("ID inválido. Por favor, digite um número inteiro.");
                return;
            }

            try
            {
                _produtoRepository.ExcluirProduto(idParaExcluir);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao excluir produto do banco de dados: {ex.Message}");
                Console.WriteLine("Verifique sua string de conexão.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }
    }
}