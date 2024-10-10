import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../../store/auth/auth.actions';
import { AccountService } from '../../../core/services/account/acount.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private store: Store,
    private userService: AccountService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    const email = this.loginForm.value['email'];
    const password = this.loginForm.value['password'];

    this.userService.loginUser(email, password).subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this.store.dispatch(
          AuthActions.setAuthenticated({ isAuthenticated: true })
        );
        localStorage.setItem('isAuthenticated', 'true');
        this.router.navigate(['/user']).then(() => {
          window.location.reload();
        });
      } else {
        this.errorMessage = 'Incorrect email or password';
      }
    });
  }
}
