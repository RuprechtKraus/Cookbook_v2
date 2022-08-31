import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryDto } from '../dtos/category-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(
    private _http: HttpClient
  ) { }

  getCategories(): Observable<CategoryDto[]> {
    return this._http.get<CategoryDto[]>('http://localhost:5010/api/category');
  }
}
