import { Component, inject, signal } from '@angular/core';
import { RegisterRequest } from '../../api/api-client';
import { UserService } from '../user-service';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-register-user',
  styleUrl: './register-user.css',
  templateUrl: './register-user.html',
})
export class RegisterUser {
  email = ""
  password = "";

  private userService = inject(UserService);

  register() {
    const request: RegisterRequest = {
      email: this.email,
      password: this.password,
    };

    this.userService.register(request).subscribe({
      next: () => console.log("registration successful"),
      error: error => console.log(error)
    });

  }
}
