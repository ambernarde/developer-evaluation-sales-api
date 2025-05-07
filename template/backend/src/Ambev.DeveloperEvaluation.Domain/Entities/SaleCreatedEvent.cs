using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public SaleCreatedEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }

}
