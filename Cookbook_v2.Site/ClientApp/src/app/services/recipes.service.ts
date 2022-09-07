import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

import { Observable } from "rxjs";
import { RecipeDetailsDto } from "../dtos/recipe-details-dto";
import { RecipePreviewDto } from "../dtos/recipe-preview-dto";
import { RecipeCreateCommand } from "../commands/recipe-create-command";
import { RecipeSearchFilters } from "../interfaces/recipe-search-filters";
import { RecipeSearchResult } from "../interfaces/recipe-search-result";

@Injectable({
  providedIn: "root",
})
export class RecipesService {
  private readonly apiUrl: string = "http://localhost:5010/api";

  constructor(private _http: HttpClient) {}

  search(filters: RecipeSearchFilters): Observable<RecipeSearchResult> {
    return this._http.post<RecipeSearchResult>(`${this.apiUrl}/recipe/search`, filters);
  }

  getRecipePreviews(queryParams?: HttpParams): Observable<RecipePreviewDto[]> {
    return this._http.get<RecipePreviewDto[]>(
      `${this.apiUrl}/recipe/previews`,
      {
        params: queryParams,
      }
    );
  }

  getRecipeByID(id: number): Observable<RecipeDetailsDto> {
    return this._http.get<RecipeDetailsDto>(
      `${this.apiUrl}/recipe/details/${id}`
    );
  }

  createRecipe(recipe: RecipeCreateCommand): Observable<any> {
    return this._http.post(`${this.apiUrl}/recipe/create`, recipe);
  }

  deleteRecipe(id: number): Observable<any> {
    return this._http.delete(`${this.apiUrl}/recipe/delete/${id}`);
  }
}
