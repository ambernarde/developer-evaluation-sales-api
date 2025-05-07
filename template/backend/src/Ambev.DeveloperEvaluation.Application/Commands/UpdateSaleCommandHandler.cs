using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetByIdAsync(request.Id);

            if (existingSale == null)
                throw new KeyNotFoundException("Sale not found");

            existingSale.Date = request.Date;
            existingSale.Items = request.Items.Select(i => new Domain.Entities.SaleItem
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity
            }).ToList();

            await _saleRepository.UpdateAsync(existingSale);

            return Unit.Value;
        }
    }
}
