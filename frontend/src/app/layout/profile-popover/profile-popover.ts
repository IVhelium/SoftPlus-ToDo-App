import { Component, inject } from '@angular/core';
import { AuthService } from '../../auth/auth-service';
import { Router, RouterLink } from '@angular/router';
import { LucideLogOut, LucideLogIn, LucideUserPlus } from "@lucide/angular";

@Component({
  selector: 'app-profile-popover',
  imports: [
    RouterLink,
    LucideLogOut,
    LucideLogIn,
    LucideUserPlus
],
  templateUrl: './profile-popover.html',
})
export class ProfilePopover {
  private readonly router = inject(Router);
  protected readonly authService = inject(AuthService);

  constructor() {
    this.authService.loadCurrentUser();
  }

  protected logout(): void {
    this.authService
      .logout()
      .subscribe({
        next: () => {
          void this.router.navigateByUrl('/login');
        },
        error: () => {}
      });
  }
}
