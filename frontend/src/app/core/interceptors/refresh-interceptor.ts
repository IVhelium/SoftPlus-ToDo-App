import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "../../auth/auth-service";
import { ToastService } from "../toast/toast-service";
import { Router } from "@angular/router";
import { catchError, switchMap, throwError } from "rxjs";

export const refreshInterceptor: HttpInterceptorFn = (request, next) => {
    const authService = inject(AuthService);
    const toastService = inject(ToastService);
    const router = inject(Router);

    const skipRefresh = 
        request.url.includes('/api/auth/login') ||
        request.url.includes('/api/auth/register') || 
        request.url.includes('/api/auth/refresh')

    return next(request).pipe(
        catchError(error => {
            if (!(error instanceof HttpErrorResponse) || error.status !== 401 || skipRefresh)
                return throwError(() => error); 

            return authService.refresh().pipe(
                switchMap(() => next(request)),  // If the refresh is successful, repeat the request
                catchError(refreshError => {
                    toastService.error('Session expired. Please log in again');
                    void router.navigateByUrl('/login');

                    return throwError(() => refreshError);
                })
            );
                
        }),
    );
}