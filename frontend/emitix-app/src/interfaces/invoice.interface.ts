import { InvoiceProductResponse } from "./invoiceProduct.interface";

export interface InvoiceResponse {
    id: string;
    number: number;
    series: string;
    issuedDate: Date;
    totalAmount: number;
    invoiceStatus: number;
    products: InvoiceProductResponse[];
}