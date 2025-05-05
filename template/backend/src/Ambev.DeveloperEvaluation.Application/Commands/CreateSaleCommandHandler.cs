using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleItems = new List<SaleItem>();

            foreach (var item in request.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Produto {item.ProductId} excede o limite de 20 unidades.");

                decimal discount = 0;
                if (item.Quantity >= 10)
                    discount = 0.20m;
                else if (item.Quantity >= 4)
                    discount = 0.10m;

                var saleItem = new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = discount,
                    Total = item.Quantity * item.UnitPrice * (1 - discount)
                };

                saleItems.Add(saleItem);
            }

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleDate = request.SaleDate,
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                Items = saleItems,
                TotalAmount = saleItems.Sum(i => i.Total),
                IsCancelled = false
            };

            await _saleRepository.AddAsync(sale, cancellationToken);

            // Simulação de publicação de evento
            Console.WriteLine($"[EVENTO] SaleCreated: SaleId = {sale.Id}");

            return sale.Id;
        }
    }
}
