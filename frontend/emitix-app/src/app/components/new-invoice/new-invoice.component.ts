import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {MatFormField, MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatChipsModule } from '@angular/material/chips';
import {
  MAT_DIALOG_DATA,
  MatDialogTitle,
  MatDialogContent
} from '@angular/material/dialog';
import { ProductService } from '../../services/product.service';
import { ProductResponse } from '../../interfaces/products.interface';
import { CreateInvoiceRequest } from '../../interfaces/create-invoice-request.interface';
import { InvoiceService } from '../../services/invoice.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-new-invoice',
  imports: [MatDialogTitle, MatDialogContent,MatChipsModule,MatFormFieldModule, MatAutocompleteModule ,MatIconModule, MatInputModule, MatButtonModule,FormsModule, ReactiveFormsModule, MatInputModule, MatFormField, FormsModule],
  templateUrl: './new-invoice.component.html',
  styleUrl: './new-invoice.component.css'
})
export class NewInvoiceComponent {
  selectedProducts: {product: ProductResponse, quantity: number}[] = []

  productCtrl = new FormControl<ProductResponse | null>(null);
  quantityCtrl = new FormControl<number>(0, {nonNullable: true});

  dialogRef = inject(MatDialogRef<NewInvoiceComponent>)
  
  form = new FormGroup({
    number: new FormControl<number>(0, {nonNullable: true}),
    series: new FormControl('', {nonNullable: true}),
  })
  
  products: ProductResponse[] = [];
  snackbar = inject(MatSnackBar)
  productService = inject(ProductService)
  invoiceService = inject(InvoiceService)
  data = inject(MAT_DIALOG_DATA)  
  
  constructor(){
    this.productService.getProcuts().subscribe(data => {
      this.products = data;
    })
  }

  addProduct()
  {
    const product = this.productCtrl.value;
    const quantity = this.quantityCtrl.value;

    if(product && quantity > 0){
      this.selectedProducts.push({product, quantity})
      this.productCtrl.setValue(null);
      this.quantityCtrl.setValue(0);
    }
  }

  removeProduct(productToRemove: ProductResponse) {
    this.selectedProducts = this.selectedProducts.filter(
      p => p.product.code !== productToRemove.code
    );
  }

  onCancelButtonClickedClick(){
    this.dialogRef.close();
  }

  createInvoice(){
    if(this.form.invalid) return;

    const raw = this.form.getRawValue();

    const value: CreateInvoiceRequest = {
      number: raw.number,
      series: raw.series,
      products: this.selectedProducts.map(p=> ({
        productCode: p.product.code,
        quantity: p.quantity,
        unitPrice: p.product.price
      }))
    };

    this.invoiceService.createInvoice(value).subscribe({
      next: ()=> {
        this.snackbar.open("Nota fiscal gravada com sucesso!", 'Fechar', {duration: 3000})
        this.dialogRef.close(true)},
      error: (err) => {
        const msg = err.error?.message || 'Erro ao gerar a nota fiscal.';
        this.snackbar.open(msg, 'Fechar', {duration: 6000})
      }
    })
  }

  displayProduct(product: ProductResponse): string {
    return product?.code || '';
  }
}
