import { Injectable, inject } from '@angular/core';
import { ReturnPetDto, UpdatePetDto } from '../api/api-client';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PetClient {
  url = 'http://localhost:5253';

  private _httpClient = inject(HttpClient);

  getUserPet(token: string){
    return this._httpClient.get<ReturnPetDto[]>(`${this.url}/api/profile/pet`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
  }

  updateUserPet(token: string, request: UpdatePetDto) {
    console.log(request.petId)
    return this._httpClient.patch(`${this.url}/api/profile/pet/${request.petId}`, request, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  createNewPet(token: string, request: UpdatePetDto) {
    return this._httpClient.post<void>(`${this.url}/api/profile/pet`, request, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  deletePet(token: string, petId: string)
  {
    return this._httpClient.delete(`${this.url}/api/profile/pet/${petId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
