import { ApplicationConfig, inject, provideAppInitializer, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';


import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { InitService } from '../core/services/init-service';
import { last, lastValueFrom } from 'rxjs';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes , withViewTransitions()),
    provideHttpClient(),// Ensure HttpClient is provided
    provideAppInitializer(async () => {
      const initService = inject(InitService);
      return new Promise(resolve => {
        setTimeout(() => {
          try {
            return lastValueFrom(initService.init());
          } finally {
            const splach = document.getElementById('initial-splash');
            if (splach) {
              splach.remove();
            }
          }
        }, 1000);
      })
    })

  ]
};
