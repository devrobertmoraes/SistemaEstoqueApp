using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SistemaEstoqueApp;

public class Program
{
    public static void Main(string[] args)
    {
        IProdutoRepository produtoRepository = new ProdutoRepository();

        IProdutoService produtoService = new ProdutoService(produtoRepository);

        ConsoleUI consoleUI = new ConsoleUI(produtoService);

        consoleUI.Run();
    }
}