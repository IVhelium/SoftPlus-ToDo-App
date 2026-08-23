import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./auth/login/login')
            .then(component => component.Login)
    },
    {
        path: 'register',
        loadComponent: () => import('./auth/register/register')
            .then(component => component.Register)
    },
    {
        path: '',
        loadComponent: () => import('./layout/app-layout/app-layout')
            .then(component => component.AppLayout),

        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'tasks'
            },
            {
                path: 'tasks',
                loadComponent: () => import('./tasks/tasks-page/tasks-page')
                    .then(component => component.TasksPage)
            }
        ]
    },
    {
        path: '**',
        redirectTo: 'tasks'
    }
];