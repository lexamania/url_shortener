import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-auth-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './auth-modal.component.html',
  styleUrl: './auth-modal.component.scss'
})
export class AuthModalComponent {
  private authService = inject(AuthService);
  private apiService = inject(ApiService);
  private router = inject(Router);

  isLogin = signal(true);
  isLoading = signal(false);
  errorMessage = signal('');
  email = signal('');
  password = signal('');

  toggleMode(): void {
    this.isLogin.update(v => !v);
    this.errorMessage.set('');
    this.email.set('');
    this.password.set('');
  }

  submit(): void {
    if (!this.email() || !this.password()) {
      this.errorMessage.set('Please fill in all fields');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');

    if (this.isLogin())
      this.login();
    else
      this.register();
  }

  private login(): void {
    this.apiService.login(this.email(), this.password()).subscribe({
      next: async () => {
        if (!await this.authService.checkAuth())
          throw new Error('Authentication failed');

        this.isLoading.set(false);
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set(
          err.error?.message || err.message || 'An error occurred. Please try again.'
        );
      }
    });
  }

  private register(): void {
    this.apiService.register(this.email(), this.password()).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.isLogin.set(true);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set(
          err.error?.message || err.message || 'An error occurred. Please try again.'
        );
      }
    });
  }
}
