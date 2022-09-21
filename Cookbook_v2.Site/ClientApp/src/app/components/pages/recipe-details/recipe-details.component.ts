import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { LocationService } from "../../../services/location.service";
import { RecipesService } from "../../../services/recipes.service";
import { AccountService } from "src/app/services/account.service";
import { RecipeDetailsDto } from "src/app/dtos/recipe-details-dto";
import { AuthenticatedUserDto } from "src/app/dtos/authenticated-user-dto";

@Component({
  selector: "app-recipe-details",
  templateUrl: "./recipe-details.component.html",
  styleUrls: ["./recipe-details.component.css"],
})
export class RecipeDetailsComponent implements OnInit {
  user?: AuthenticatedUserDto;

  recipe: RecipeDetailsDto = {
    id: 0,
    title: "",
    description: "",
    timesLiked: 0,
    timesFavorited: 0,
    cookingTimeInMinutes: 0,
    servingsCount: 0,
    authorUsername: "",
    isLikedByActiveUser: false,
    isFavoritedByActiveUser: false,
    tags: [],
    imageName: "",
    recipeSteps: [],
    ingredientsSections: [],
  };

  constructor(
    private _recipeService: RecipesService,
    private _locationService: LocationService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _accountSerivce: AccountService
  ) {
    _accountSerivce.user.subscribe((u) => (this.user = u));
  }

  ngOnInit() {
    this.loadRecipe();
  }

  loadRecipe(): void {
    const id = Number(this._route.snapshot.paramMap.get("id"));
    this._recipeService.getRecipeDetailsByID(id).subscribe(
      (response) => {
        this.recipe = response;
      },
      (badRequest) => {
        this._router.navigate(["/"]);
      }
    );
  }

  onGoBackClick(): void {
    this._locationService.goBack();
  }

  deleteRecipe(): void {
    if (confirm("Вы уверены что хотите удалить рецепт?")) {
      this._recipeService.deleteRecipe(this.recipe.id).subscribe(
        (response) => {
          this._router.navigate(["/recipes"]);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
