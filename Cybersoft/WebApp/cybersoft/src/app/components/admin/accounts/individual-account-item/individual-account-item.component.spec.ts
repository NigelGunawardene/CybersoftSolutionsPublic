import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualAccountItemComponent } from './individual-account-item.component';

describe('IndividualAccountItemComponent', () => {
  let component: IndividualAccountItemComponent;
  let fixture: ComponentFixture<IndividualAccountItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualAccountItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndividualAccountItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
