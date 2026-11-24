import { UserClient } from './user-client';
import { HttpClient } from '@angular/common/http';
import { inject, signal, Injectable } from '@angular/core';
import { ApiClient, RegisterRequest, LoginRequest, ReturnUserProfileDto } from '../api/api-client';

@Injectable({
  providedIn: 'root',
})
export class UserService {


  private _apiClient = inject(ApiClient);

  private _userClient = inject(UserClient)

  token = signal<string | null>(localStorage.getItem('token'));

  navigateUser = "";

  storeToken(token: string) {
  this.token.set(token);
  localStorage.setItem('token', token);
  }

  register(request: RegisterRequest){
    return this._apiClient.register(request);
  }

  login(request: LoginRequest){
    return this._apiClient.login(request);
  }

  redirectUser()
  {
    const token = this.token();
    if(token === null)
    {
      return;
    }
    this._userClient.getUser(token).subscribe({
      next: (user) => {
        if(Object.values(user).some(value => value === null)){

          this.navigateUser = "/setup-profile";

        }
        else{
          this.navigateUser = "/home";
        }
      }
    })
  }


}
