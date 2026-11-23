import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Start } from './home/start/start';

@Component({
  imports: [RouterOutlet, Start],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
}
