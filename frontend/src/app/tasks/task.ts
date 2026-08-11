export interface Task {
    id: string,
    name: string,
    description: string | null,
    isCompleted: boolean,
    dueDateUtc: string | null,

    categoryId: string | null,
    categroyName: string | null,
    categoryColor: string | null
}

export interface TaskQuery {
    search?: string,
    categoryId?: string,
    isCompleted?: boolean,

    dueFromUtc?: string,
    dueToUtc?: string,

    page: number,
    pageSize: number
}

export interface TaskRequest {
    name: string,
    description: string | null,
    dueDateUtc: string | null,
    categoryId: string | null
} 