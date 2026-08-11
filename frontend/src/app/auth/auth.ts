export interface LoginRequest {
    email: string,
    password: string
}

export interface RegisterRequest {
    username: string,
    email: string,
    password: string
}

export interface RegisterResponse {
    message: string,
    userId: string
}

export interface LogoutResponse {
    message: string
}