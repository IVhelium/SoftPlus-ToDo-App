export type ToastType = 'error' | 'success' | 'info';

export interface ToastMessage {
    id: number,
    message: string,
    type: ToastType,
    visible: boolean
}