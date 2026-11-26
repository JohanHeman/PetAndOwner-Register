import { Component, inject, signal } from '@angular/core';
import { RegisterRequest } from '../../api/api-client';
import { UserService } from '../user-service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  imports: [FormsModule],
  selector: 'app-register-user',
  styleUrl: './register-user.css',
  templateUrl: './register-user.html',
})
export class RegisterUser {
  email = '';
  password = '';

  private userService = inject(UserService);

  statusMessage = signal('');

  private _router = inject(Router)

  register() {
    const request: RegisterRequest = {
      email: this.email,
      password: this.password,
    };

    this.userService.register(request).subscribe({
      next: () => {
        console.log('registration successful');
        this._router.navigate(["/login-user"])
      },
      error: (error) => {
        this.statusMessage.set('Registration failed. See console for more info');
        console.log(error);
      },
    });
  }
}
