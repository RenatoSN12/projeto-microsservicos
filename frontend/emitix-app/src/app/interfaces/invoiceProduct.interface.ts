export interface InvoiceProduct {
    productCode: string;
    quantity: number;
    unitPrice: number;
  }

  export interface InvoiceProductResponse {
    productCode: string;
    quantity: number;
    unitPrice: number;
    subtotal: number;
  }