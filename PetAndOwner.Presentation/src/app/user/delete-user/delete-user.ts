import { Component, output, signal } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-delete-user',
  styleUrl: './delete-user.css',
  templateUrl: './delete-user.html',
})
export class DeleteUser {

  deleteStatus = output<boolean>();

}
