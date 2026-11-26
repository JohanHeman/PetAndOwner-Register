import { ReturnPetDto, ReturnUserProfileDto, UpdateUserProfileDto } from './../../api/api-client';
import { Component, inject } from '@angular/core';
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

  genders = [
  {  label: 'Male'},
  {  label: 'Female'},
  {  label: 'Unknown'}];

  private _router = inject(Router)

  onSubmit() {
    this.setupUserProfile();
  }

  setupUserProfile() {
    if (!this.user) return;

    this._userService.setupUserProfile(this.user).subscribe({
      next: () => {
        console.log('profile setup complete');
        this._router.navigate(["/"]);
      }
    });
  }
}
