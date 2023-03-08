import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorsPageComponent } from './mentors-page.component';

describe('MentorsPageComponent', () => {
  let component: MentorsPageComponent;
  let fixture: ComponentFixture<MentorsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MentorsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MentorsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
