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
        ProdutoRepository produtoRepository = new ProdutoRepository();

        ConsoleUI consoleUI = new ConsoleUI(produtoRepository);

        consoleUI.Run();
    }
}