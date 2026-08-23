import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LucideMenu, LucideSearch, LucideX, LucideUser, LucideLogOut } from '@lucide/angular'
import { Sidebar } from '../sidebar/sidebar';
import { TaskFilterService } from '../../tasks/task-filter-service';
import { FormsModule } from '@angular/forms';
import { ProfilePopover } from '../profile-popover/profile-popover';

@Component({
  selector: 'app-header',
  imports: [
    FormsModule,
    RouterLink,
    Sidebar,
    ProfilePopover,
    LucideMenu,
    LucideSearch,
    LucideX,
    LucideUser
],
  templateUrl: './header.html',
})
export class Header {
  private readonly router = inject(Router);
  private readonly filterService = inject(TaskFilterService);

  protected readonly isMenuOpen = signal(false);
  protected readonly isProfileOpen = signal(false);

  protected search = this.filterService.search();


  protected toggleMenu(): void {
    this.isMenuOpen.update(value => !value);
  }

  protected closeMenu(): void {
    this.isMenuOpen.set(false);
  }

  protected toggleProfile(): void {
    this.isProfileOpen.update(value => !value);
  }

  protected searchTasks(): void {
    this.filterService.setSearch(this.search);

    void this.router.navigateByUrl('/tasks');
  }
}
