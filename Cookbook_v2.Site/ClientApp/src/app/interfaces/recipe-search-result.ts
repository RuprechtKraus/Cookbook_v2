import { RecipePreviewDto } from "../dtos/recipe-preview-dto";
import { SearchResult } from "./search-result";

export interface RecipeSearchResult extends SearchResult<RecipePreviewDto[]> {
  result: RecipePreviewDto[];
}