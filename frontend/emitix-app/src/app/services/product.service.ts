import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResponse } from '../../interfaces/api-response.interface';
import { ProductResponse } from '../../interfaces/products.interface';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor() { }

  private readonly apiUrl = 'http://localhost:5125/api/v1/products';

  http = inject(HttpClient); 

  getProcuts(): Observable<ProductResponse[]> {
    return this.http.get<ApiResponse<ProductResponse[]>>(this.apiUrl).pipe(map(res => res.data))
  }
}
