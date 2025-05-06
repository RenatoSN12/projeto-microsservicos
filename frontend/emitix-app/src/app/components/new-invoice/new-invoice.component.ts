import {Component, inject} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {MatFormField, MatInputModule} from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogTitle,
  MatDialogContent
} from '@angular/material/dialog';
import { ProductService } from '../../services/product.service';
import { ProductResponse } from '../../../interfaces/products.interface';

@Component({
  selector: 'app-new-invoice',
  imports: [MatDialogTitle, MatDialogContent, MatInputModule,MatAutocompleteModule, MatButtonModule,FormsModule, ReactiveFormsModule, MatInputModule, MatFormField, FormsModule],
  templateUrl: './new-invoice.component.html',
  styleUrl: './new-invoice.component.css'
})
export class NewInvoiceComponent {

  dialogRef = inject(MatDialogRef<NewInvoiceComponent>)
  stateCtrl = new FormControl('');
  products: ProductResponse[] = [];
  productService = inject(ProductService)
  data = inject(MAT_DIALOG_DATA)  
  
  constructor(){
    this.productService.getProcuts().subscribe(data => {
      this.products = data;
    })
  }

  onCancelClick(){
    this.dialogRef.close();
  }

}
