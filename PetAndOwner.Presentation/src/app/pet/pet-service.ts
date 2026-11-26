import { inject, Injectable } from '@angular/core';
import { ApiClient } from '../api/api-client';

@Injectable({
  providedIn: 'root',
})
export class PetService {

  private _client = inject(ApiClient)


  getAllPets()
  {
    return this._client.pets();
  }



}
