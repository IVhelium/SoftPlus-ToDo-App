import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest, LogoutResponse, RegisterRequest, RegisterResponse } from './auth';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly httpClient = inject(HttpClient);

    login(request: LoginRequest): Observable<void> {
        return this.httpClient.post<void>(
            '/api/auth/login',
            request,
            {
                withCredentials: true
            }
        );
    }

    register(request: RegisterRequest): Observable<RegisterResponse> {
        return this.httpClient.post<RegisterResponse>(
            '/api/auth/register',
            request,
            {
                withCredentials: true
            }
        );
    }

    logout(): Observable<LogoutResponse> {
        return this.httpClient.post<LogoutResponse>(
            '/api/auth/logout',
            {},
            {
                withCredentials: true
            }
        );
    }
}