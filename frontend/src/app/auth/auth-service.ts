import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { LoginRequest, LogoutResponse, RegisterRequest, RegisterResponse } from './auth';
import { finalize, map, Observable, shareReplay } from 'rxjs';

@Service()
export class AuthService {
    private readonly httpClient = inject(HttpClient);

    private refreshRequest: Observable<void> | null = null;

    login(request: LoginRequest): Observable<void> {
        return this.httpClient.post<void>(
            '/api/auth/login',
            request,
        );
    }

    register(request: RegisterRequest): Observable<RegisterResponse> {
        return this.httpClient.post<RegisterResponse>(
            '/api/auth/register',
            request,
        );
    }

    refresh(): Observable<void> {
        if (this.refreshRequest) 
            return this.refreshRequest; // If Refresh is already running, the same request is used

        this.refreshRequest = this.httpClient.post(
            '/api/auth/refresh',
            {}
        ).pipe(
            map(() => undefined), // return Observable<void>
            finalize(() => {
                this.refreshRequest = null
            }),
            shareReplay({
                bufferSize: 1,
                refCount: false
            })
        );

        return this.refreshRequest;
    }

    logout(): Observable<LogoutResponse> {
        return this.httpClient.post<LogoutResponse>(
            '/api/auth/logout',
            {},
        );
    }
}