import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { IngredientsSection } from 'src/app/interfaces/ingredients-section';

@Component({
  selector: 'app-ingredients-blank',
  templateUrl: './ingredients-blank.component.html',
  styleUrls: ['./ingredients-blank.component.css']
})
export class IngredientsBlankComponent implements OnInit {
  @Input() removable: boolean = false;
  @Input() section: IngredientsSection;
  @Output() remove = new EventEmitter<IngredientsSection>();

  constructor( ) { 
    
  }

  ngOnInit(): void {
  }

  onClick(): void {
    this.remove.emit(this.section);
  }

}
