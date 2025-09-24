import { Injectable } from "@angular/core";
import { environment } from "../../enviroment/enviroment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class LogService {

    private apiUrl = `${environment.apiUrl}`

    constructor(private http: HttpClient) {}

    private getHeaders(): HttpHeaders {
        const token = localStorage.getItem('token');
        return new HttpHeaders({
          Authorization: token ? `Bearer ${token}` : ''
        });
    }

    getAllLogs(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/Log`, {headers: this.getHeaders() });
    }

    createLog(log: any): Observable<any>{
        return this.http.post<any>(`${this.apiUrl}/Log`, log, {headers: this.getHeaders() });
    }

    search(term: string): Observable<any[]>{
        return this.http.get<any[]>(`${this.apiUrl}/Log/search?term=${term}`, {headers: this.getHeaders() });
    }
}