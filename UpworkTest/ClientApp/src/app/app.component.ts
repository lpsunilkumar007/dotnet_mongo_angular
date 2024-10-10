import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';
import * as AuthActions from './store/auth/auth.actions';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './shared/navbar/navbar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule,NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  isAuthenticated = false;
  title = 'SecurePrivacy';
  constructor(
    private store: Store,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}
  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      const storedAuth = localStorage.getItem('isAuthenticated');
      this.isAuthenticated = storedAuth === 'true';
      this.cdr.detectChanges();
    }
  }

  logout() {
    localStorage.setItem('isAuthenticated', 'false');
    this.store.dispatch(
      AuthActions.setAuthenticated({ isAuthenticated: false })
    );
    this.router.navigate(['/login']);
  }
}
