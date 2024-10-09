import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/auth/register/register.component';
import { UserComponent } from './pages/user/user.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { AuthGuard } from './shared/auth-guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
];
