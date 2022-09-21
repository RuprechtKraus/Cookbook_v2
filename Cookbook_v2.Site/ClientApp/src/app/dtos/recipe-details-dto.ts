import { RecipeIngredientSectionDto } from "./recipe-ingredient-section-dto";
import { RecipeStepDto } from "./recipe-step-dto";

export interface RecipeDetailsDto {
  id: number;
  title: string;
  description: string;
  timesLiked: number;
  timesFavorited: number;
  cookingTimeInMinutes: number;
  servingsCount: number;
  imageName: string;
  authorUsername: string;
  tags: string[];
  recipeSteps: RecipeStepDto[];
  isLikedByActiveUser: boolean;
  isFavoritedByActiveUser: boolean;
  ingredientsSections: RecipeIngredientSectionDto[];
}