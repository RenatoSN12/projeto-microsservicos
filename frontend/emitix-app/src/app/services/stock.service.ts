import { inject, Injectable } from '@angular/core';
import { UpdateProductStockDto } from '../DTOs/product-stock.dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../interfaces/api-response.interface';
import { ProductStockDto } from '../interfaces/product-stock.interface';
import { NewProductStockDto } from '../DTOs/new-productstock.interface';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  private readonly apiUrl = 'http://localhost:5231/api/v1/stock';

  http = inject(HttpClient); 
  
  constructor() 
  { }

  updateStock(request: UpdateProductStockDto[]): Observable<ApiResponse<ProductStockDto[]>>{
    return this.http.put<ApiResponse<ProductStockDto[]>>(this.apiUrl, request);
  }

  pushItemStock(request: NewProductStockDto): Observable<ApiResponse<ProductStockDto>>{
    return this.http.post<ApiResponse<ProductStockDto>>(this.apiUrl, request)
  }
}
