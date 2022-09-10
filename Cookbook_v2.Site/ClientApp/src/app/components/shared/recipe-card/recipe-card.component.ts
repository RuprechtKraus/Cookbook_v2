import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { RecipePreviewDto } from "src/app/dtos/recipe-preview-dto";

@Component({
  selector: "app-recipe-card",
  templateUrl: "./recipe-card.component.html",
  styleUrls: ["./recipe-card.component.css"],
})
export class RecipeCardComponent implements OnInit {
  @Input() recipe: RecipePreviewDto;
  @Input() isClickable: boolean;
  onDetailPage: boolean;

  constructor(public _router: Router) {}

  ngOnInit(): void {
    this.onDetailPage = this.IsOnDetailsPage();
  }

  onClick(event: Event): void {
    if (this.isClickable) {
      const target = event.target as any;

      if (!(target.type === "button" || this.IsElementButtonChild(target))) {
        console.log("Clicked");
        this._router.navigate([`/recipes/details/${this.recipe.id}`]);
      }
    }
  }

  onLikeClick(): void {

  }

  onFavoriteClick(): void {
      
  }

  private IsOnDetailsPage(): boolean {
    var pattern = new RegExp("^/recipes/details/[0-9]+$");
    return (this.onDetailPage = pattern.test(this._router.url));
  }
  
  private IsElementButtonChild(element: any): boolean {
    let parent: any | null = element.parentElement;

    while (parent !== null) {
      if (parent.type === "button") {
        return true;
      }
      parent = parent.parentElement;
    }
    
    return false;
  }
}
