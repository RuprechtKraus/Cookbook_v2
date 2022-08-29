import { Component, OnInit } from '@angular/core';

import { RecipesService } from '../../../services/recipes.service';
import { LocationService } from '../../../services/location.service';
import { RecipePreview } from 'src/app/interfaces/recipe';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {
  favRecipes: RecipePreview[] = [];

  constructor(
    private _recipesService: RecipesService,
    private _locationService: LocationService
  ) { }

  ngOnInit(): void {
    this.getFavoriteRecipes();
  }

  getFavoriteRecipes(): void {
    // this._recipesService.getRecipes()
    //   .subscribe((recipes: RecipeToLoad[]) => this.favRecipes = recipes.filter(
    //     r => (r.id === 1 || r.id === 3)
    //     ));
  }

  onGoBackClick(): void {
    this._locationService.goBack();
  }

}
