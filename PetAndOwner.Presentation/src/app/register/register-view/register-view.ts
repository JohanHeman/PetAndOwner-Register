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

  currentTable = signal<string>("persons")


  ngOnInit() {
    this.loadPersonTable();
  }

  loadPetsTable() {
    return this.apiClient.pets().subscribe({
      next: (data) => this.tableData.set(data),
      error: (err) => console.log(err),
    });
  }

  loadPersonTable(){
    return this.apiClient.persons().subscribe({
      next: (data) => this.tableData.set(data),
      error: (err) => console.log(err),
    });
  }

  toggle(){
    if (this.currentTable() === "persons"){
      this.currentTable.set("pets")
      this.loadPetsTable();
    }
    else if(this.currentTable() === "pets"){
      this.currentTable.set("persons")
      this.loadPersonTable();
    }
    else{
      //error
    }
  }

}
