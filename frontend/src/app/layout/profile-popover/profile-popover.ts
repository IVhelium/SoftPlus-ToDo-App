import { Component, inject } from '@angular/core';
import { AuthService } from '../../auth/auth-service';
import { Router } from '@angular/router';
import { LucideLogOut } from "@lucide/angular";

@Component({
  selector: 'app-profile-popover',
  imports: [
    LucideLogOut
],
  templateUrl: './profile-popover.html',
})
export class ProfilePopover {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  protected logout(): void {
    this.authService
      .logout()
      .subscribe(() => {
        void this.router.navigateByUrl('/login');
      });
  }
}
