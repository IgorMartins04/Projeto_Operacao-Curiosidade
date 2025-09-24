import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroment/enviroment';

interface User {
  name: string;
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/User`

  constructor(private http: HttpClient) {}

  createUser(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}`, user);
  }
}
