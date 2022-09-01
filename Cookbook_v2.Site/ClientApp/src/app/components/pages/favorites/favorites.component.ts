import { Component, OnInit } from "@angular/core";

import { RecipesService } from "../../../services/recipes.service";
import { LocationService } from "../../../services/location.service";
import { RecipePreviewDto } from "src/app/dtos/recipe-preview-dto";

@Component({
  selector: "app-favorites",
  templateUrl: "./favorites.component.html",
  styleUrls: ["./favorites.component.css"],
})
export class FavoritesComponent implements OnInit {
  favRecipes: RecipePreviewDto[] = [];

  constructor(
    private _recipesService: RecipesService,
    private _locationService: LocationService
  ) {}

  ngOnInit(): void {}

  onGoBackClick(): void {
    this._locationService.goBack();
  }
}
