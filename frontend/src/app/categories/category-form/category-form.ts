import { Component, input, OnChanges, output, SimpleChanges } from '@angular/core';
import { Category, CategoryRequest } from '../cotegory';
import { LucideX } from "@lucide/angular";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-category-form',
  imports: [
    FormsModule,
    LucideX
  ],
  templateUrl: './category-form.html',
})
export class CategoryForm implements OnChanges {
  readonly open = input(false);
  readonly category = input<Category | null>(null);
  readonly saved = output<CategoryRequest>();
  readonly closed = output<void>();

  protected name = '';
  protected color = '#2564cf';

  ngOnChanges(changes: SimpleChanges): void {
    const category = this.category();
    this.name = category?.name ?? '';
    this.color = category?.color ?? '#2564cf';
  }

  protected save(): void {
    if (!this.name.trim()) return;

    this.saved.emit({
      name: this.name.trim(),
      color: this.color
    });
  }

  protected close(): void {
    this.closed.emit();
  }
}
