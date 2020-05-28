/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CarerListComponent } from './carer-list.component';

describe('CarerListComponent', () => {
  let component: CarerListComponent;
  let fixture: ComponentFixture<CarerListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarerListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
