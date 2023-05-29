import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpCompleteComponent } from './sign-up-complete.component';

describe('SignUpCompleteComponent', () => {
  let component: SignUpCompleteComponent;
  let fixture: ComponentFixture<SignUpCompleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SignUpCompleteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignUpCompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
