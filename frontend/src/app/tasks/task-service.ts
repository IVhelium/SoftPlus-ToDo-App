import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { PaginatedResponse } from '../pagination/pagination';
import { Task, TaskRequest } from './task';
import { Observable } from 'rxjs';

@Service()
export class TaskService {
    private readonly httpClient = inject(HttpClient);

    getTasks(
        search: string,
        categoryId: string | null,
        isCompleted: boolean | null,
        page: number,
        pageSize: number
    ): Observable<PaginatedResponse<Task>> {
        let params = new HttpParams()
            .set('page', page)
            .set('pageSize', pageSize);

        if (search.trim()) params = params.set('search', search.trim());
        if (categoryId) params = params.set('categoryId', categoryId);
        if (isCompleted !== null) params = params.set('isCompleted', isCompleted);
        
        return this.httpClient.get<PaginatedResponse<Task>>(
            '/api/tasks/get',
            {
                params,
            }
        );
    }

    createTask(request: TaskRequest): Observable<Task> {
        return this.httpClient.post<Task>(
            '/api/tasks/create',
            request,
        );
    }

    updateTask(
        taskId: string,
        request: TaskRequest
    ): Observable<Task> {
        return this.httpClient.patch<Task>(
            `/api/tasks/update/${taskId}`,
            request,
        );
    }

    changeStatus(
        taskId: string,
        isCompleted: boolean
    ): Observable<void> {
        return this.httpClient.patch<void>(
            `/api/tasks/update/status/${taskId}`,
            {
                isCompleted
            },
        );
    }

    deleteTask(taskId: string): Observable<void> {
        return this.httpClient.delete<void>(
            `/api/tasks/delete/${taskId}`,
        );
    }
}