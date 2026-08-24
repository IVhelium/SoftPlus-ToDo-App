import { HttpClient } from '@angular/common/http';
import { inject, Service, signal } from '@angular/core';
import { AuthUser, LoginRequest, LogoutResponse, RegisterRequest } from './auth';
import { finalize, map, Observable, shareReplay, tap } from 'rxjs';
import ta from '@angular/common/locales/ta';

@Service()
export class AuthService {
    private readonly httpClient = inject(HttpClient);

    readonly currentUser = signal<AuthUser | null>(null);
    readonly authChecked = signal(false);

    private refreshRequest: Observable<AuthUser> | null = null;

    loadCurrentUser(): void {
        if (this.authChecked()) return;

        this.httpClient.get<AuthUser | null>(
            '/api/auth/me',
        ).pipe(
            finalize(() => {
                this.authChecked.set(true);
            })
        ).subscribe({
            next: user => {
                this.currentUser.set(user);
            },
            error: () => {
                this.currentUser.set(null);
            }
        })
    }

    login(request: LoginRequest): Observable<AuthUser> {
        return this.httpClient.post<AuthUser>(
            '/api/auth/login',
            request,
        ).pipe(
            tap(user => {
                this.currentUser.set(user);
                this.authChecked.set(true);
            })
        );
    }

    register(request: RegisterRequest): Observable<AuthUser> {
        return this.httpClient.post<AuthUser>(
            '/api/auth/register',
            request,
        ).pipe(
            tap(user => {
                this.currentUser.set(user);
                this.authChecked.set(true);
            })
        );
    }

    refresh(): Observable<AuthUser> {
        if (this.refreshRequest)
            return this.refreshRequest; // If Refresh is already running, the same request is used

        this.refreshRequest = this.httpClient.post<AuthUser>(
            '/api/auth/refresh',
            {}
        ).pipe(
            tap(user => {
                this.currentUser.set(user);
                this.authChecked.set(true);
            }),
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
        ).pipe(
            tap(() => {
                this.currentUser.set(null);
                this.authChecked.set(true);
            })
        );
    }
}