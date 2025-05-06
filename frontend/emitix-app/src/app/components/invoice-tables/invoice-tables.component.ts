import { Component, inject } from '@angular/core';
import { InvoiceResponse } from '../../interfaces/invoice.interface';
import { InvoiceService } from '../../services/invoice.service';
import {MatButtonModule} from '@angular/material/button';
import { CommonModule, DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UpdateProductStockDto } from '../../DTOs/product-stock.dto';
import { StockService } from '../../services/stock.service';
import { InvoiceKeyDto } from '../../DTOs/invoice-key.dto';
import { AlterInvoiceStatusDto } from '../../DTOs/alter-invoice-status.dto';

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
  stockService = inject(StockService);
  snackbar = inject(MatSnackBar)

  constructor(){
    this.refreshInvoices();
  }

  refreshInvoices(){
    this.invoiceService.getInvoices().subscribe(data=> {
      this.invoices = data;
    })
  }

  printInvoice(invoice: InvoiceResponse) {
    const products = invoice.products.map(p => new UpdateProductStockDto(p.productCode, p.quantity, 2));
    const invoiceKey = new InvoiceKeyDto(invoice.number, invoice.series);
  
    this.stockService.updateStock(products).subscribe({
      next: () => {
        this.invoiceService.alterStatus(new AlterInvoiceStatusDto(invoiceKey.invoiceNumber, invoiceKey.invoiceSeries, 2)).subscribe({
          next: () => {
            this.invoiceService.printInvoice(invoiceKey).subscribe({
              next: () => {
                this.snackbar.open("Nota fiscal impressa com sucesso!", "Fechar", { duration: 3000 });
              },
              error: () => {
                const stockRollback = products.map(p => new UpdateProductStockDto(p.productCode, p.quantity, 1));
                this.stockService.updateStock(stockRollback).subscribe({
                  next: () => {
                    this.invoiceService.alterStatus(new AlterInvoiceStatusDto(invoiceKey.invoiceNumber, invoiceKey.invoiceSeries, 1)).subscribe(() => {
                      this.snackbar.open('Erro ao imprimir a nota fiscal. A movimentação de estoque foi compensada.', 'Fechar', { duration: 4000 });
                    });
                 },
                });
              }
            });
          },
          error: () => {
            const stockRollback = products.map(p => new UpdateProductStockDto(p.productCode, p.quantity, 1));
            this.stockService.updateStock(stockRollback).subscribe(() => {
              this.snackbar.open('Erro ao alterar status da nota. A movimentação de estoque foi compensada.', 'Fechar', { duration: 4000 });
            });
          }
        });
      },
      error: (err) => {
        const msg = err.error?.message || 'Erro ao verificar estoque';
        this.snackbar.open(msg, 'Fechar', { duration: 6000, panelClass: ['multi-line-snackbar'] });
      }
    });
  }
}
