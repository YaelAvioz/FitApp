import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorFlipCardComponent } from './mentor-flip-card.component';

describe('MentorFlipCardComponent', () => {
  let component: MentorFlipCardComponent;
  let fixture: ComponentFixture<MentorFlipCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MentorFlipCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MentorFlipCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
