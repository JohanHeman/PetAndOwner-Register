import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';


@Component({
  imports: [RouterLink],
  selector: 'app-my-profile',
  styleUrl: './my-profile.css',
  templateUrl: './my-profile.html',
})
export class MyProfile {

  // inject user service
  // create function to trigger the get request from the service to get all the user profiles

}
