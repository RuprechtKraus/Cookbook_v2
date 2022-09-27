import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { RecipeDetailsDto } from "../dtos/recipe-details-dto";
import { RecipePreviewDto } from "../dtos/recipe-preview-dto";
import { RecipeCreateCommand } from "../commands/recipe-create-command";
import { RecipeSearchFilters } from "../interfaces/recipe-search-filters";
import { RecipeSearchResult } from "../interfaces/recipe-search-result";
import { RecipeEditorDto } from "../dtos/recipe-editor-dto";

@Injectable({
  providedIn: "root",
})
export class RecipesService {
  private readonly apiUrl: string = "http://localhost:5010/api";

  constructor(private _http: HttpClient) {}

  getByUserId(id: number): Observable<RecipePreviewDto[]> {
    return this._http.get<RecipePreviewDto[]>(
      `${this.apiUrl}/recipe/by_user_id/${id}`
    );
  }

  search(filters: RecipeSearchFilters): Observable<RecipeSearchResult> {
    return this._http.post<RecipeSearchResult>(
      `${this.apiUrl}/recipe/search`,
      filters
    );
  }

  getRecipeDetailsById(id: number): Observable<RecipeDetailsDto> {
    return this._http.get<RecipeDetailsDto>(
      `${this.apiUrl}/recipe/details/${id}`
    );
  }

  getRecipeEditor(id: number): Observable<RecipeEditorDto> {
    return this._http.get<RecipeEditorDto>(
      `${this.apiUrl}/recipe/editor/${id}`
    );
  }

  getFavoriteRecipes(): Observable<RecipePreviewDto[]> {
    return this._http.get<RecipePreviewDto[]>(
      `${this.apiUrl}/recipe/favorites`
    );
  }

  createRecipe(recipe: RecipeCreateCommand): Observable<any> {
    return this._http.post(`${this.apiUrl}/recipe/create`, recipe);
  }

  updateRecipe(id: number, recipe: RecipeCreateCommand): Observable<any> {
    return this._http.post(`${this.apiUrl}/recipe/update/${id}`, recipe);
  }

  deleteRecipe(id: number): Observable<any> {
    return this._http.delete(`${this.apiUrl}/recipe/delete/${id}`);
  }

  addLikeToRecipe(id: number): Observable<number> {
    return this._http.post<number>(`${this.apiUrl}/recipe/${id}/likes/add`, {});
  }

  removeLikeFromRecipe(id: number): Observable<number> {
    return this._http.delete<number>(
      `${this.apiUrl}/recipe/${id}/likes/remove`,
      {}
    );
  }

  addRecipeToFavorites(id: number): Observable<number> {
    return this._http.post<number>(
      `${this.apiUrl}/recipe/${id}/favorites/add`,
      {}
    );
  }

  removeRecipeFromFavorites(id: number): Observable<number> {
    return this._http.delete<number>(
      `${this.apiUrl}/recipe/${id}/favorites/remove`,
      {}
    );
  }
}
