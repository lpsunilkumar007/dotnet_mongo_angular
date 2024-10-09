// src/app/core/user/user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/user/userModel';
import { API_URL, API_URL_USER_REQUEST_DELETE } from '../../config/api.config';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = `${API_URL}/Users`;
  private apiUrlDelete = `${API_URL_USER_REQUEST_DELETE}`;

  constructor(private http: HttpClient) {}

  fetchUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${user.id}`, user);
  }

  deleteUser(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}?userId=${userId}`);
  }
  markForDelete(user: User): Observable<User>{
    return this.http.put<User>(`${this.apiUrlDelete}`, user);
  }
}
