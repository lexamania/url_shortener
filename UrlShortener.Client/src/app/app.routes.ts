import { Routes } from '@angular/router';
import { UrlsListComponent } from './components/urls-list/urls-list.component';
import { UrlDetailComponent } from './components/url-detail/url-detail.component';
import { AuthModalComponent } from './components/auth-modal/auth-modal.component';
import { AboutComponent } from './components/about/about.component';

export const routes: Routes = [
  { path: '', component: UrlsListComponent },
  { path: 'urls/:id', component: UrlDetailComponent },
  { path: 'login', component: AuthModalComponent },
  { path: 'register', component: AuthModalComponent },
  { path: 'about', component: AboutComponent },
  { path: '**', redirectTo: '' }
];
