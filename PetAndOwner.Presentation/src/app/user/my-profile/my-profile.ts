import { Component, inject, signal } from '@angular/core';
import { UserService } from '../user-service';
import { ReturnUserProfileDto } from '../../api/api-client';
import { KeyValuePipe } from '@angular/common';
import { EditProfile } from '../edit-profile/edit-profile';

@Component({
  imports: [KeyValuePipe, EditProfile],
  selector: 'app-my-profile',
  styleUrl: './my-profile.css',
  templateUrl: './my-profile.html',
})

export class MyProfile {
  private _userService = inject(UserService);

  dto = signal<ReturnUserProfileDto>({});

  valueToEdit = signal("");


  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile() {
    this._userService.getUserProfile().subscribe({
      next: (profile) => {
        this.dto.set(profile);
        console.log('retrieved profile');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  Edit(){

  }



}
