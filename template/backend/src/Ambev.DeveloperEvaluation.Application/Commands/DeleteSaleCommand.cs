using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class DeleteSaleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
