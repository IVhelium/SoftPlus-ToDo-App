import { Component, HostListener, input, output, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LucideListTodo, LucideClock, LucideCheck, LucidePlus, LucideEllipsis } from "@lucide/angular";
import { Category } from '../../categories/cotegory';


@Component({
  selector: 'app-sidebar',
  imports: [
    RouterLink,
    RouterLinkActive,
    LucideListTodo,
    LucideClock,
    LucideCheck,
    LucidePlus,
    LucideEllipsis
],
  templateUrl: './sidebar.html',
})
export class Sidebar {
  readonly isOpen = input(false);   // The value comes from the Header
  readonly closed = output<void>(); // The event is sent back to the Header

  protected readonly isCategoryFormOpen = signal(false);
  protected readonly categories = signal<Category[]>([]); 

  protected close(): void {
    this.closed.emit();
  }

  @HostListener('document:keydown.escape')
  protected closeOnEscape(): void {
    if (this.isOpen()) {
      this.close();
    }
  }

  protected openCategoryForm(): void {
    this.isCategoryFormOpen.set(true);
  }

  protected closeCategoryForm(): void {
    this.isCategoryFormOpen.set(false);
  }
}
