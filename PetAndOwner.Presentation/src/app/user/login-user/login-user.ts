import { Component, inject, signal } from '@angular/core';
import { LoginRequest } from '../../api/api-client';
import { UserService } from '../user-service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  imports: [FormsModule],
  selector: 'app-login-user',
  styleUrl: './login-user.css',
  templateUrl: './login-user.html',
})
export class LoginUser {
  token = signal<string>('undefined');

  email = '';
  password = '';
  navigateUser = '';

  private userService = inject(UserService);
  private router = inject(Router);

  Login() {
    const request: LoginRequest = {
      email: this.email,
      password: this.password,
    };

    this.userService.login(request).subscribe({
      next: (res) => {
        this.userService.storeToken(res.accessToken!);
        console.log('login successful');

        this.userService.setRedirectUserValue()?.subscribe({
          next: (profile) => {
            if (profile.isCompleted) {
              this.navigateUser = '/home-page';
            } else {
              this.navigateUser = '/setup-profile';
            }
            this.redirectUser();
          },
        });
      },
      error: (error) => console.log(error),
    });
  }

  redirectUser() {
    this.router.navigate([this.navigateUser])
  }
}
