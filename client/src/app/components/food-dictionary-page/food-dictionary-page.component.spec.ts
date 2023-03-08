import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodDictionaryPageComponent } from './food-dictionary-page.component';

describe('FoodDictionaryPageComponent', () => {
  let component: FoodDictionaryPageComponent;
  let fixture: ComponentFixture<FoodDictionaryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodDictionaryPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodDictionaryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
