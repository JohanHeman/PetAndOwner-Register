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

newValue = signal("");


sendNewValue = output<string>();

onSubmit()
{
  this.sendNewValue.emit(this.newValue());
}
// take this value
//set the new value
// send it out to the parent and create an object from it there


}
