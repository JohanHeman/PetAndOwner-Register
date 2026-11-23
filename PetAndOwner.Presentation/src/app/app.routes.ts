import { Routes } from '@angular/router';
import { RegisterUser } from './user/register-user/register-user';
import { Start } from './home/start/start';

export const routes: Routes = [
  {path: "register-user", component: RegisterUser},
  {path: "", component: Start}
];
