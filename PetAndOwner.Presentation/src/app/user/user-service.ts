import { DeleteUser } from './delete-user/delete-user';
import { UserClient } from './user-client';
import { HttpClient } from '@angular/common/http';
import { inject, signal, Injectable } from '@angular/core';
import {
  ApiClient,
  RegisterRequest,
  LoginRequest,
  ReturnUserProfileDto,
  UpdateUserProfileDto,
  UpdatePetDto,
} from '../api/api-client';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _apiClient = inject(ApiClient);

  private _userClient = inject(UserClient);

  token = signal<string | null>(localStorage.getItem('token'));

  storeToken(token: string) {
    this.token.set(token);
    localStorage.setItem('token', token);
  }

  register(request: RegisterRequest) {
    return this._apiClient.register(request);
  }

  login(request: LoginRequest) {
    return this._apiClient.login(request);
  }

  setRedirectUserValue() {
    const token = this.token();
    if (token === null) {
      return;
    }
    return this._userClient.getUser(token);
  }

  setupUserProfile(request: UpdateUserProfileDto) {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._userClient.setupUserProfile(token, request);
  }

  getUserProfile() {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._userClient.getUserProfile(token);
  }

  updateUserProfile(dto: UpdateUserProfileDto) {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._userClient.updateUserProfile(token, dto);
  }

  deleteUser() {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._userClient.deleteUser(token);
  }

  getAllUsers() {
    return this._apiClient.persons();
  }

  getUserPets() {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._userClient.getUserPet(token);
  }

  updateUserPet(dto: UpdatePetDto) {
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }

    return this._userClient.updateUserPet(token, dto);
  }

  createUserPet(newPet: UpdatePetDto){
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._userClient.createNewPet(token, newPet)
  }

  deleteUserPet(id: string){
    const token = this.token();
    if (token === null) {
      throw new Error('User is not authenticated');
    }
    return this._userClient.deletePet(token, id)
  }
}
