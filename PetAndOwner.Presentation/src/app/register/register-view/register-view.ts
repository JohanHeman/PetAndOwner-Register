import { PetService } from '../../pet/pet-service';
import { UserService } from '../../user/user-service';
import { RegisterTable } from './../register-table/register-table';
import { Component, signal, inject } from '@angular/core';

@Component({
  imports: [RegisterTable],
  selector: 'app-register-view',
  styleUrl: './register-view.css',
  templateUrl: './register-view.html',
})
export class RegisterView {
  tableData = signal<Record<string, unknown>[]>([]);

  currentTable = signal<string>('persons');

  private _petService = inject(PetService);

  private _userService = inject(UserService);

  ngOnInit() {
    this.loadPersonTable();
  }

  loadPetsTable() {
    return this._petService.getAllPets().subscribe({
      next: (data) => this.tableData.set(data),
      error: (err) => console.log(err),
    });
  }

  loadPersonTable() {
    return this._userService.getAllUsers().subscribe({
      next: (data) => this.tableData.set(data.map(({ id, ...person }) => person)),
      error: (err) => console.log(err),
    });
  }

  toggle() {
    if (this.currentTable() === 'persons') {
      this.currentTable.set('pets');
      this.loadPetsTable();
    } else if (this.currentTable() === 'pets') {
      this.currentTable.set('persons');
      this.loadPersonTable();
    } else {
      //error
    }
  }
}
