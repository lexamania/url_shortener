import { Component, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiService, DetailedUrlDto } from '../../services/api.service';

@Component({
  selector: 'app-url-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './url-detail.component.html',
  styleUrl: './url-detail.component.scss'
})
export class UrlDetailComponent implements OnInit {
  private apiService = inject(ApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  urlData = signal<DetailedUrlDto | null>(null);
  isLoading = signal(true);
  isEditing = signal(false);
  editTitle = signal('');
  isSaving = signal(false);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.router.navigate(['/']);
      return;
    }

    this.loadUrlDetail(id);
  }

  loadUrlDetail(id: string): void {
    this.isLoading.set(true);

    this.apiService.getUrlDetail(id).subscribe({
      next: (data) => {
        this.urlData.set(data);
        this.editTitle.set(data.title);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error loading URL detail:', err);
        this.isLoading.set(false);
      }
    });
  }

  startEdit(): void {
    if (this.urlData()?.canModify) {
      this.isEditing.set(true);
    }
  }

  cancelEdit(): void {
    this.isEditing.set(false);
    if (this.urlData()) {
      this.editTitle.set(this.urlData()!.title);
    }
  }

  saveTitle(): void {
    if (!this.urlData()) return;

    this.isSaving.set(true);
    this.apiService.updateUrl(this.urlData()!.id, this.editTitle()).subscribe({
      next: (updated) => {
        this.urlData.set(updated);
        this.isEditing.set(false);
        this.isSaving.set(false);
      },
      error: (err) => {
        console.error('Error updating URL:', err);
        this.isSaving.set(false);
      }
    });
  }

  deleteUrl(): void {
    if (!this.urlData()?.canModify) return;
    if (confirm('Are you sure you want to delete this URL?')) {
      this.apiService.deleteUrls([this.urlData()!.id]).subscribe({
        next: () => {
          this.router.navigate(['/']);
        },
        error: (err) => console.error('Error deleting URL:', err)
      });
    }
  }

  copyToClipboard(text: string): void {
    navigator.clipboard.writeText(text).then(() => {
      alert('Copied to clipboard!');
    });
  }
}
