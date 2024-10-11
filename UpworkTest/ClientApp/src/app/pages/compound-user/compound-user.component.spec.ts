import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompoundUserComponent } from './compound-user.component';

describe('CompoundUserComponent', () => {
  let component: CompoundUserComponent;
  let fixture: ComponentFixture<CompoundUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompoundUserComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompoundUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
