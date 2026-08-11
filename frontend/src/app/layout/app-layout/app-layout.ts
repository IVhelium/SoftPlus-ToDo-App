import { Component } from '@angular/core';
import { Header } from '../header/header';

@Component({
  selector: 'app-layout',
  imports: [
    Header,
  ],
  templateUrl: './app-layout.html',
})
export class AppLayout {}
