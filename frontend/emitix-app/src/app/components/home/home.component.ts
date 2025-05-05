import { Component } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { InvoiceTablesComponent } from "../invoice-tables/invoice-tables.component";


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, InvoiceTablesComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  
}
