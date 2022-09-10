import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { RecipePreviewDto } from "src/app/dtos/recipe-preview-dto";
import { AccountService } from "src/app/services/account.service";
import { RecipesService } from "src/app/services/recipes.service";

@Component({
  selector: "app-recipe-card",
  templateUrl: "./recipe-card.component.html",
  styleUrls: ["./recipe-card.component.css"],
})
export class RecipeCardComponent implements OnInit {
  @Input() recipe: RecipePreviewDto;
  @Input() isClickable: boolean;
  onDetailPage: boolean;

  unauthenticatedMessage: string = "Войдите в свой аккаунт, чтобы оставлять лайки и добавлять рецепты в избранное";

  constructor(
    public _router: Router,
    private _recipeService: RecipesService,
    private _accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.onDetailPage = this.IsOnDetailsPage();
  }

  onClick(event: Event): void {
    if (this.isClickable) {
      const target = event.target as any;

      if (!(target.type === "button" || this.IsElementButtonChild(target))) {
        this._router.navigate([`/recipes/details/${this.recipe.id}`]);
      }
    }
  }

  onLikeClick(): void {
    if (!this._accountService.isAuthenticated()) {
      alert(this.unauthenticatedMessage);
      return;
    }
    
    if (!this.recipe.isLikedByActiveUser) {
      this.AddLike();
    } else {
      this.RemoveLike();
    }
  }

  onFavoriteClick(): void {
    if (!this._accountService.isAuthenticated()) {
      alert(this.unauthenticatedMessage);
      return;
    }
    
    if (!this.recipe.isFavoritedByActiveUser) {
      this.AddFavorite();
    } else {
      this.RemoveFavorite();
    }
  }

  private AddLike(): void {
    this.recipe.isLikedByActiveUser = true;
    this.recipe.timesLiked++;
    this._recipeService.addLikeToRecipe(this.recipe.id).subscribe();
  }

  private RemoveLike() {
    this.recipe.isLikedByActiveUser = false;
    this.recipe.timesLiked--;
    this._recipeService.removeLikeFromRecipe(this.recipe.id).subscribe();
  }

  private AddFavorite(): void {
    this.recipe.isFavoritedByActiveUser = true;
    this.recipe.timesFavorited++;
    this._recipeService.addRecipeToFavorites(this.recipe.id).subscribe();
  }

  private RemoveFavorite(): void {
    this.recipe.isFavoritedByActiveUser = false;
    this.recipe.timesFavorited--;
    this._recipeService.removeRecipeFromFavorites(this.recipe.id).subscribe();
  }

  private IsOnDetailsPage(): boolean {
    var pattern = new RegExp("^/recipes/details/[0-9]+$");
    return (this.onDetailPage = pattern.test(this._router.url));
  }

  private IsElementButtonChild(element: any): boolean {
    let parent: any | null = element.parentElement;

    while (parent !== null) {
      if (parent.type === "button") {
        return true;
      }
      parent = parent.parentElement;
    }

    return false;
  }
}
