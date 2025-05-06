import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { InvoiceResponse } from '../../interfaces/invoice.interface';
import { ApiResponse } from '../../interfaces/api-response.interface';
import { CreateInvoiceRequest } from '../../interfaces/create-invoice-request.interface';
import { AlterInvoiceStatusDto } from '../../DTOs/alter-invoice-status.dto';
import { InvoiceKeyDto } from '../../DTOs/invoice-key.dto';

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

  createInvoice(values: CreateInvoiceRequest): Observable<ApiResponse<InvoiceResponse>> {
    return this.http.post<ApiResponse<InvoiceResponse>>('http://localhost:5016/api/v1/invoices', values);
  }

 alterStatus(invoice: AlterInvoiceStatusDto):Observable<ApiResponse<InvoiceResponse>>{
    return this.http.put<ApiResponse<InvoiceResponse>>(this.apiUrl + '/alter-status', invoice)
 }

  printInvoice(invoice: InvoiceKeyDto):Observable<ApiResponse<InvoiceResponse>>{
    return this.http.post<ApiResponse<InvoiceResponse>>(this.apiUrl + '/print', invoice)
  }

}
