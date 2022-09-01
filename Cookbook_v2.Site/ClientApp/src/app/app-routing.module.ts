import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent } from './components/pages/main/main.component';
import { RecipesComponent } from './components/pages/recipes/recipes.component';
import { RecipeDetailsComponent } from './components/pages/recipe-details/recipe-details.component';
import { RecipeCreateComponent } from './components/pages/recipe-create/recipe-create.component';
import { MyProfileComponent } from './components/pages/my-profile/my-profile.component';
import { FavoritesComponent } from './components/pages/favorites/favorites.component';
import { AuthGuard } from './helpers/auth.guard';


const routes: Routes = [
  { path: '', component: MainComponent},
  { path: 'recipes', component: RecipesComponent },
  { path: 'recipes/details/:id', component: RecipeDetailsComponent },
  { path: 'create-recipe', component: RecipeCreateComponent, canActivate: [AuthGuard] },
  { path: 'my-profile', component: MyProfileComponent, canActivate: [AuthGuard] },
  { path: 'favorites', component: FavoritesComponent, canActivate: [AuthGuard] },
  { path: "**", redirectTo: "/" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
