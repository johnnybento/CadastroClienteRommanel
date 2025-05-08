import { environment } from 'src/environments/environment';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface RegistrarPayload {
  cmd: any;
}

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private apiUrl = environment.apiUrl; //

  constructor(private http: HttpClient) {}

  // Antes: registrar(data: Partial<Cliente>): Observable<{ id: string }>
  // Agora:
  registrar(payload: RegistrarPayload): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(this.apiUrl, payload);
  }

  obterPorId(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
}
