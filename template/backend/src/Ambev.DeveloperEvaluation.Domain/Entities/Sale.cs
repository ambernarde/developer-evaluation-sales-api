using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }

        // Date when the sale was made
        public DateTime SaleDate { get; set; }

        // External Identity (Customer)
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        // External Identity (Branch)
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        // Sale Number
        public string SaleNumber { get; set; } = string.Empty;

        // Sale Items
        public List<SaleItem> Items { get; set; } = new();

        // Total sale amount
        public decimal TotalAmount { get; set; }

        // Cancelled flag
        public bool IsCancelled { get; set; }
    }

}
