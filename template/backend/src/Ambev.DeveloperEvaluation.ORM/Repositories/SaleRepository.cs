using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            sale.Id = Guid.NewGuid();
            foreach (var item in sale.Items)
            {
                item.Id = Guid.NewGuid();
            }

            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Sale sale)
        {
            var existingSale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == sale.Id);

            if (existingSale != null)
            {
                // Atualiza propriedades principais
                existingSale.SaleNumber = sale.SaleNumber;
              
                existingSale.BranchName = sale.BranchName;
                existingSale.Date = sale.Date;

                // Remove itens antigos
                _context.SaleItems.RemoveRange(existingSale.Items);

                // Adiciona novos itens
                foreach (var item in sale.Items)
                {
                    item.Id = Guid.NewGuid();
                    item.SaleId = existingSale.Id;
                }
                existingSale.Items = sale.Items;

                await _context.SaveChangesAsync();
            }
        }
    }
}
