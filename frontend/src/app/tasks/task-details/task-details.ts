import { Component, input, output } from '@angular/core';
import { Task } from '../task';
import { LucideX, LucideCheck, LucideTag, LucideCalendarDays, LucideClock3, LucideTrash2 } from "@lucide/angular";
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-task-details',
  imports: [DatePipe, LucideX, LucideCheck, LucideTag, LucideCalendarDays, LucideClock3, LucideTrash2],
  templateUrl: './task-details.html',
})
export class TaskDetails {
  readonly open = input(false);
  readonly task = input<Task | null>(null);
  readonly closed = output<void>();
  readonly edit = output<void>();
  readonly remove = output<void>();
  readonly statusChanged = output<void>();

  protected close(): void {
    this.closed.emit();
  }
}
