import { Component, inject, input, OnChanges, output, SimpleChanges } from '@angular/core';
import { CategoryService } from '../../categories/category-service';
import { Task, TaskRequest } from '../task';
import { LucideX } from "@lucide/angular";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-form',
  imports: [
    FormsModule,
    LucideX
  ],
  templateUrl: './task-form.html',
})
export class TaskForm implements OnChanges {
  protected readonly categoryService = inject(CategoryService);

  readonly open = input(false);
  readonly task = input<Task | null>(null);
  readonly saved = output<TaskRequest>();
  readonly closed = output<void>();

  protected name = '';
  protected description = '';
  protected categoryId = '';
  protected dueDate = '';

  ngOnChanges(changes: SimpleChanges): void {
    const task = this.task();

    this.name = task?.name ?? '';
    this.description = task?.description ?? '';
    this.categoryId = task?.categoryId ?? '';
    this.dueDate = task?.dueDateUtc ? task.dueDateUtc.slice(0, 10) : '';
  }

  protected save(): void {
    if (!this.name.trim()) return;

    this.saved.emit({
      name: this.name.trim(),
      description: this.description.trim() || null,
      categoryId: this.categoryId || null,
      dueDateUtc: this.dueDate ? `${this.dueDate}T00:00:00.000Z` : null
    });
  }

  protected close(): void {
    this.closed.emit();
  }
}
