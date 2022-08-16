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
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppMvcContext appMvcContext) : base(appMvcContext) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid fornecedorId)
        {
            return await appMvcContext.Fornecedores.AsNoTracking().Include(fornecedor => fornecedor.Endereco).FirstOrDefaultAsync(fornecedor => fornecedor.Id == fornecedorId);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid fornecedorId)
        {
            return await appMvcContext.Fornecedores
                .AsNoTracking()
                .Include(fornecedor => fornecedor.Endereco)
                .Include(fornecedor => fornecedor.Produtos)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == fornecedorId);
        }
    }
}
