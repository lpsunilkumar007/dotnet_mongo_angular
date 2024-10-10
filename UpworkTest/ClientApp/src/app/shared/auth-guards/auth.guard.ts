import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import * as AuthSelectors from '../../store/auth/auth.selectors';
import * as AuthActions from '../../store/auth/auth.actions';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private store: Store, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.store.select(AuthSelectors.selectIsAuthenticated).pipe(
      map((isAuthenticated) => {
        // If store says user is authenticated, allow activation
        if (isAuthenticated) {
          return true;
        }

        // Check localStorage if store doesn't have the authenticated state
        const isAuthenticatedFromLocalStorage =
          localStorage.getItem('isAuthenticated') === 'true';

        if (isAuthenticatedFromLocalStorage) {
          // Dispatch an action to update the store with the localStorage value
          this.store.dispatch(
            AuthActions.setAuthenticated({ isAuthenticated: true })
          );
          return true;
        } else {
          // Navigate to login if not authenticated
          this.router.navigate(['/login']);
          return false;
        }
      })
    );
  }
}
