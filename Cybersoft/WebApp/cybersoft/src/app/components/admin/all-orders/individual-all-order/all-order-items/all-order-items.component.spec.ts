import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllOrderItemsComponent } from './all-order-items.component';

describe('AllOrderItemsComponent', () => {
  let component: AllOrderItemsComponent;
  let fixture: ComponentFixture<AllOrderItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllOrderItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllOrderItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
