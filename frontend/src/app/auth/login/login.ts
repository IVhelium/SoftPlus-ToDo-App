import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth-service';
import { LoginRequest } from './../auth';

@Component({
  selector: 'app-login',
  imports: [ 
    FormsModule,
    RouterLink
  ],
  templateUrl: './login.html',
})
export class Login {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  protected email = '';
  protected password = '';

  protected login(): void {
    if (!this.email.trim() || !this.password) return;

    this.authService.login({
      email: this.email,
      password: this.password
    }).subscribe({
      next: () => {
        void this.router.navigateByUrl('/tasks');
      },
      error: () => {}
    });
  }
}
