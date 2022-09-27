import { Component, OnInit } from "@angular/core";
import { ENTER, SPACE } from "@angular/cdk/keycodes";

import { TimeOption } from "../../../interfaces/time-option";
import { MatChipInputEvent } from "@angular/material/chips";
import { ServingOption } from "../../../interfaces/serving-options";
import { LocationService } from "../../../services/location.service";
import { AccountService } from "src/app/services/account.service";
import { ImageService } from "src/app/services/image.service";
import { RecipesService } from "src/app/services/recipes.service";
import { ActivatedRoute, Router } from "@angular/router";
import { RecipeCreateCommand } from "src/app/commands/recipe-create-command";
import { RecipeIngredientSectionDto } from "src/app/dtos/recipe-ingredient-section-dto";
import { RecipeStepDto } from "src/app/dtos/recipe-step-dto";
import { RecipeEditorDto } from "src/app/dtos/recipe-editor-dto";

@Component({
  selector: "app-recipe-create",
  templateUrl: "./recipe-create.component.html",
  styleUrls: ["./recipe-create.component.css"],
})
export class RecipeCreateComponent implements OnInit {
  readonly allowedFileTypes = ["image/jpeg", "image/png"] as const;
  readonly separatorKeysCodes = [ENTER, SPACE] as const;

  id: number;
  editMode: boolean;
  canRemoveStep: boolean = false;
  imageUploaded: boolean = false;

  timeOptions: TimeOption[] = [
    { value: 30, viewValue: "30" },
    { value: 35, viewValue: "35" },
    { value: 40, viewValue: "40" },
    { value: 45, viewValue: "45" },
    { value: 50, viewValue: "50" },
    { value: 55, viewValue: "55" },
    { value: 60, viewValue: "60" },
    { value: 65, viewValue: "65" },
    { value: 70, viewValue: "70" },
    { value: 75, viewValue: "75" },
    { value: 80, viewValue: "80" },
    { value: 85, viewValue: "85" },
    { value: 90, viewValue: "90" },
    { value: 95, viewValue: "95" },
    { value: 100, viewValue: "100" },
    { value: 105, viewValue: "105" },
    { value: 110, viewValue: "110" },
    { value: 115, viewValue: "115" },
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
    private _recipeService: RecipesService,
    private _route: ActivatedRoute,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.id = this._route.snapshot.params["id"];
    this.editMode = this.id !== undefined;

    if (this.editMode) {
      this._recipeService.getRecipeEditor(this.id).subscribe(
        (recipeDetails) => {
          this.setRecipeForEdit(recipeDetails);
        },
        () => {
          this._router.navigate(["/"]);
        }
      );
    }
  }

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

  private setRecipeForEdit(recipeEditor: RecipeEditorDto): void {
    this.recipe.title = recipeEditor.title;
    this.recipe.description = recipeEditor.description;
    this.recipe.cookingTimeInMinutes = recipeEditor.cookingTimeInMinutes;
    this.recipe.servingsCount = recipeEditor.servingsCount;
    this.recipe.ingredientsSections = recipeEditor.ingredientsSections;
    this.recipe.recipeSteps = recipeEditor.recipeSteps;
    this.recipe.tags = recipeEditor.tags;
    this.recipe.imageBase64 = recipeEditor.imageBase64;
    this.imageUploaded = true;
    this.canRemoveStep = true;
  }

  onSubmit(): void {
    if (this.editMode) {
      this.updateRecipe();
    } else {
      this.createRecipe();
    }
  }

  isEditMode(): boolean {
    return this.id !== undefined;
  }

  private updateRecipe(): void {
    this._recipeService.updateRecipe(this.id, this.recipe).subscribe(
      () => {
        alert("Рецепт успешно обновлен!");
        this._router.navigate([`/recipes/details/${this.id}`]);
      },
      (badRequest) => {
        alert(badRequest.error.Message);
      }
    );
  }

  private createRecipe(): void {
    this._recipeService.createRecipe(this.recipe).subscribe(
      (recipeId) => {
        alert("Рецепт успешно создан!");
        this._router.navigate([`/recipes/details/${recipeId}`]);
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
