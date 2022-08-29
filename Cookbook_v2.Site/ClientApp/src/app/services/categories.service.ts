import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../interfaces/category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(
    private _http: HttpClient
  ) { }

  getCategories(): Observable<Category[]> {
    return this._http.get<Category[]>('api/category');
  }
}
