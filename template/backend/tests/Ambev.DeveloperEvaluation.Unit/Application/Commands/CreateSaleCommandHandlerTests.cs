using Ambev.DeveloperEvaluation.Application.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;


namespace Ambev.DeveloperEvaluation.Unit.Application.Commands;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleCommandHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale command When handling Then saves sale and returns Sale Id")]
    public async Task Handle_ValidCommand_ShouldAddSale()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = "123456",       
            BranchName = "Main Branch",
            Date = DateTime.UtcNow,
            Items = new List<CreateSaleCommand.SaleItemDto>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10.0m },
            new() { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 20.0m }
        }
        };

        var expectedId = Guid.NewGuid();
        var mappedEntity = new Sale
        {
            Id = expectedId,
            SaleNumber = command.SaleNumber,       
            BranchName = command.BranchName,
            SaleDate = command.Date,
            Items = command.Items.Select(i => new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = 0,
                SaleId = expectedId
            }).ToList()
        };

        _mapper.Map<Sale>(command).Returns(mappedEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _repository.Received(1).AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());

        result.Should().NotBeEmpty();
        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s =>
           
            s.Items.Count == 2 &&
            s.TotalAmount > 0
        ), Arg.Any<CancellationToken>());

    }

    [Fact(DisplayName = "Given item quantity > 20 When handling Then throws exception")]
    public async Task Handle_TooManyItems_ShouldThrow()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = "999",
         
            BranchName = "Filial X",
            Date = DateTime.Now,
            Items = new List<CreateSaleCommand.SaleItemDto>

            {
                new() { ProductName = "Item A", Quantity = 21, UnitPrice = 50.0m }
            }
        };

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*excede o limite de 20 unidades*");
    }
}
