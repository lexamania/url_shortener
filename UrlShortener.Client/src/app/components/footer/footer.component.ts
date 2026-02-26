import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  template: `
    <footer class="app-footer">
      <p>&copy; {{ currentYear }} Lexamania. All rights reserved.</p>
    </footer>
  `,
  styles: [`
    .app-footer {
      background-color: var(--footer-bg);
      border-top: 1px solid var(--border-color);
      padding: 1.5rem 2rem;
      text-align: center;
      margin-top: auto;

      p {
        margin: 0;
        color: var(--text-secondary);
        font-size: 0.875rem;
        font-weight: 500;
      }
    }
  `]
})
export class FooterComponent {
  currentYear = new Date().getFullYear();
}
