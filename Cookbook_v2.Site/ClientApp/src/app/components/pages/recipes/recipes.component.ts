import { Component, OnInit } from "@angular/core";
import { UntypedFormBuilder } from "@angular/forms";

import { CategoryDto } from "../../../dtos/category-dto";
import { CategoriesService } from "../../../services/categories.service";
import { RecipesService } from "../../../services/recipes.service";
import { LocationService } from "../../../services/location.service";
import { RecipePreview } from "src/app/interfaces/recipe";
import { AccountService } from "src/app/services/account.service";
import { Router } from "@angular/router";
import { ModalWindowService } from "../../shared/modal-window/modal-window.service";

@Component({
  selector: "app-recipes",
  templateUrl: "./recipes.component.html",
  styleUrls: ["./recipes.component.css"],
})
export class RecipesComponent implements OnInit {
  categories: CategoryDto[] = [];
  recipePreviews: RecipePreview[] = [];
  searchForm = this._formBuilder.group({
    searchText: "",
  });

  constructor(
    private _categoriesService: CategoriesService,
    private _recipesService: RecipesService,
    private _formBuilder: UntypedFormBuilder,
    private _locationService: LocationService,
    private _accountService: AccountService,
    private _modalService: ModalWindowService,
    private _router: Router
  ) {}

  ngOnInit() {
    this._categoriesService
      .getCategories()
      .subscribe((categories: CategoryDto[]) => (this.categories = categories));
    this._recipesService
      .getRecipePreviews()
      .subscribe((recipes: RecipePreview[]) => (this.recipePreviews = recipes));
  }

  onSubmit(): void {}

  onGoBackClick(): void {
    this._locationService.goBack();
  }

  goToRecipeCreationPage(): void {
    if (this._accountService.userValue) {
      this._router.navigate(["/create-recipe"]);
    } else {
      this.openModal("unauthorized-modal");
    }
  }

  openModal(id: string) {
    this._modalService.open(id);
  }
}
