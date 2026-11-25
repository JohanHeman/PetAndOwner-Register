import { Component, inject, input } from '@angular/core';
import { ReturnPetDto } from '../../api/api-client';
import { UserService } from '../user-service';

@Component({
  imports: [],
  selector: 'app-pet-profile-card',
  styleUrl: './pet-profile-card.css',
  templateUrl: './pet-profile-card.html',
})
export class PetProfileCard {

  _userService = inject(UserService);

  pet = input.required<ReturnPetDto>();
}
