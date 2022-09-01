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
  onDetailPage: boolean;

  constructor(public _router: Router) {}

  ngOnInit(): void {
    this.onDetailPage = this.IsOnDetailsPage();
  }

  private IsOnDetailsPage(): boolean {
    var pattern = new RegExp("^\/recipes\/details\/[0-9]+$");
    return this.onDetailPage = pattern.test(this._router.url);
  }
}
