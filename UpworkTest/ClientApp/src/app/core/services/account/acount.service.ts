// src/app/core/services/user.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterViewModel } from '../../models/account/registerViewModel';
import { API_URL } from '../../config/api.config';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private apiUrl = `${API_URL}/account`;

  constructor(private http: HttpClient) {}

  registerUser(registerData: RegisterViewModel) {
    return this.http.post<RegisterViewModel>(this.apiUrl, registerData);
  }
}
