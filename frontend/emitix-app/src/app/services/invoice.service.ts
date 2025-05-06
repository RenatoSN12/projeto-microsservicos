import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResponse } from '../../interfaces/api-response.interface';
import { InvoiceResponse } from '../../interfaces/invoice.interface';
import { CreateInvoiceRequest } from '../../interfaces/create-invoice-request.interface';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor() { }

  private readonly apiUrl = 'http://localhost:5016/api/v1/invoices';

  http = inject(HttpClient); 

  getInvoices(): Observable<InvoiceResponse[]> {
    return this.http.get<ApiResponse<InvoiceResponse[]>>(this.apiUrl).pipe(map(res => res.data))
  }

  getProductsByInvoice(number: number, series: string): Observable<InvoiceResponse> {
    return this.http.get<ApiResponse<InvoiceResponse>>(this.apiUrl + "?invoiceNumber=" + number + "&invoiceSeries=" + series).pipe(map(res => res.data))
  }

  createInvoice(values: CreateInvoiceRequest): Observable<InvoiceResponse> {
    return this.http.post<InvoiceResponse>('http://localhost:5016/api/v1/invoices', values);
  }

  printInvoice(values : {invoiceNumber:number, invoiceSeries:string}){
    return this.http.post<InvoiceResponse>('http://localhost:5016/api/v1/invoices/print', values);
  }

}
