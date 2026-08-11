export interface Category {
    id: string,
    name: string,
    color: string | null,
    taskCount: number
}

export interface CategoryRequest {
    name: string,
    color: string | null
}