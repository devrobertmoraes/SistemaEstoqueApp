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


    }
}