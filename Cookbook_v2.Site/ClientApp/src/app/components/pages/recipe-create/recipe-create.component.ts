import { Component, OnInit } from "@angular/core";
import { ENTER, SPACE } from "@angular/cdk/keycodes";

import { TimeOption } from "../../../interfaces/time-option";
import { MatChipInputEvent } from "@angular/material/chips";
import { ServingOption } from "../../../interfaces/serving-options";
import { LocationService } from "../../../services/location.service";
import { IngredientsSection } from "src/app/interfaces/ingredients-section";
import { RecipeDTO } from "src/app/dtos/recipe-dto";
import { AccountService } from "src/app/services/account.service";
import { ImageService } from "src/app/services/image.service";
import { RecipesService } from "src/app/services/recipes.service";
import { RecipeStep } from "src/app/interfaces/recipe-step";
import { Router } from "@angular/router";

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

  recipe: RecipeDTO = {
    name: "",
    description: "",
    cookingTimeInMinutes: null,
    servingsAmount: null,
    userID: this._accountService.userValue.userID,
    user: this._accountService.userValue.login,
    ingredientsSections: [{ name: "", products: "" }],
    recipeSteps: [
      {
        stepIndex: 1,
        description: "",
      },
    ],
    tags: [],
    imageBase64: "",
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
      name: "",
      products: "",
    });
  }

  removeIngredientsSection(ingToRemove: IngredientsSection): void {
    const index = this.recipe.ingredientsSections.indexOf(ingToRemove);
    if (index >= 0) {
      this.recipe.ingredientsSections.splice(index, 1);
    }
  }

  addStep(): void {
    this.recipe.recipeSteps.push({ stepIndex: 0, description: "" });
    this.canRemoveStep = true;
    this.adjustStepIndices();
  }

  removeStep(step: RecipeStep): void {
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
      this.recipe.recipeSteps[i].stepIndex = i + 1;
    }
  }

  onSubmit(): void {
    this._recipeSerivce.createRecipe(this.recipe).subscribe((recipeID) => {
      alert("Рецепт успешно создан!");
      this._router.navigate([`/recipes/${recipeID}`]);
    });
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
