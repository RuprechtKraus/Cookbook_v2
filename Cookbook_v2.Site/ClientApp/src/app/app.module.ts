import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './components/pages/main/main.component';
import { NavBarComponent } from './components/layout/nav-bar/nav-bar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { RecipesComponent } from './components/pages/recipes/recipes.component';
import { RecipeCardComponent } from './components/shared/recipe-card/recipe-card.component';
import { RecipeDetailsComponent } from './components/pages/recipe-details/recipe-details.component';
import { RecipeCreateComponent } from './components/pages/recipe-create/recipe-create.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IngredientsBlankComponent } from './components/shared/ingredients-blank/ingredients-blank.component';
import { MyProfileComponent } from './components/pages/my-profile/my-profile.component';
import { FavoritesComponent } from './components/pages/favorites/favorites.component';
import { ModalWindowComponent } from './components/shared/modal-window/modal-window.component';
import { LoginModalComponent } from './components/shared/login-modal/login-modal.component';
import { RegistrationModalComponent } from './components/shared/registration-modal/registration-modal.component';
import { UnauthorizedModalComponent } from './components/shared/unauthorized-modal/unauthorized-modal.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    NavBarComponent,
    FooterComponent,
    RecipesComponent,
    RecipeCardComponent,
    RecipeDetailsComponent,
    RecipeCreateComponent,
    IngredientsBlankComponent,
    MyProfileComponent,
    FavoritesComponent,
    ModalWindowComponent,
    LoginModalComponent,
    RegistrationModalComponent,
    UnauthorizedModalComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatSelectModule,
    FormsModule,
    NgSelectModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
