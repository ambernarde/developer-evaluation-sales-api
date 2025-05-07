using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }

        // ✅ Data da venda
        public DateTime Date { get; set; }

        // ✅ Identidade externa: cliente e filial
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string SaleNumber { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        // ✅ Valor total da venda
        public decimal TotalAmount { get; set; }

        // ✅ Cancelamento
        public bool IsCancelled { get; set; }

        // ✅ Lista de produtos vendidos
        public List<SaleItem> Items { get; set; } = new();
        public DateTime SaleDate { get; set; }
    }
}
