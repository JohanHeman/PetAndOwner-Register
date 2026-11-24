import { Component, inject, signal } from '@angular/core';
import { LoginRequest } from '../../api/api-client';
import { UserService } from '../user-service';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-login-user',
  styleUrl: './login-user.css',
  templateUrl: './login-user.html',
})
export class LoginUser {

  token = signal<string>("undefined");

  email = ""
  password = "";

  private userService = inject(UserService);

  Login() {
    const request: LoginRequest = {
      email: this.email,
      password: this.password,
    };

    this.userService.login(request).subscribe({
    next: (res) => {
        this.userService.storeToken(res.accessToken!);
        console.log('login successful');

        //call function to redirect user

    },
      error: error => console.log(error)
    });
  }

    checkUserProfile(){

    }
}
