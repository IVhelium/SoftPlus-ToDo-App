import { Component, effect, inject, signal } from '@angular/core';
import { TaskService } from '../task-service';
import { TaskFilterService } from '../task-filter-service';
import { Task, TaskRequest } from '../task';
import { CategoryService } from '../../categories/category-service';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { LucideAArrowDown, LucidePlus } from "@lucide/angular";
import { TaskItem } from '../task-item/task-item';
import { TaskForm } from '../task-form/task-form';
import { TaskDetails } from '../task-details/task-details';

@Component({
  selector: 'app-tasks-page',
  imports: [
    TaskForm,
    TaskDetails,
    TaskItem,
    LucidePlus,
    MatPaginator
],
  templateUrl: './tasks-page.html',
})
export class TasksPage {
  private readonly taskService = inject(TaskService);
  private readonly categoryService = inject(CategoryService);
  protected readonly filterService = inject(TaskFilterService);

  protected readonly tasks = signal<Task[]>([]);
  protected readonly totalCount = signal(0);
  protected readonly loading = signal(false);
  protected readonly isTaskFormOpen = signal(false);
  protected readonly editingTask = signal<Task | null>(null);
  protected readonly selectedTask = signal<Task | null>(null);
  protected readonly isTaskDetailsOpen = signal(false);

  constructor() {
    effect(() => {
      const search = this.filterService.search();
      const categoryId = this.filterService.categoryId();
      const isCompleted = this.filterService.isCompleted();
      const page = this.filterService.page();
      const pageSize =this.filterService.pageSize();

      this.loadTasks(
        search,
        categoryId,
        isCompleted,
        page,
        pageSize
      );
    });
  }

  private loadTasks(
    search: string,
    categoryId: string | null,
    isCompleted: boolean | null,
    page: number,
    pageSize: number
  ): void {
    this.loading.set(true);

    this.taskService.getTasks(
      search,
      categoryId,
      isCompleted,
      page,
      pageSize
    ).subscribe({
      next: response => {
        this.tasks.set(response.items);
        this.totalCount.set(response.totalCount);
        this.loading.set(false);
      },

      error: () => {
        this.loading.set(false);
      }
    })
  }

  private reloadTasks(): void {
    this.loadTasks(
      this.filterService.search(),
      this.filterService.categoryId(),
      this.filterService.isCompleted(),
      this.filterService.page(),
      this.filterService.pageSize()
    );
  }

  protected changePage(event: PageEvent): void {
    this.filterService.setPage(event.pageIndex + 1, event.pageSize);
  }

  protected openTaskDetails(task: Task): void {
    this.selectedTask.set(task);
    this.isTaskDetailsOpen.set(true);
  }

  protected closeTaskDetails(): void {
    this.isTaskDetailsOpen.set(false);

    setTimeout(() => {
      if (!this.isTaskDetailsOpen())
        this.selectedTask.set(null);
    }, 300);
  }

  protected openCreateTask(): void {
    this.editingTask.set(null);
    this.isTaskFormOpen.set(true);
  }

  protected openEditTask(task: Task): void {
    this.editingTask.set(task);
    this.isTaskFormOpen.set(true);
  }

  protected closeTaskForm(): void {
    this.isTaskFormOpen.set(false);
    
    setTimeout(() => {
      if (!this.isTaskFormOpen())
        this.editingTask.set(null);
    }, 300);
  }

  protected saveTask(request: TaskRequest): void {
    const task = this.editingTask();
    const operation = task ? this.taskService.updateTask(task.id, request)
      : this.taskService.createTask(request);

    operation.subscribe({
      next: savedTask => {
        if (this.selectedTask()?.id === savedTask.id)
          this.selectedTask.set(savedTask);

        this.closeTaskForm();
        this.reloadTasks();
        this.categoryService.getCategories();
      },

      error: () => {}
    });
  }

  protected changeStatus(task: Task): void {
    const isCompleted = !task.isCompleted;

    this.taskService.changeStatus(
      task.id,
      isCompleted
    ).subscribe({
      next: () => {
        const now = new Date().toISOString();

        if (this.selectedTask()?.id === task.id) {
          this.selectedTask.update(selectedTask =>
            selectedTask ? {
              ...task,
              isCompleted,
              completedAtUtc: isCompleted ? now : null,
              updatedAtUtc: now
            } : null
          );
        }

        this.reloadTasks();
      },

      error: () => {}
    });
  }

  protected deleteTask(task: Task): void {
    const confirmed = confirm(`Delete task "${task.name}"`);
    if (!confirmed) return;

    this.taskService.deleteTask(task.id)
      .subscribe({
        next: () => {
          if (this.selectedTask()?.id === task.id)
            this.closeTaskDetails();

          this.reloadTasks();
          this.categoryService.getCategories();
        },

        error: () => {}
      });
  }
}
