import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubNavComponent } from './subnav.component';

describe('SubnavComponent', () => {
  let component: SubNavComponent;
  let fixture: ComponentFixture<SubNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubNavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
