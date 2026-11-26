import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NewPetForm } from './new-pet-form';

describe('NewPetForm', () => {
  let component: NewPetForm;
  let fixture: ComponentFixture<NewPetForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewPetForm],
    }).compileComponents();

    fixture = TestBed.createComponent(NewPetForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
