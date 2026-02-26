import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ConfigService {
  private config: any;

  constructor(private http: HttpClient) { }

  loadConfig(): void {
    this.http.get('/config/config.json').subscribe((response) => {
      this.config = response;
    });
  }

  get apiBaseUrl(): string {
    return this.config?.apiBaseUrl;
  }
}
