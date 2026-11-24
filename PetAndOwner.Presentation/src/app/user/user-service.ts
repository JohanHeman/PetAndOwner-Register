import { inject, signal, Injectable } from '@angular/core';
import { ApiClient, RegisterRequest, LoginRequest } from '../api/api-client';

@Injectable({
  providedIn: 'root',
})
export class UserService {


  private apiClient = inject(ApiClient);

  token = signal<string | null>(localStorage.getItem('token'));

  storeToken(token: string) {
  this.token.set(token);
  localStorage.setItem('token', token);
  }

  register(request: RegisterRequest){
    return this.apiClient.register(request);
  }

  login(request: LoginRequest){
    return this.apiClient.login(request);
  }


}
