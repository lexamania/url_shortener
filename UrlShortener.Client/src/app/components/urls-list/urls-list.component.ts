import { Component, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, ShortUrlDto, ListResultModel } from '../../services/api.service';

@Component({
  selector: 'app-urls-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './urls-list.component.html',
  styleUrl: './urls-list.component.scss'
})
export class UrlsListComponent implements OnInit {
  private apiService = inject(ApiService);

  urls = signal<ShortUrlDto[]>([]);
  selectedIds = signal<Set<string>>(new Set());
  isLoading = signal(false);
  currentPage = signal(1);
  pageSize = signal(10);
  totalPages = signal(0);
  hasMorePages = signal(false);

  errorMsg = signal<string | null>(null);
  newUrl = this.getClearUrlForm();

  ngOnInit(): void {
    this.loadUrls();
  }

  loadUrls(): void {
    this.isLoading.set(true);
    this.apiService.getUrls(this.currentPage(), this.pageSize()).subscribe({
      next: (response: ListResultModel<ShortUrlDto>) => {
        this.urls.update(items => [...items, ...response.items]);
        this.totalPages.set(response.page.totalPages);
        this.hasMorePages.set(
          this.currentPage() < this.totalPages()
        );
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error loading URLs:', err);
        this.isLoading.set(false);
      }
    });
  }

  loadMore(): void {
    if (this.hasMorePages()) {
      this.currentPage.update(p => p + 1);
      this.loadUrls();
    }
  }

  toggleSelection(id: string): void {
    this.selectedIds.update(ids => {
      const newIds = new Set(ids);
      if (newIds.has(id)) {
        newIds.delete(id);
      } else {
        newIds.add(id);
      }
      return newIds;
    });
  }

  isSelected(id: string): boolean {
    return this.selectedIds().has(id);
  }

  deleteUrl(id: string): void {
    if (confirm('Are you sure you want to delete this URL?')) {
      this.apiService.deleteUrls([id]).subscribe({
        next: () => {
          this.urls.update(items => items.filter(url => url.id !== id));
          this.selectedIds.update(ids => {
            const newIds = new Set(ids);
            newIds.delete(id);
            return newIds;
          });
        },
        error: (err) => console.error('Error deleting URL:', err)
      });
    }
  }

  deleteSelected(): void {
    if (this.selectedIds().size === 0) return;
    if (confirm(`Delete ${this.selectedIds().size} URL(s)?`)) {
      this.apiService.deleteUrls(Array.from(this.selectedIds())).subscribe({
        next: () => {
          const idsToDelete = new Set(this.selectedIds());
          this.urls.update(items => items.filter(url => !idsToDelete.has(url.id)));
          this.selectedIds.set(new Set());
        },
        error: (err) => console.error('Error deleting URLs:', err)
      });
    }
  }

  copyToClipboard(text: string): void {
    navigator.clipboard.writeText(text).then(() => {
      alert('Copied to clipboard!');
    });
  }

  createUrl(): void {
    this.errorMsg.set(null);

    if (!this.newUrl.url) {
      this.errorMsg.set('URL is required');
      return;
    }

    this.apiService
      .createUrl(this.newUrl.url, this.newUrl.shortUrl || undefined, this.newUrl.title || undefined)
      .subscribe({
        next: (response: ShortUrlDto) => {
          this.urls.update(list => [response, ...list]);
          this.newUrl = this.getClearUrlForm();
        },
        error: (err) => {
          console.error('Error creating URL:', err);
          this.errorMsg.set(err.error?.message || 'Failed to create URL');
        }
      });
  }

  private getClearUrlForm() {
    return { url: '', shortUrl: '', title: '' };
  }
}
