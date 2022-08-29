import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Recipe, RecipePreview } from '../interfaces/recipe';
import { RecipeDTO } from '../dtos/recipe-dto';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {
  constructor(
    private _http: HttpClient
  ) { }

  getRecipePreviews(queryParams?: HttpParams): Observable<RecipePreview[]> {
    return this._http.get<RecipePreview[]>("api/recipe/GetPreviews", { params: queryParams });
  }
  
  getRecipeByID(id: number): Observable<Recipe> {
    return this._http.get<Recipe>(`api/recipe/${id}`);
  }

  createRecipe(recipe: RecipeDTO): Observable<any> {
    return this._http.post("api/recipe", recipe);
  }

  deleteRecipe(id: number): Observable<any> {
    return this._http.delete(`api/recipe/${id}`);
  }
}
