export class AlterInvoiceStatusDto{
    constructor(number:number, series:string, status: number)
    {
        this.number = number;
        this.series = series;
        this.status = status;
    }
    
    number:number;
    series:string;
    status:number;
}