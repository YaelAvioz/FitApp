import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OurMentorsComponent } from './our-mentors.component';

describe('OurMentorsComponent', () => {
  let component: OurMentorsComponent;
  let fixture: ComponentFixture<OurMentorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OurMentorsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OurMentorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
