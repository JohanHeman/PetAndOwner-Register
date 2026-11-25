import { Component, inject } from '@angular/core';
import { UserService } from '../user-service';

@Component({
  imports: [],
  selector: 'app-pet-profile-card',
  styleUrl: './pet-profile-card.css',
  templateUrl: './pet-profile-card.html',
})
export class PetProfileCard {

  _userService = inject(UserService);

  //pet:  = {};

  getPet(){
    this._userService.getPetProfile().subscribe
  }
}
