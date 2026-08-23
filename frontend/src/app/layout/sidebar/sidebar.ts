import { Component, HostListener, inject, input, output, signal } from '@angular/core';
import { LucideListTodo, LucideClock, LucideCheck, LucidePlus, LucideEllipsis, LucideTrash2 } from "@lucide/angular";
import { Category, CategoryRequest } from '../../categories/cotegory';
import { CategoryService } from '../../categories/category-service';
import { TaskFilterService } from '../../tasks/task-filter-service';
import { CategoryForm } from "../../categories/category-form/category-form";


@Component({
  selector: 'app-sidebar',
  imports: [
    LucideListTodo,
    LucideClock,
    LucideCheck,
    LucidePlus,
    LucideEllipsis,
    LucideTrash2,
    CategoryForm
  ],
  templateUrl: './sidebar.html',
})
export class Sidebar {
  protected readonly categoryService = inject(CategoryService);
  protected readonly filterService = inject(TaskFilterService);

  constructor() {
    this.categoryService.getCategories();
  }

  readonly isOpen = input(false);   // The value comes from the Header
  readonly closed = output<void>(); // The event is sent back to the Header

  protected readonly isCategoryFormOpen = signal(false);
  protected readonly editingCategory = signal<Category | null>(null);
  protected readonly openedCategoryMenu = signal<string | null>(null);

  protected close(): void {
    this.closed.emit();
  }

  @HostListener('document:keydown.escape')
  protected closeOnEscape(): void {
    if (this.isOpen()) {
      this.close();
    }
  }

  protected showAll(): void {
    this.filterService.showAll();
    this.close();
  }

  protected showActive(): void {
    this.filterService.setCompleted(false);
    this.close();
  }

  protected showCompleted(): void {
    this.filterService.setCompleted(true);
    this.close();
  }

  protected selectCategory(categoryId: string): void {
    this.filterService.setCategory(categoryId);
    this.close();
  }

  protected openCreateCategory(): void {
    this.editingCategory.set(null);
    this.isCategoryFormOpen.set(true);
  }

  protected openEditCategory(category: Category): void {
    this.openedCategoryMenu.set(null);
    this.editingCategory.set(category);
    this.isCategoryFormOpen.set(true);
  }

  protected closeCategoryForm(): void {
    this.isCategoryFormOpen.set(false);
    this.editingCategory.set(null);
  }

  protected saveCategory(request: CategoryRequest): void {
    const category = this.editingCategory();
    const operation = category ? this.categoryService.updateCategory(category.id, request)
      : this.categoryService.createCategory(request);

    operation.subscribe({
      next: () => {
        this.categoryService.getCategories();
        this.closeCategoryForm();
      },
      error: () => {}
    });
  }

  protected deleteCategory(category: Category): void {
    const confirmed = confirm(`Delete category "${category.name}"?`);

    if (!confirmed) return;

    this.categoryService.deleteCategory(category.id)
      .subscribe({
        next: () => {
          if (this.filterService.categoryId() === category.id)
            this.filterService.setCategory(null);

          this.categoryService.getCategories();
          this.openedCategoryMenu.set(null);
        },
        error: () => {}
      });
  }

  protected toggleCategoryMenu(categoryId: string): void {
    if (this.openedCategoryMenu() === categoryId) {
      this.openedCategoryMenu.set(null);
      return;
    }

    this.openedCategoryMenu.set(categoryId);
  }
}