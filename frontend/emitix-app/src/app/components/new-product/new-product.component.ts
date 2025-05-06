import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogContent, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProductService } from '../../services/product.service';
import { NewProductDto } from '../../DTOs/new-product.interface';
import { StockService } from '../../services/stock.service';
import { NewProductStockDto } from '../../DTOs/new-productstock.interface';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-new-product',
  imports: [MatDialogModule,MatInputModule, MatButtonModule, MatFormFieldModule, MatDialogContent, ReactiveFormsModule],
  templateUrl: './new-product.component.html',
  styleUrl: './new-product.component.css'
})
export class NewProductComponent {
  form = new FormGroup({
    code: new FormControl<string>('', {nonNullable: true}),
    description: new FormControl<string>(''),
    unitPrice: new FormControl<number>(0, {nonNullable: true})
  })

  readonly productService = inject(ProductService)
  readonly stockService = inject(StockService)
  readonly dialogRef = inject(MatDialogRef<NewProductComponent>) 
  readonly snackbar = inject(MatSnackBar)

  onCancelButtonClicked(){
    this.dialogRef.close()
  }

  onCreateButtonClicked(){
    if(this.form.value.code == null || this.form.value.unitPrice == null)
      return;

    const newProduct = this.form.value!;

    this.productService.pushProduct(new NewProductDto (
      newProduct.code!,
      newProduct.description!,
      newProduct.unitPrice! )).subscribe({
        next: ()=> {
        this.stockService.pushItemStock(new NewProductStockDto(newProduct.code!, 0)).subscribe({
          next: ()=>{
            this.snackbar.open("Produto " + newProduct.code + " cadastrado com sucesso!", "Fechar", {duration: 4000})
            this.dialogRef.close()
          },
          error: (err)=> {
            const msg = err.error?.message || 'Erro ao realizar a gravação do produto.';
            this.snackbar.open(msg, 'Fechar', {duration: 6000})
          }
        })
      },
      error: (err) => {
        const msg = err.error?.message || 'Erro ao realizar a gravação do produto.';
        this.snackbar.open(msg, 'Fechar', {duration: 6000})
      } 
    })
  }
}
