import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Category, CategoryRequest } from './cotegory';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    private readonly httpClient = inject(HttpClient);

    readonly categories = signal<Category[]>([]);

    getCategories(): void {
        this.httpClient.get<Category[]>(
            '/api/categories/get',
            {
                withCredentials: true
            }
        ).subscribe(categories => {
            this.categories.set(categories)
        });
    }

    createCategory(request: CategoryRequest) {
        return this.httpClient.post<Category>(
            '/api/categories/create',
            request,
            {
                withCredentials: true
            }
        );
    }

    updateCategory(
        categoryId: string,
        request: CategoryRequest
    ) {
        return this.httpClient.patch<Category>(
            `/api/categories/update/${categoryId}`,
            request,
            {
                withCredentials: true
            }
        );
    }

    deleteCategory(categoryId: string) {
        return this.httpClient.delete<void>(
            `/api/categories/delete/${categoryId}`,
            {
                withCredentials: true
            }
        )
    }
}
