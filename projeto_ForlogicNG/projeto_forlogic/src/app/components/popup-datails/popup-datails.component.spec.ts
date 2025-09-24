import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupDatailsComponent } from './popup-datails.component';

describe('PopupDatailsComponent', () => {
  let component: PopupDatailsComponent;
  let fixture: ComponentFixture<PopupDatailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PopupDatailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PopupDatailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
