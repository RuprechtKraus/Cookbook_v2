import { Component, OnInit } from "@angular/core";
import { UntypedFormBuilder, Validators } from "@angular/forms";

import { CategoryDto } from "../../../dtos/category-dto";
import { CategoriesService } from "../../../services/categories.service";
import { RecipesService } from "../../../services/recipes.service";
import { LocationService } from "../../../services/location.service";
import { AccountService } from "src/app/services/account.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ModalWindowService } from "../../shared/modal-window/modal-window.service";
import { RecipePreviewDto } from "src/app/dtos/recipe-preview-dto";
import { RecipeSearchFilters } from "src/app/interfaces/recipe-search-filters";
import { RecipeSearchResult } from "src/app/interfaces/recipe-search-result";
import { CustomValidators } from "src/app/helpers/validators";

@Component({
  selector: "app-recipes",
  templateUrl: "./recipes.component.html",
  styleUrls: ["./recipes.component.css"],
})
export class RecipesComponent implements OnInit {
  categories: CategoryDto[] = [];
  recipePreviews: RecipePreviewDto[] = [];
  searchForm = this._formBuilder.group({
    searchText: [
      "",
      [
        Validators.pattern(/^[а-яА-Я1-9\s,]+$/),
        Validators.required,
        CustomValidators.NotWhiteSpaceString,
      ],
    ],
  });

  constructor(
    private _categoriesService: CategoriesService,
    private _recipesService: RecipesService,
    private _formBuilder: UntypedFormBuilder,
    private _locationService: LocationService,
    private _accountService: AccountService,
    private _modalService: ModalWindowService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadCategories();
    this.loadRecipes();
  }

  loadCategories(): void {
    this._categoriesService
      .getCategories()
      .subscribe((categories: CategoryDto[]) => (this.categories = categories));
  }

  loadRecipes(): void {
    const searchFilers: RecipeSearchFilters = {};
    this._route.queryParamMap.subscribe((params) => {
      searchFilers.tags = params.getAll("tags");
    });
    this._recipesService
      .search(searchFilers)
      .subscribe(
        (recipes: RecipeSearchResult) => (this.recipePreviews = recipes.result)
      );
  }

  onSubmit(): void {
    const searchText: string = this.searchForm.get("searchText").value;

    if (this.searchForm.invalid) {
      console.error("Invalid search format");
      return;
    }

    const params = { queryParams: { tags: searchText.split(", ") } };
    this._router.navigate(["/recipes"], params).then(() => this.loadRecipes());
  }

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
