import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';

export interface ShortUrlDto {
  id: string;
  url: string;
  shortUrl: string;
  isOwner: boolean;
  canModify: boolean;
}

export interface DetailedUrlDto {
  id: string;
  url: string;
  shortUrl: string;
  title: string;
  isOwner: boolean;
  canModify: boolean;
}

export interface PageModel {
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface ListResultModel<T> {
  items: T[];
  page: PageModel;
}

export interface UserDto {
  id: number;
  email: string;
  isAdmin: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    let config = inject(ConfigService);
    this.apiUrl = config.apiBaseUrl;
  }

  getUrls(pageNumber: number = 1, pageSize: number = 10): Observable<ListResultModel<ShortUrlDto>> {
    const params = new HttpParams()
      .set('page', pageNumber)
      .set('size', pageSize);
    return this.http.get<ListResultModel<ShortUrlDto>>(`${this.apiUrl}/urls`, { params });
  }

  getUrlDetail(id: string): Observable<DetailedUrlDto> {
    return this.http.get<DetailedUrlDto>(`${this.apiUrl}/urls/${id}`);
  }

  createUrl(url: string, shortUrl?: string, title?: string): Observable<DetailedUrlDto> {
    return this.http.post<DetailedUrlDto>(`${this.apiUrl}/urls`, { url, shortUrl, title });
  }

  updateUrl(id: string, title: string): Observable<DetailedUrlDto> {
    return this.http.patch<DetailedUrlDto>(`${this.apiUrl}/urls`, { id, title });
  }

  deleteUrls(ids: string[]): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/urls`, { body: { ids } });
  }

  register(email: string, password: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/auth/register`, { email, password });
  }

  login(email: string, password: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/auth/login`, { email, password });
  }

  logout(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/auth/logout`, {});
  }

  me(): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.apiUrl}/auth/me`);
  }
}
