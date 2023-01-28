import { Injectable, Inject } from '@angular/core';
import { Product } from 'src/app/models/product/product';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { PagedCollection } from 'src/app/models/paged-collection/paged-collection';
import { UpsertProduct } from 'src/app/models/product/upsertProduct';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly apiPath: string = 'api/product';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}${this.apiPath}`);
  }

  upsertProduct(upsertProduct: UpsertProduct): Observable<Product> {
    return this.http.post<Product>(`${this.baseUrl}${this.apiPath}`, upsertProduct);
  }

  deleteProduct(productId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${this.apiPath}/${productId}`);
  }

  getAllProductsPaginated(pagesize: number, pagenumber: number) {
    return this.http
      .get<PagedCollection<Product>>(
        `${this.baseUrl}${this.apiPath}/paginatedproductsasync/?pagesize=${pagesize}&pagenumber=${pagenumber}`
      )
      .pipe(catchError(this.handleError<PagedCollection<Product>>()));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
