using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class CreateSaleCommand : IRequest<Guid> 
    {
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public string SaleNumber { get; set; } = default!;

        public string BranchName { get; set; } = default!;
        public DateTime Date { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();

        public class SaleItemDto
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = default!;
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}
