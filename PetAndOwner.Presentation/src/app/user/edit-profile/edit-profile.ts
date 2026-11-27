import { Component, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-edit-profile',
  styleUrl: './edit-profile.css',
  templateUrl: './edit-profile.html',
})
export class EditProfile {
  valueToChange = input<string>();

  currentValue = input<any>();

  newValue = signal('');

  genders = [
  {  label: 'Male'},
  {  label: 'Female'},
  {  label: 'Unknown'}];

  sendNewValue = output<string>();

  isDate = input<boolean>()

  onSubmit() {
    this.sendNewValue.emit(this.newValue());
  }
}
