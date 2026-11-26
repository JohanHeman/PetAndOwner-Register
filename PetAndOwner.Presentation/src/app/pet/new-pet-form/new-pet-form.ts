import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpdatePetDto } from '../../api/api-client';


@Component({
  imports: [FormsModule],
  selector: 'app-new-pet-form',
  styleUrl: './new-pet-form.css',
  templateUrl: './new-pet-form.html',
})
export class NewPetForm {

  pet = signal<UpdatePetDto>({})

  saved = output<UpdatePetDto>();
  canceled = output<string>();

  genders = [
  {  label: 'Male', value: 0 },
  {  label: 'Female', value: 1, },
  {  label: 'Unknown', value: 2, }];

  genderLable(value?: number){
    return this.genders.find(g => g.value === value)?.label
  }

  onSubmit() {
    this.saved.emit(this.pet());
  }

  cancel(){
    this.canceled.emit("cancel pressed");
  }

}
