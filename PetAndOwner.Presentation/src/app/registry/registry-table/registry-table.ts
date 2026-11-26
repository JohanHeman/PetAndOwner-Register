import { Component, inject, computed, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-register-table',
  styleUrl: './registry-table.css',
  templateUrl: './registry-table.html',
})
export class RegisterTable {


  data = input.required<Record<string, unknown>[]>();//record is generic any item type with a string key and unkown value, so object feild names and their values

  columns = computed(() => {
    const firstRow = this.data()[0];
    if (firstRow === undefined) {
      return []; // no data = no columns
    }
    return Object.keys(firstRow); //returns an array of the object’s property names
  });

}
