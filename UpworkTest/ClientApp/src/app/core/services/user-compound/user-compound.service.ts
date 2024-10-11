// src/app/core/user/user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserCompound } from '../../models/user/userModel';
import { API_URL_USER_COMPOUND } from '../../config/api.config';

@Injectable({
  providedIn: 'root',
})
export class UserCompoundService {
  private apiUrl = `${API_URL_USER_COMPOUND}`;

  constructor(private http: HttpClient) {}

  fetchUsersCompound(firstName:string, lastName:string): Observable<UserCompound[]> {
    debugger;
    return this.http.get<UserCompound[]>(this.apiUrl + "?firstName=" + firstName + "&lastName=" + lastName);
  }
}
