import { ApiClient } from '../../api/api-client';
import { RegisterTable } from './../register-table/register-table';
import { Component, signal, inject} from '@angular/core';

@Component({
  imports: [RegisterTable],
  selector: 'app-register-view',
  styleUrl: './register-view.css',
  templateUrl: './register-view.html',
})
export class RegisterView {

  private apiClient = inject(ApiClient);

  tableData = signal<Record<string, unknown>[]>([])

  pets = signal<string[]>([])
  persons = signal<string[]>([])

  onClick(){
    this.getPetsTable()
    //this.getPersonTable()
  }
  getPetsTable(){
    this.apiClient.pets().subscribe({
      next: (res) => {
      console.log(res)},
      error: (err) => console.log(err),
    });
  }

  getPersonTable(){
    this.apiClient.persons().subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }
  
}
