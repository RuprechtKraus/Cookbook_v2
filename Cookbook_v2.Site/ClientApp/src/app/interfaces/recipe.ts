import { IngredientsSection } from './ingredients-section';
import { RecipeStep } from './recipe-step';

export interface RecipeBase {
  userID: number;
  user: string;
  name: string;
  description: string;
  cookingTimeInMinutes: number;
  servingsAmount: number;
  tags: string[];
}

export interface RecipeWithImageFavsAndLikes {
  timesLiked: number;
  timesFavorited: number;
  imageURL: string;
}

export interface RecipeWithDetailedInfo {
  recipeSteps: RecipeStep[];
  ingredientsSections: IngredientsSection[];
}

export interface RecipePreview extends RecipeBase, RecipeWithImageFavsAndLikes {
  recipeID: number;
}

export interface Recipe extends RecipePreview, RecipeWithDetailedInfo {}
