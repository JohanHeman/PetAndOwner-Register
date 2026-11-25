import { Injectable, inject } from '@angular/core';
import { ProfileStatusDto, ReturnUserProfileDto, UpdateUserProfileDto } from '../api/api-client';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserClient {
  url = 'http://localhost:5253';

  private _httpClient = inject(HttpClient);
  getUser(token: string) {
    return this._httpClient.get<ProfileStatusDto>(`${this.url}/api/profile`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  setupUserProfile(token:string, request: UpdateUserProfileDto)
  {
    return this._httpClient.patch<void>(`${this.url}/api/profile/setup`, request, {
      headers: {
        Authorization: `Bearer ${token}`
      },
    });
  }

  getUserProfile(token: string)
  {
    return this._httpClient.get<ReturnUserProfileDto>(`${this.url}/api/user-profile`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
  }
}
