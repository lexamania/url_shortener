import { Injectable, inject, signal } from '@angular/core';
import { ApiService, UserDto } from './api.service';
import { BehaviorSubject, firstValueFrom, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiService = inject(ApiService);

  private userSubject = new BehaviorSubject<UserDto | null>(null);
  user$ = this.userSubject.asObservable();

  async checkAuth(): Promise<boolean> {
    try {
      const user = await firstValueFrom(this.apiService.me());
      this.userSubject.next(user);
      return true;
    } catch (err) {
      console.error('Unauthorized:', err);
      this.userSubject.next(null);
      return false;
    }
  };
}
