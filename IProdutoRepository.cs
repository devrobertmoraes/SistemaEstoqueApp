using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstoqueApp
{
    public interface IProdutoRepository
    {
        void AdicionarProduto(Produto produto);
        List<Produto> ObterTodosProdutos();
        Produto ObterProdutoPorId(int id);
        void ExcluirProduto(int id);
        void AtualizarProduto(Produto produto);
    }
}
