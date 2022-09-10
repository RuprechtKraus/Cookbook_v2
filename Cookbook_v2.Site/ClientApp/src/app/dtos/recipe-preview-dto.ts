export interface RecipePreviewDto {
  id: number;
  title: string;
  description: string;
  timesLiked: number;
  timesFavorited: number;
  cookingTimeInMinutes: number;
  servingsCount: number;
  imageName: string;
  authorUsername: string;
  isLikedByActiveUser: boolean;
  isFavoritedByActiveUser: boolean;
  tags: string[];
}