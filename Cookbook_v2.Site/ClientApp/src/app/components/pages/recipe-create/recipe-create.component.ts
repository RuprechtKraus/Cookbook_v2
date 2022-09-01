import { Component, OnInit } from "@angular/core";
import { ENTER, SPACE } from "@angular/cdk/keycodes";

import { TimeOption } from "../../../interfaces/time-option";
import { MatChipInputEvent } from "@angular/material/chips";
import { ServingOption } from "../../../interfaces/serving-options";
import { LocationService } from "../../../services/location.service";
import { AccountService } from "src/app/services/account.service";
import { ImageService } from "src/app/services/image.service";
import { RecipesService } from "src/app/services/recipes.service";
import { Router } from "@angular/router";
import { RecipeCreateCommand } from "src/app/commands/recipe-create-command";
import { RecipeIngredientSectionDto } from "src/app/dtos/recipe-ingredient-section-dto";
import { RecipeStepDto } from "src/app/dtos/recipe-step-dto";

@Component({
  selector: "app-recipe-create",
  templateUrl: "./recipe-create.component.html",
  styleUrls: ["./recipe-create.component.css"],
})
export class RecipeCreateComponent implements OnInit {
  readonly allowedFileTypes = ["image/jpeg", "image/png"] as const;
  readonly separatorKeysCodes = [ENTER, SPACE] as const;

  canRemoveStep: boolean = false;
  imageUploaded: boolean = false;

  timeOptions: TimeOption[] = [
    { value: 30, viewValue: "30" },
    { value: 60, viewValue: "60" },
    { value: 90, viewValue: "90" },
    { value: 120, viewValue: "120" },
  ];

  servingOptions: ServingOption[] = [
    { value: 1, viewValue: "1" },
    { value: 2, viewValue: "2" },
    { value: 3, viewValue: "3" },
    { value: 4, viewValue: "4" },
    { value: 5, viewValue: "5" },
    { value: 6, viewValue: "6 " },
  ];

  recipe: RecipeCreateCommand = {
    userId: this._accountService.userValue.id,
    title: "",
    description: "",
    cookingTimeInMinutes: null,
    servingsCount: null,
    ingredientsSections: [{ title: "", ingredients: "" }],
    recipeSteps: [
      {
        index: 1,
        description: "",
      },
    ],
    tags: [],
  };

  constructor(
    private _locationService: LocationService,
    private _accountService: AccountService,
    private _imageService: ImageService,
    private _recipeSerivce: RecipesService,
    private _router: Router
  ) {}

  ngOnInit(): void {}

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || "").trim();
    if (value) {
      this.recipe.tags.push(value);
    }
    event.input.value = "";
  }

  removeTag(tag: string): void {
    const index = this.recipe.tags.indexOf(tag);
    if (index >= 0) {
      this.recipe.tags.splice(index, 1);
    }
  }

  addIngredientsSection(): void {
    this.recipe.ingredientsSections.push({
      title: "",
      ingredients: "",
    });
  }

  removeIngredientsSection(sectionToRemove: RecipeIngredientSectionDto): void {
    const index = this.recipe.ingredientsSections.indexOf(sectionToRemove);
    if (index >= 0) {
      this.recipe.ingredientsSections.splice(index, 1);
    }
  }

  addStep(): void {
    this.recipe.recipeSteps.push({ index: 0, description: "" });
    this.canRemoveStep = true;
    this.adjustStepIndices();
  }

  removeStep(step: RecipeStepDto): void {
    const index = this.recipe.recipeSteps.indexOf(step);
    if (index >= 0) {
      this.recipe.recipeSteps.splice(index, 1);
      if (this.recipe.recipeSteps.length === 1) {
        this.canRemoveStep = false;
      }
    }
    this.adjustStepIndices();
  }

  private adjustStepIndices(): void {
    for (let i: number = 0; i < this.recipe.recipeSteps.length; i++) {
      this.recipe.recipeSteps[i].index = i + 1;
    }
  }

  onSubmit(): void {
    this._recipeSerivce.createRecipe(this.recipe).subscribe(
      (recipeId) => {
        alert("Рецепт успешно создан!");
        this._router.navigate([`/recipes/${recipeId}`]);
      },
      (badRequest) => {
        alert(badRequest.error.Message);
      }
    );
  }

  async onImageUploaded(fileInput: any): Promise<void> {
    try {
      this.recipe.imageBase64 = await this._imageService.readImageAsBase64(
        fileInput.currentTarget.files?.item(0)
      );
    } catch (exception) {
      alert(exception);
      return;
    }
    this.imageUploaded = true;
  }

  trackByIndex(index: number, obj: any): number {
    return index;
  }

  onGoBackClick(): void {
    this._locationService.goBack();
  }
}
