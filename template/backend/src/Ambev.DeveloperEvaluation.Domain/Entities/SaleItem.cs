﻿using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }

     
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SaleId { get; set; }

      
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    
        public decimal Discount { get; set; }

        public decimal Total { get; set; }

    }
}
