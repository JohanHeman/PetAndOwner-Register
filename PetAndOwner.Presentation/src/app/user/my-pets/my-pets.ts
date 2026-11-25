import { Component, inject, signal } from '@angular/core';
import { UserService } from '../user-service';
import { ReturnPetDto, UpdatePetDto } from '../../api/api-client';
import { PetProfileCard } from '../pet-profile-card/pet-profile-card';

@Component({
  imports: [PetProfileCard],
  selector: 'app-my-pets',
  styleUrl: './my-pets.css',
  templateUrl: './my-pets.html',
})
export class MyPets {
  _userService = inject(UserService);


  pets = signal<ReturnPetDto[]>([]);

  ngOnInit(): void {
    this.loadUserPets();
  }

  loadUserPets(){
    this._userService.getUserPets().subscribe({
      next: (pets) => {
        this.pets.set(pets);
        console.log('retrieved pets');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }


  onSaved(petUpdate: UpdatePetDto) {
    this._userService.updateUserPet(petUpdate).subscribe({
      next: () => {
        console.log("success")
        this.loadUserPets();
      },
      error: (error) => {
        console.error('Failed to update', error);
      },
    })

  }
}
