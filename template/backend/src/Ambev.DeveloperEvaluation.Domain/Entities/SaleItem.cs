using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public SaleItem() { }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity < 4) return 0;
            if (quantity < 10) return quantity * unitPrice * 0.10m;
            return quantity * unitPrice * 0.20m;
        }
    }

}
