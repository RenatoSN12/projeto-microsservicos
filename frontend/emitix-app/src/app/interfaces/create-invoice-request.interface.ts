import { InvoiceProduct } from "./invoiceProduct.interface";

export interface CreateInvoiceRequest {
    number: number;
    series: string;
    products: InvoiceProduct[];
  }