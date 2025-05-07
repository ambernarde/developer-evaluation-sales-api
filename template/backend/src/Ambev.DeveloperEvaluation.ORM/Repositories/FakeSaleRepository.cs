using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class FakeSaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();

        public Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            _sales.Add(sale);
            return Task.CompletedTask;
        }

        public Task<Sale?> GetByIdAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            return Task.FromResult<Sale?>(sale);
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return Task.FromResult(_sales.ToList());
        }

        public Task UpdateAsync(Sale sale)
        {
            var existing = _sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existing != null)
            {
                _sales.Remove(existing);
                _sales.Add(sale);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            if (sale != null)
                _sales.Remove(sale);

            return Task.CompletedTask;
        }
    }
}
