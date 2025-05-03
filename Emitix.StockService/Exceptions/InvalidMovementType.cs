namespace Emitix.StockService.Exceptions;

public class InvalidMovementType()
    : DomainException("Tipo de movimentação inválido. Utilize '1 - Entrada' ou '2 - Saída'.");