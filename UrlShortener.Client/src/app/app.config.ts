import {
  ApplicationConfig,
  provideAppInitializer,
  provideBrowserGlobalErrorListeners,
  inject
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { CookieService } from 'ngx-cookie-service';

import { routes } from './app.routes';
import { ConfigService } from './services/config.service';

export function initializeConfig() {
  const configService = inject(ConfigService);
  return configService.loadConfig();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideAppInitializer(initializeConfig),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideClientHydration(withEventReplay()),
    provideHttpClient(),
    CookieService,
  ]
};
