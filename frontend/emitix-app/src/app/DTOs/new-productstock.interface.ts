export class NewProductStockDto{
    constructor(id:string, quantity: number){
        this.productId = id;
        this.quantity = quantity;
    }

    productId: string;
    quantity: number;
}