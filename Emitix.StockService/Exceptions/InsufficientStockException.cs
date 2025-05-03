namespace Emitix.StockService.Exceptions;

public class InsufficientStockException(string productCode)
    : DomainException($"Não foi possível realizar a movimentação: o produto com código {productCode} ficaria com estoque negativo.");