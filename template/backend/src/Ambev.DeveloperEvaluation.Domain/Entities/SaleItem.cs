using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }

        // ✅ Produto vendido
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SaleId { get; set; }

        // ✅ Preço e quantidade
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // ✅ Desconto aplicado
        public decimal Discount { get; set; }

        // ✅ Valor total do item (com desconto)
        public decimal Total { get; set; }

    }
}
