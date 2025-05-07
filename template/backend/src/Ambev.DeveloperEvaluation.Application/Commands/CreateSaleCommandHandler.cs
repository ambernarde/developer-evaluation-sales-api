using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(request);

            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Produto {item.ProductId} excede o limite de 20 unidades.");

                decimal discount = 0;
                if (item.Quantity >= 10)
                    discount = 0.20m;
                else if (item.Quantity >= 4)
                    discount = 0.10m;

                item.Discount = discount;
                item.Total = item.Quantity * item.UnitPrice * (1 - discount);
            }

            sale.TotalAmount = sale.Items.Sum(i => i.Total);
            sale.IsCancelled = false;
            sale.Id = Guid.NewGuid(); // garante que o ID é novo

            await _saleRepository.AddAsync(sale, cancellationToken);

            Console.WriteLine($"[EVENTO] SaleCreated: SaleId = {sale.Id}");

            return sale.Id;
        }
    }
}
