using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class UpdateSaleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItemDto> Items { get; set; }

        public class SaleItemDto
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }
    }
}
