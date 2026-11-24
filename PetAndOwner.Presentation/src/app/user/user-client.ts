import { Injectable, inject } from '@angular/core';
import { ReturnUserProfileDto } from '../api/api-client';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserClient {
  url = 'http://localhost:5253';

  private _httpClient = inject(HttpClient);
  getUser(token: string) {
    return this._httpClient.get<ReturnUserProfileDto>('http://localhost:5253/api/profile', {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
