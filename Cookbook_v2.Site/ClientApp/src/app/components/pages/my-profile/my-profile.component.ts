import { Component, OnInit } from "@angular/core";
import { RecipePreviewDto } from "src/app/dtos/recipe-preview-dto";
import { UserDetailsDto } from "src/app/interfaces/user";
import { AccountService } from "src/app/services/account.service";
import { RecipeSearchFilters } from "src/app/interfaces/recipe-search-filters";

import { LocationService } from "../../../services/location.service";
import { RecipesService } from "../../../services/recipes.service";

@Component({
  selector: "app-my-profile",
  templateUrl: "./my-profile.component.html",
  styleUrls: ["./my-profile.component.css"],
})
export class MyProfileComponent implements OnInit {
  hidePass: boolean = true;
  myRecipes: RecipePreviewDto[] = [];
  user: UserDetailsDto = {
    name: "",
    username: "",
    about: "",
    recipesCount: 0,
    likesCount: 0,
    favoritesCount: 0,
  };

  constructor(
    private _recipesService: RecipesService,
    private _locationService: LocationService,
    private _accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.loadUserData();
    this.loadRecipes();
  }

  switchPassField(): void {
    this.hidePass = !(this.hidePass === true);
  }

  loadUserData(): void {
    this._accountService
      .getByID(this._accountService.userValue.id)
      .subscribe((response) => {
        this.user = response;
      });
  }

  loadRecipes(): void {
    this._recipesService
      .getByUserId(this._accountService.userValue.id)
      .subscribe((recipes) => (this.myRecipes = recipes));
  }

  onGoBackClick(): void {
    this._locationService.goBack();
  }
}
