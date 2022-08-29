import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';
  currentLink? = null;
  previousUrl: string = null;
  currentUrl: string = null;

  constructor(
    private _router: Router
  ) {
    
  }

  onRouteActivate(): void {
    switch(this._router.url)
    {
      case '/':
        this.currentLink = document.getElementById('main-page-link');
        break;
      case '/recipes':
        this.currentLink = document.getElementById('recipes-page-link');
        break;
      case '/favorites':
        this.currentLink = document.getElementById('favorites-page-link');
        break;
      default:
        this.currentLink = null;
        break;
    }
    if (this.currentLink !== null && this.currentLink)
      this.currentLink.classList.add('nav-bar__active');
  }

  onRouteDeactivate(): void {
    if (this.currentLink !== null && this.currentLink)
      this.currentLink.classList.remove('nav-bar__active');
  }
}
