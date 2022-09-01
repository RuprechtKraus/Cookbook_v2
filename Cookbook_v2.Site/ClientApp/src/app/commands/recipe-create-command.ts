import { RecipeIngredientSectionDto } from "../dtos/recipe-ingredient-section-dto";
import { RecipeStepDto } from "../dtos/recipe-step-dto";

export interface RecipeCreateCommand {
  userId: number;
  title: string;
  description: string;
  cookingTimeInMinutes: number;
  servingsCount: number;
  recipeSteps: RecipeStepDto[];
  ingredientsSections: RecipeIngredientSectionDto[];
  tags: string[];
  imageBase64?: string;
}