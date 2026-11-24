import { Component } from '@angular/core';
import { FormsModule} from '@angular/forms';


@Component({
  imports: [FormsModule],
  selector: 'app-setup-profile',
  styleUrl: './setup-profile.css',
  templateUrl: './setup-profile.html',
})
export class SetupProfile {

  userName = ""

onSubmit() {
throw new Error('Method not implemented.');
}

}
