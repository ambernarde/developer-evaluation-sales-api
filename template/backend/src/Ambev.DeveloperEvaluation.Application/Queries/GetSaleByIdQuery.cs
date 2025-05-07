using Ambev.DeveloperEvaluation.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleResponse?>
    {
        public Guid SaleId { get; set; }

        public GetSaleByIdQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
