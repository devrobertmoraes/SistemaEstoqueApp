using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Azure.Core.Pipeline;

namespace SistemaEstoqueApp
{
    public class ProdutoRepository
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=EstoqueDB;Integrated Security=True;TrustServerCertificate=True";

        public ProdutoRepository()
        {

        }

        public void AdicionarProduto(Produto produto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Produtos (Nome, Preco, QuantidadeEmEstoque) VALUES (@Nome, @Preco, @QuantidadeEmEstoque); SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@QuantidadeEmEstoque", produto.QuantidadeEmEstoque);

                    connection.Open();

                    int novoId = Convert.ToInt32(command.ExecuteScalar());

                    produto.Id = novoId;

                    Console.WriteLine($"Produto '{produto.Nome}' adicionado com sucesso! ID:{produto.Id}");
                }
            }
        }

        public List<Produto> ObterTodosProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Nome, Preco, QuantidadeEmEstoque FROM Produtos";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produto produto = new Produto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                QuantidadeEmEstoque = reader.GetInt32(reader.GetOrdinal("QuantidadeEmEstoque")),
                            };

                            produtos.Add(produto);
                        }
                    }
                }
            }

            return produtos;
        }

        public Produto ObterProdutoPorId(int id)
        {
            Produto produto = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Nome, Preco, QuantidadeEmEstoque FROM Produtos WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produto = new Produto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                QuantidadeEmEstoque = reader.GetInt32(reader.GetOrdinal("QuantidadeEmEstoque")),
                            };
                        }
                    }
                }
            }

            return produto;
        }

        public void AtualizarProduto(Produto produto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, QuantidadeEmEstoque = @QuantidadeEmEstoque WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@QuantidadeEmEstoque", produto.QuantidadeEmEstoque);
                    command.Parameters.AddWithValue("@Id", produto.Id);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Produto com ID {produto.Id} atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Erro: Produto com ID {produto.Id} não encontrado para atualização!");
                    }
                }
            }
        }

        public void ExcluirProduto(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Produto com ID {id} excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Erro: Produto com ID {id} não encontrado para exclusão!");
                    }
                }
            }
        }
    }
}
