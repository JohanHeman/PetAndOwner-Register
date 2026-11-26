import { Component, inject, signal } from '@angular/core';
import { PetService } from '../../pet/pet-service';
import { ReturnPetDto, UpdatePetDto } from '../../api/api-client';
import { PetProfileCard } from '../../pet/pet-profile-card/pet-profile-card';
import { NewPetForm } from '../../pet/new-pet-form/new-pet-form';

@Component({
  imports: [PetProfileCard, NewPetForm],
  selector: 'app-my-pets',
  styleUrl: './my-pets.css',
  templateUrl: './my-pets.html',
})
export class MyPets {
  _petService = inject(PetService);


  pets = signal<ReturnPetDto[]>([]);

  ngOnInit(): void {
    this.loadUserPets();
  }

  loadUserPets(){
    this._petService.getUserPets().subscribe({
      next: (pets) => {
        this.pets.set(pets);
        console.log('retrieved pets');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }


  onSave(petUpdate: UpdatePetDto) {
    this._petService.updateUserPet(petUpdate).subscribe({
      next: () => {
        console.log(`${petUpdate.name} updated`)
        this.loadUserPets();
      },
      error: (error) => {
        console.error('Failed to update', error);
      },
    })
  }

  showForm = signal(false)

  addPetClicked(){
    this.showForm.update(f => f = !f)
    console.log("addpetclicked", this.showForm)

  }

  saveNewPet(newPet: UpdatePetDto){
    this._petService.createUserPet(newPet).subscribe({
      next: () => {
        console.log("new pet saved");
        this.showForm.set(false)
        this.loadUserPets();
      }
    })
  }

  onPetDeleted(petId: string){
    console.log("pet delete clicked")
    this._petService.deleteUserPet(petId).subscribe({
      next: () => {
        console.log("pet deleted");
        this.loadUserPets();
      }
    })
  }

}
