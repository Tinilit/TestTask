import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { CategoryDto } from '../models/category';

@Injectable()
export class CategoryService extends ApiService {
    public getList(): Observable<CategoryDto[]> {
        return this.get<CategoryDto[]>('category/list', null);
    }

    public autoComplete(term: string): Observable<CategoryDto[]>{
        return this.get<CategoryDto[]>('category/list', {term: term});
    }
}
