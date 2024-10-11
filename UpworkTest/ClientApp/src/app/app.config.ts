// src/app/app.config.ts

import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideStore } from '@ngrx/store';
import { userReducer } from './store/user/user.reducer';
import { authReducer } from './store/auth/auth.reducer';
import { provideHttpClient } from '@angular/common/http';
import { provideEffects } from '@ngrx/effects';
import { UserEffects } from './store/user/user.effects';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr'; // Import provideToastr
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { UserCompoundEffects } from './store/user-compound/user-compound.effects';
import { userCompoundReducer } from './store/user-compound/user-compound.reducer';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(),
    provideAnimations(),
    provideStore({
      user: userReducer,
      auth: authReducer,      
      usersCompound:userCompoundReducer
    }),
    FontAwesomeModule,
    provideEffects([UserEffects,UserCompoundEffects]),
    provideToastr({
      // Configure Toastr
      positionClass: 'toast-top-right',
      timeOut: 3000,
      preventDuplicates: true,
      progressBar: true,
    }),
  ],
};
