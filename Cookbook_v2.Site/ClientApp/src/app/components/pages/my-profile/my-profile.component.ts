import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { RecipePreview } from "src/app/interfaces/recipe";
import { User } from "src/app/interfaces/user";
import { AccountService } from "src/app/services/account.service";

import { LocationService } from "../../../services/location.service";
import { RecipesService } from "../../../services/recipes.service";

@Component({
  selector: "app-my-profile",
  templateUrl: "./my-profile.component.html",
  styleUrls: ["./my-profile.component.css"],
})
export class MyProfileComponent implements OnInit {
  hidePass: boolean = true;
  myRecipes: RecipePreview[] = [];
  user: User = {
    name: "",
    login: "",
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
      .getByID(this._accountService.userValue.userID)
      .subscribe((response) => {
        this.user = response;
      });
  }

  loadRecipes(): void {
    this._recipesService
      .getRecipePreviews(
        new HttpParams().set("userID", this._accountService.userValue.userID)
      )
      .subscribe((recipes) => (this.myRecipes = recipes));
  }

  onGoBackClick(): void {
    this._locationService.goBack();
  }
}
