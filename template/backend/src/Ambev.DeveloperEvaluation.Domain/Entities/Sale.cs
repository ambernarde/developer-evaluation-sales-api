using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }

     
        public DateTime Date { get; set; }

     
        public Guid CustomerId { get; set; }
 

        public string SaleNumber { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

       
        public decimal TotalAmount { get; set; }

      
        public bool IsCancelled { get; set; }

        
        public List<SaleItem> Items { get; set; } = new();
        public DateTime SaleDate { get; set; }
    }
}
