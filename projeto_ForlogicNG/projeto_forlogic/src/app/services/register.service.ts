import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroment/enviroment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      Authorization: token ? `Bearer ${token}` : ''
    });
  }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Register`, { headers: this.getHeaders() });
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Register/${id}`, { headers: this.getHeaders() });
  }

  create(register: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Register`, register, { headers: this.getHeaders() });
  }

  update(id: number, register: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/Register/${id}`, register, { headers: this.getHeaders() });
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Register/${id}`, { headers: this.getHeaders() });
  }

  search(term: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Register/search?term=${term}`, { headers: this.getHeaders() });
  }

  getTotal(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/Register/total`, { headers: this.getHeaders() });
  }

  getLastMonth(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/Register/last-month`, { headers: this.getHeaders() });
  }

  getPendencies(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/Register/get-pendencies`, { headers: this.getHeaders() });
  }
}
