import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import {
  LucideMenu,
  LucideSearch,
  LucideX,
  LucideUser
} from '@lucide/angular'
import { Sidebar } from '../sidebar/sidebar';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,

    Sidebar,

    LucideMenu,
    LucideSearch,
    LucideX,
    LucideUser
  ],
  templateUrl: './header.html',
})
export class Header {
  protected readonly isMenuOpen = signal(false);

  protected toggleMenu(): void {
    this.isMenuOpen.update(value => !value);
  }

  protected closeMenu(): void {
    this.isMenuOpen.set(false);
  }
}
