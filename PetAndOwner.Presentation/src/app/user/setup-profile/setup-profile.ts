import { ReturnPetDto, ReturnUserProfileDto, UpdateUserProfileDto } from './../../api/api-client';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../user-service';
import { Router } from '@angular/router';

@Component({
  imports: [FormsModule],
  selector: 'app-setup-profile',
  styleUrl: './setup-profile.css',
  templateUrl: './setup-profile.html',
})
export class SetupProfile {
  _userService = inject(UserService);

  user: UpdateUserProfileDto = {};

  statusMessage = signal("");

  genders = [
  {  label: 'Male'},
  {  label: 'Female'},
  {  label: 'Unknown'}];

  private _router = inject(Router)

  onSubmit() {
    this.setupUserProfile();
  }

  setupUserProfile() {

    this._userService.setupUserProfile(this.user).subscribe({
      next: () => {
        this._router.navigate(["/"]);
      },
      error: (error) => {
        this.statusMessage.set('Registration failed. See console for more info');
        console.log(error);
      }
    });
  }
}
