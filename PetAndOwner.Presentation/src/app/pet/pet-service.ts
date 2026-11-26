import { inject, Injectable, signal } from '@angular/core';
import { ApiClient, UpdatePetDto } from '../api/api-client';
import { PetClient } from './pet-client';

@Injectable({
  providedIn: 'root',
})
export class PetService {

  private _client = inject(ApiClient)

  private _petClient = inject(PetClient)

  token = signal<string | null>(localStorage.getItem('token'));

  storeToken(token: string) {
    this.token.set(token);
    localStorage.setItem('token', token);
  }

  getAllPets()
  {
    return this._client.pets();
  }

  getUserPets() {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._petClient.getUserPet(token);
  }

  updateUserPet(dto: UpdatePetDto) {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._petClient.updateUserPet(token, dto);
  }

  createUserPet(newPet: UpdatePetDto){
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._petClient.createNewPet(token, newPet)
  }

  deleteUserPet(id: string){
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._petClient.deletePet(token, id)
  }
}
