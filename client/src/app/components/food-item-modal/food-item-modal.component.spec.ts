import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodItemModalComponent } from './food-item-modal.component';

describe('FoodItemModalComponent', () => {
  let component: FoodItemModalComponent;
  let fixture: ComponentFixture<FoodItemModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodItemModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodItemModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
