import { Component, Input } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../store/auth/auth.actions';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  @Input() isVisible: boolean = false; // Input property to control visibility
  constructor(private store: Store, private router: Router) {}
  logout() {
    localStorage.setItem('isAuthenticated', 'false');
    this.store.dispatch(
      AuthActions.setAuthenticated({ isAuthenticated: false })
    );
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}
