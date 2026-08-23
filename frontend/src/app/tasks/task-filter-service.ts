import { computed, Service, signal } from '@angular/core';

@Service()
export class TaskFilterService {
    readonly search = signal('');
    readonly categoryId = signal<string | null>(null);
    readonly isCompleted = signal<boolean | null>(null);
    readonly page = signal(1);
    readonly pageSize = signal(40);

    readonly isAllTasks = computed(() => 
        this.categoryId() === null &&
        this.isCompleted() === null
    );

    setSearch(value: string): void {
        this.search.set(value.trim());
        this.page.set(1);
    }

    setCategory(categoryId: string | null): void {
        this.categoryId.set(categoryId);
        this.page.set(1);
    }

    setCompleted(isCompleted: boolean | null): void {
        this.isCompleted.set(isCompleted);
        this.page.set(1);
    }

    setPage(
        page: number,
        pageSize: number
    ): void {
        this.page.set(page);
        this.pageSize.set(pageSize);
    }

    showAll(): void {
        this.categoryId.set(null);
        this.isCompleted.set(null);
        this.page.set(1);
    }
}
