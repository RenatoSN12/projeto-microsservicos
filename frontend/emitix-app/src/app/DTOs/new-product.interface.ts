export class NewProductDto{
    constructor(productCode: string, productDescription: string, productPrice: number){
        this.code = productCode;
        this.description = productDescription;
        this.price = productPrice
    }
    code:string;
    description:string;
    price:number
}