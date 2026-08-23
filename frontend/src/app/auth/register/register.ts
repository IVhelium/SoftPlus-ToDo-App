import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../auth-service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [
    FormsModule
  ],
  templateUrl: './register.html',
})
export class Register {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  protected username = '';
  protected email = '';
  protected password = '';

  protected register(): void {
    if (!this.username.trim() || !this.email.trim() || this.password.length < 8) return;

    this.authService.register({
      username: this.username,
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
