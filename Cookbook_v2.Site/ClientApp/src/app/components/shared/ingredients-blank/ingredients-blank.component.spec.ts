import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientsBlankComponent } from './ingredients-blank.component';

describe('IngredientsBlankComponent', () => {
  let component: IngredientsBlankComponent;
  let fixture: ComponentFixture<IngredientsBlankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngredientsBlankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientsBlankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
