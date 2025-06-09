using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstoqueApp
{
    public interface IProdutoService
    {
        void AdicionarProduto(Produto produto);
        List<Produto> ObterTodosProdutos();
        Produto ObterProdutoPorId(int id);
        void AtualizarProduto(Produto produto);
        void ExcluirProduto(int id);
    }
}
