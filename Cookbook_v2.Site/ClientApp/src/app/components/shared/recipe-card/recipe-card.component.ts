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
  onDetailPage: boolean;

  unauthenticatedMessage: string =
    "Войдите в свой аккаунт, чтобы оставлять лайки и добавлять рецепты в избранное";

  constructor(
    public _router: Router,
    private _recipeService: RecipesService,
    private _accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.onDetailPage = this.IsOnDetailsPage();
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
    this._recipeService
      .addLikeToRecipe(this.recipe.id)
      .subscribe((likeCount) => {
        this.recipe.timesLiked = likeCount;
        this.recipe.isLikedByActiveUser = true;
      });
  }

  private RemoveLike() {
    this._recipeService
      .removeLikeFromRecipe(this.recipe.id)
      .subscribe((likeCount) => {
        this.recipe.timesLiked = likeCount;
        this.recipe.isLikedByActiveUser = false;
      });
  }

  private AddFavorite(): void {
    this._recipeService
      .addRecipeToFavorites(this.recipe.id)
      .subscribe((favoriteCount) => {
        this.recipe.timesFavorited = favoriteCount;
        this.recipe.isFavoritedByActiveUser = true;
      });
  }

  private RemoveFavorite(): void {
    this._recipeService
      .removeRecipeFromFavorites(this.recipe.id)
      .subscribe((favoriteCount) => {
        this.recipe.timesFavorited = favoriteCount;
        this.recipe.isFavoritedByActiveUser = false;
      });
  }

  private IsOnDetailsPage(): boolean {
    var pattern = new RegExp("^/recipes/details/[0-9]+$");
    return (this.onDetailPage = pattern.test(this._router.url));
  }
}
