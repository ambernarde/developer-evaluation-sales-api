using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Sale> _sales;

        public SaleRepository(DbContext context)
        {
            _context = context;
            _sales = context.Set<Sale>();
        }

        public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _sales.Include(s => s.Items).ToListAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var sale = await _sales.FindAsync(id);
            if (sale != null)
            {
                _sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
