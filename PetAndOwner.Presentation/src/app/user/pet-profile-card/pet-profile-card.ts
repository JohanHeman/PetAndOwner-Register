import { Component, inject, input, output, signal } from '@angular/core';
import { ReturnPetDto, UpdatePetDto } from '../../api/api-client';
import { UserService } from '../user-service';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-pet-profile-card',
  styleUrl: './pet-profile-card.css',
  templateUrl: './pet-profile-card.html',
})
export class PetProfileCard {

  _userService = inject(UserService);

  pet = input.required<ReturnPetDto>();
  saved = output<UpdatePetDto>();

  genders = [
  {  label: 'Male', value: 0 },
  {  label: 'Female', value: 1, },
  {  label: 'Unknown', value: 2, }];

  genderLable(value?: number){
    return this.genders.find(g => g.value === value)?.label
  }
  editing = signal(false);
  activeField = signal('');
  draft: string | number = '';

  edit(field: string, currentValue: any) {
    this.activeField.set(field);
    this.draft = currentValue;
    console.log("current value", currentValue)
  }


  save() {
    const field = this.activeField();

    const newValue = this.draft
    const oldValue = (this.pet() as any)[field];
    const isUnchanged = newValue === oldValue;

    console.log("newValue", newValue)
    if (isUnchanged) {
      this.activeField.set('');
      return;
    }

    const petUpdate = { ...this.pet() };
    petUpdate[field] = newValue;

    console.log("petUpdate",petUpdate)
    this.saved.emit(petUpdate);

    this.activeField.set('');
  }

  cancel() {
    this.activeField.set('');
  }
}
