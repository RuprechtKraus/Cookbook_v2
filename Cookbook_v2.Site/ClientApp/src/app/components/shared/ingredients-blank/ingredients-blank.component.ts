import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { RecipeIngredientSectionDto } from 'src/app/dtos/recipe-ingredient-section-dto';

@Component({
  selector: 'app-ingredients-blank',
  templateUrl: './ingredients-blank.component.html',
  styleUrls: ['./ingredients-blank.component.css']
})
export class IngredientsBlankComponent implements OnInit {
  @Input() removable: boolean = false;
  @Input() section: RecipeIngredientSectionDto;
  @Output() remove = new EventEmitter<RecipeIngredientSectionDto>();

  constructor( ) { 
    
  }

  ngOnInit(): void {
  }

  onClick(): void {
    this.remove.emit(this.section);
  }

}
