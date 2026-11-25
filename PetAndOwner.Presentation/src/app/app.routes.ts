import { Routes } from '@angular/router';
import { RegisterUser } from './user/register-user/register-user';
import { LoginUser } from './user/login-user/login-user';
import { Start } from './home/start/start';
import { HomePage } from './home/home-page/home-page';
import { RegisterView } from './register/register-view/register-view';
import { SetupProfile } from './user/setup-profile/setup-profile';
import { MyProfile } from './user/my-profile/my-profile';
import { Profile } from './user/profile/profile';
import { MyPets } from './user/my-pets/my-pets';

export const routes: Routes = [
  {path: "register-user", component: RegisterUser},
  {path: "register-view", component: RegisterView},
  {path: "login-user", component: LoginUser},
  {path: "home-page", component: HomePage},
  {path: "", component: Start},
  {path: "setup-profile", component: SetupProfile},
  {path: "my-profile", component: MyProfile},
  {path: "profile", component: Profile},
  {path: "my-pets", component: MyPets}

];
