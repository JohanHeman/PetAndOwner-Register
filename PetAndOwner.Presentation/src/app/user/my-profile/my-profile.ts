import { Component, inject, signal } from '@angular/core';
import { UserService } from '../user-service';
import { ReturnUserProfileDto, UpdateUserProfileDto } from '../../api/api-client';
import { KeyValuePipe } from '@angular/common';
import { EditProfile } from '../edit-profile/edit-profile';
import { DeleteUser } from '../delete-user/delete-user';

@Component({
  imports: [KeyValuePipe, EditProfile, DeleteUser],
  selector: 'app-my-profile',
  styleUrl: './my-profile.css',
  templateUrl: './my-profile.html',
})
export class MyProfile {
  private _userService = inject(UserService);

  dto = signal<ReturnUserProfileDto>({});

  updateDto = signal<UpdateUserProfileDto>({});

  valueToEdit = signal('');

  wantsToDelete = signal(false);

  deleteUser() {
    this._userService.deleteUser().subscribe({
      next: () => {
        console.log('user deleted');
        this.wantsToDelete.set(false);
      },
      error: (err) => {
        console.error('Failed to delete user', err);
      },
    });
  }

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile() {
    this._userService.getUserProfile().subscribe({
      next: (profile) => {
        this.dto.set(profile);

        this.updateDto.set({
          name: profile.name,
          birthDate: profile.birthDate,
          gender: profile.gender,
          phoneNumber: profile.phoneNumber,
          cityName: profile.cityName,
          email: profile.email,
          country: profile.country,
        });

        console.log('retrieved profile');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  // keyof is the property to change, recieved from the htmlusing the key from KeyValuePipe
  updateProfile(property: keyof UpdateUserProfileDto, newValue: string) {
    this.updateDto.update((current) => ({
      ...current,
      [property]: newValue,
    }));

    this._userService.updateUserProfile(this.updateDto()).subscribe({
      next: () => {
        this.valueToEdit.set('');
        this.getUserProfile();
      },
      error: (error) => {
        console.error('Failed to update profile', error);
      },
    });

    this.getUserProfile();
  }
}
