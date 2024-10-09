// src/app/app.config.ts

import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideStore } from '@ngrx/store';
import { userReducer } from './store/user/user.reducer';
import { authReducer } from './store/auth/auth.reducer'; // Import authReducer
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(),
    provideStore({ 
      user: userReducer,
      auth: authReducer // Register the auth reducer
    }),
  ],
};
