using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppMvcContext appMvcContext) : base(appMvcContext) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid produtoId)
        {
            return await appMvcContext.Produtos.AsNoTracking().Include(fornecedor => fornecedor.Fornecedor).FirstOrDefaultAsync(produto => produto.Id == produtoId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await appMvcContext.Produtos.AsNoTracking().Include(fornecedor => fornecedor.Fornecedor).OrderBy(produto => produto.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(produto => produto.FornecedorId == fornecedorId);
        }
    }
}
