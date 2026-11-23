import { inject, Injectable } from '@angular/core';
import { ApiClient, RegisterRequest } from '../api/api-client';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  private apiClient = inject(ApiClient);

  register(request: RegisterRequest){
    return this.apiClient.register(request);
  }


}
