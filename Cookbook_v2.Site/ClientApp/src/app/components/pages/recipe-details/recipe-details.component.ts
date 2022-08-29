import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { LocationService } from "../../../services/location.service";
import { RecipesService } from "../../../services/recipes.service";
import { Recipe } from "src/app/interfaces/recipe";
import { AccountService } from "src/app/services/account.service";
import { UserLoginDTO } from "src/app/dtos/user-login-dto";

@Component({
  selector: "app-recipe-details",
  templateUrl: "./recipe-details.component.html",
  styleUrls: ["./recipe-details.component.css"],
})
export class RecipeDetailsComponent implements OnInit {
  user?: UserLoginDTO;

  recipe: Recipe = {
    recipeID: 0,
    userID: 0,
    name: "",
    description: "",
    timesLiked: 0,
    timesFavorited: 0,
    cookingTimeInMinutes: 0,
    servingsAmount: 0,
    user: "",
    tags: [],
    imageURL: "",
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
    this._recipeService.getRecipeByID(id).subscribe(
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
      this._recipeService.deleteRecipe(this.recipe.recipeID).subscribe(
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
