import { Service, signal } from '@angular/core';
import { ToastMessage, ToastType } from './toast';

@Service()
export class ToastService {
    private toastId = 0;
    private readonly maxToasts = 3;

    readonly toasts = signal<ToastMessage[]>([]);

    private show(
        message: string,
        type: ToastType
    ): void {
        const id = ++this.toastId;
        const toast: ToastMessage = {
            id,
            message,
            type,
            visible: false
        }

        this.toasts.update(toasts => {
            const currentToasts = toasts.slice(-(this.maxToasts - 1));

            return [
                ...currentToasts,
                toast
            ];
        });

        setTimeout(() => {
            this.toasts.update(toasts => toasts.map(toast => 
                toast.id === id ? { ...toast, visible: true } : toast
            ));
        });

        setTimeout(() => {
            this.hide(id)
        }, 3500);
    }

    hide(id: number): void {
        this.toasts.update(toasts => toasts.map(toast => 
            toast.id === id ? { ...toast, visible: false } : toast
        ));

        setTimeout(() => {
            this.toasts.update(toast => toast.filter(
                toast => toast.id !== id
            ));
        }, 300);
    }

    error(message: string): void {
        this.show(message, 'error');
    }

    success(message: string): void {
        this.show(message, 'success');
    }

    info(message: string): void {
        this.show(message, 'info');
    }
}
