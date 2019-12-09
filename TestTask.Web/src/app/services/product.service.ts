import { Injectable } from '@angular/core';
import { Observable, from } from 'rxjs';

import { ApiService } from './api.service';
import { ProductDto, ProductListRequest, ProductListResponse } from '../models/product';

@Injectable()
export class ProductService extends ApiService {
    public getList(request: ProductListRequest): Observable<ProductListResponse> {
        return this.get<ProductListResponse>('product/list', request);
    }

    public getEditProductDto(id: string): Observable<ProductDto> {

        return this.get<ProductDto>(`product/edit/${id}`, null);
    }

    public save(item: ProductDto): Observable<void> {
        return this.post<void>('product/save', item);
    }

    public remove(id: string): Observable<void> {
        return this.delete<void>(`product/delete/${id}`, null);
    }
}
