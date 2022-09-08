import { RecipeBase, RecipeWithDetailedInfo } from '../interfaces/recipe';

export interface RecipeDTO extends RecipeBase, RecipeWithDetailedInfo {
  imageBase64: string;
}
