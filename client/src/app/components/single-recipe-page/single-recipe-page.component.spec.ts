import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleRecipePageComponent } from './single-recipe-page.component';

describe('SingleRecipePageComponent', () => {
  let component: SingleRecipePageComponent;
  let fixture: ComponentFixture<SingleRecipePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleRecipePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleRecipePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
