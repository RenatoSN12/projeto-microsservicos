export class InvoiceKeyDto{
    constructor(number:number, series:string)
    {
        this.invoiceNumber = number;
        this.invoiceSeries = series
    }
    
    invoiceNumber:number;
    invoiceSeries:string;
}