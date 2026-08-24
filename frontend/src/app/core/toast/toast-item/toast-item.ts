import { Component, inject } from '@angular/core';
import { ToastService } from '../toast-service';
import { LucideCircleAlert, LucideX } from "@lucide/angular";

@Component({
  selector: 'app-toast-item',
  imports: [LucideCircleAlert, LucideX],
  templateUrl: './toast-item.html',
})
export class ToastItem {
  protected readonly toastService = inject(ToastService);
}
