import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastItem } from './core/toast/toast-item/toast-item';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    ToastItem
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('SoftPlus');
}
