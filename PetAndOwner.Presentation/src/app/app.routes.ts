import { Routes } from '@angular/router';
import { RegisterUser } from './user/register-user/register-user';
import { LoginUser } from './user/login-user/login-user';

import { Start } from './home/start/start';
import { SetupProfile } from './user/setup-profile/setup-profile';

export const routes: Routes = [
  {path: "register-user", component: RegisterUser},
  {path: "login-user", component: LoginUser},
  {path: "", component: Start},
  {path: "setup-profile", component: SetupProfile}
];
