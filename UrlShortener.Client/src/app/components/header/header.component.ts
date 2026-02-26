import { ApiService } from './../../services/api.service';
import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ThemeService } from '../../services/theme.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  private themeService = inject(ThemeService);
  private authService = inject(AuthService);
  private apiService = inject(ApiService);
  private router = inject(Router);

  isAuthDropdownOpen = signal(false);
  user$ = this.authService.user$;
  currentTheme = this.themeService.currentTheme;

  toggleTheme(): void {
    this.themeService.toggleTheme();
  }

  toggleAuthDropdown(): void {
    this.isAuthDropdownOpen.update(v => !v);
  }

  logout(): void {
    this.apiService.logout().subscribe({
      next: async () => {
        await this.authService.checkAuth();
        this.isAuthDropdownOpen.set(false);
      },
      error: (err) => {
        console.error('Error during logout:', err);
      }
    });
  }

  goHome(): void {
    this.router.navigate(['/']);
  }
}
