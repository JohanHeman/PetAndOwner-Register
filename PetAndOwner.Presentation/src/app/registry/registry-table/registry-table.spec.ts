import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterTable } from './registry-table';

describe('RegisterTable', () => {
  let component: RegisterTable;
  let fixture: ComponentFixture<RegisterTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterTable],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
