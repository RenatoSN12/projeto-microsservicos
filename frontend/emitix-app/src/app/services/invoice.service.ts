import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResponse } from '../interfaces/api-response.interface';
import { InvoiceResponse } from '../interfaces/invoice.interface';
import { CreateInvoiceRequest } from '../interfaces/create-invoice-request.interface';
import { InvoiceKeyDto } from '../DTOs/invoice-key.dto';
import { AlterInvoiceStatusDto } from '../DTOs/alter-invoice-status.dto';

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

 alterStatus(invoice: AlterInvoiceStatusDto):Observable<InvoiceResponse>{
    return this.http.put<InvoiceResponse>(this.apiUrl + '/alter-status', invoice)
 }

  printInvoice(invoice: InvoiceKeyDto):Observable<InvoiceResponse>{
    return this.http.post<InvoiceResponse>(this.apiUrl + '/print', invoice)
  }

}
