// src/app/core/user/user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/user/userModel';
import { API_URL } from '../../config/api.config';

@Injectable({
  providedIn: 'root',
})
export class UserDataAccessService {
  private apiUrl = `${API_URL}/UsersDataAccess`;

  constructor(private http: HttpClient) {}
  markForDelete(user: User): Observable<User>{
    return this.http.put<User>(`${this.apiUrl}/UpdateByUserIdAsync?id=${user.id}`, user);
  }
}
