export interface Task {
    id: string,
    name: string,
    description: string | null,
    isCompleted: boolean,
    completedAtUtc: string | null
    dueDateUtc: string | null,

    createdAtUtc: string,
    updatedAtUtc: string | null,

    categoryId: string | null,
    categoryName: string | null,
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
