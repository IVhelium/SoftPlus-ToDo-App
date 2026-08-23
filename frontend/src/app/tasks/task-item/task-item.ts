import { DatePipe } from "@angular/common";
import { Component, input, output } from '@angular/core';
import { Task } from '../task';
import { LucideCheck, LucideCalendarDays, LucidePencil, LucideTrash2 } from "@lucide/angular";

@Component({
  selector: 'app-task-item',
  imports: [
    DatePipe,
    LucideCheck, 
    LucideCalendarDays, 
    LucidePencil, 
    LucideTrash2
  ],
  templateUrl: './task-item.html',
})
export class TaskItem {
  readonly task = input.required<Task>();
  readonly opened = output<void>();
  readonly statusChanged = output<void>();
  readonly edit = output<void>();
  readonly remove = output<void>();
}
