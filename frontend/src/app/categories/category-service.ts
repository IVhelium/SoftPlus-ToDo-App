import { HttpClient } from '@angular/common/http';
import { inject, Service, signal } from '@angular/core';
import { Category, CategoryRequest } from './cotegory';
import { Observable } from 'rxjs';

@Service()
export class CategoryService {
    private readonly httpClient = inject(HttpClient);

    readonly categories = signal<Category[]>([]);

    getCategories(): void {
        this.httpClient.get<Category[]>(
            '/api/categories/get',
        ).subscribe({
            next: categories => {
                this.categories.set(categories)
            },

            error: () => {}
        });
    }

    createCategory(request: CategoryRequest): Observable<Category> {
        return this.httpClient.post<Category>(
            '/api/categories/create',
            request,
        );
    }

    updateCategory(
        categoryId: string,
        request: CategoryRequest
    ): Observable<Category> {
        return this.httpClient.patch<Category>(
            `/api/categories/update/${categoryId}`,
            request,
        );
    }

    deleteCategory(categoryId: string): Observable<void> {
        return this.httpClient.delete<void>(
            `/api/categories/delete/${categoryId}`,
        );
    }
}
