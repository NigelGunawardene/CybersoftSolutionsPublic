import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualAllOrderComponent } from './individual-all-order.component';

describe('IndividualAllOrderComponent', () => {
  let component: IndividualAllOrderComponent;
  let fixture: ComponentFixture<IndividualAllOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualAllOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndividualAllOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
