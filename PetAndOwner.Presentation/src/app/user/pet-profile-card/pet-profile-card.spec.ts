import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PetProfileCard } from './pet-profile-card';

describe('PetProfileCard', () => {
  let component: PetProfileCard;
  let fixture: ComponentFixture<PetProfileCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PetProfileCard],
    }).compileComponents();

    fixture = TestBed.createComponent(PetProfileCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
