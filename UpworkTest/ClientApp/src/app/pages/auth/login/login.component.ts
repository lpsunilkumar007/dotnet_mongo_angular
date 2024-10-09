import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../../store/auth/auth.actions';

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
    private store: Store // Inject Store
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    debugger;
    const email = this.loginForm.value['email'];
    const password = this.loginForm.value['password'];
    if (email === 'admin@.com' && password === 'admin') {
      // this.store.dispatch(
      //   AuthActions.setAuthenticated({ isAuthenticated: true })
      // );
      this.router.navigate(['/user']);
    } else {
      this.errorMessage = 'Incorrect email or password';
    }
  }
}
