using Grpc.Core;
using Inventory.Grpc.Protos;
using Inventory.Grpc.Repositories.Interfaces;

namespace Inventory.Grpc.Services;

public class InventoryService : StockProtoService.StockProtoServiceBase
{
    private readonly IInventoryRepository _repository;
    private readonly ILogger _logger;

    public InventoryService(IInventoryRepository repository, ILogger logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<StockModel> GetStock(GetStockRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"BEGIN Get Stock of ItemNo: {request.ItemNo}");
        var stockQuanitty = await _repository.GetStockQuantity(request.ItemNo);
        var result = new StockModel()
        {
            Quantity = stockQuanitty,
        };
        _logger.LogInformation($"END Get Stock of ItemNo: {request.ItemNo} - Quantity: {result.Quantity}");
        return result;
    }
}
