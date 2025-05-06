import { Component, inject, ViewChild } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { InvoiceTablesComponent } from "../invoice-tables/invoice-tables.component";
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { NewInvoiceComponent } from '../new-invoice/new-invoice.component';
import { NewProductComponent } from '../new-product/new-product.component';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, InvoiceTablesComponent, MatIconModule, MatButtonModule, MatDialogModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  readonly dialog = inject(MatDialog);
  @ViewChild(InvoiceTablesComponent)
  invoiceTables!: InvoiceTablesComponent;

  openNewProduct(){
    const dialog = this.dialog.open(NewProductComponent);
  }

  openNewInvoice(){
    const dialog = this.dialog.open(NewInvoiceComponent);

    dialog.afterClosed().subscribe((refresh: boolean)=>{
      if (refresh){
        this.invoiceTables.refreshInvoices();
      }
    })
  }
}
