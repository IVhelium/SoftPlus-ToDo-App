import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { ToastService } from "../toast/toast-service";
import { catchError, throwError } from "rxjs";

export const errorInterceptor: HttpInterceptorFn = (request, next) => {
    const toastService = inject(ToastService);

    return next(request).pipe(
        catchError(error => {
            if (!(error instanceof HttpErrorResponse)) {
                toastService.error('Unexpected error occurred');         
                return throwError(() => error);
            } 

            if (error.status === 401)
                return throwError(() => error);

            const message = getErrorMessage(error);
            toastService.error(message);

            return throwError(() => error);
        })
    );
}

function getErrorMessage(error: HttpErrorResponse): string {
    if (typeof error.error?.message === 'string')
        return error.error.message; 

    /* if Identity return
        [
            {
                code: "",
                description: ""
            }
        ]
    */ 
    if (Array.isArray(error.error)) {
        const description = error.error.map(item =>
            item?.description
        ).filter(description =>
            typeof description === 'string'
        );

        if (description.length > 0) return description.join(', ');
    }

    switch (error.status) {
        case 0:
            return "Unable to connect to the server";

        case 400:
            return "Invalid request";

        case 403:
            return "You do not have permission to perform this action";
        
        case 404:
            return "Requested item was not found";

        case 409:
            return "This item already exists";

        case 500:
            return "Server error. Please Try again";

        default:
            return "Something went wrong"
    }
}