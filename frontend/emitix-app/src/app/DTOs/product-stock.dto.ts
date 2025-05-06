export class UpdateProductStockDto {
    constructor(
      public productCode: string,
      public quantity: number,
      public movementType: number
    ) {}
  }
  