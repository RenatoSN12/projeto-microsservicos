import { Component, inject } from '@angular/core';
import { InvoiceResponse } from '../../../interfaces/invoice.interface';
import { InvoiceService } from '../../services/invoice.service';
import {MatButtonModule} from '@angular/material/button';
import { CommonModule, DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';

@Component({
  selector: 'app-invoice-tables',
  imports: [DatePipe,MatTableModule, MatButtonModule, CommonModule, MatIconModule],
  standalone: true,
  templateUrl: './invoice-tables.component.html',
  styleUrl: './invoice-tables.component.css'
})
export class InvoiceTablesComponent {
  displayedColumns: string[] = ['numberSerie', 'customer','totalValue','issuedDate','invoiceId', 'printInvoice'];
  invoices: InvoiceResponse[] = [];
  selectedInvoice: InvoiceResponse = null!;
  invoiceService = inject(InvoiceService);

  constructor(){
    this.invoiceService.getInvoices().subscribe(data => {
      this.invoices = data
    })
  }

  toggleExpand(number: number, series: string){
    this.invoiceService.getProductsByInvoice(number, series).subscribe(data => {
      this.selectedInvoice = data;
    })
  }

  printInvoice(values: {number: number, series: string}){
    this.invoiceService.printInvoice({invoiceNumber: values.number, invoiceSeries: values.series}).subscribe(()=>{})
  }
}
