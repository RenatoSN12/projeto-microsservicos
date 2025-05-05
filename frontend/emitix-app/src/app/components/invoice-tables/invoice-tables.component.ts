import { Component, inject } from '@angular/core';
import { InvoiceResponse } from '../../../interfaces/invoice.interface';
import { InvoiceService } from '../../services/invoice.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-invoice-tables',
  imports: [DatePipe],
  standalone: true,
  templateUrl: './invoice-tables.component.html',
  styleUrl: './invoice-tables.component.css'
})
export class InvoiceTablesComponent {
  invoices: InvoiceResponse[] = [];
  selectedInvoice: InvoiceResponse = null!;
  invoiceService = inject(InvoiceService);

  toggleExpand(number: number, series: string){
    this.invoiceService.getProductsByInvoice(number, series).subscribe(data => {
      this.selectedInvoice = data;
    })
  }

  constructor(){
    this.invoiceService.getInvoices().subscribe(data => {
      this.invoices = data
    })
  }
}
